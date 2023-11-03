using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject [] objects;
    public float respawnTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjectSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ObjectSpawner()
    {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            SpawnObject();
        }
    }

    void SpawnObject()
    {
      int randomValue = Random.Range(0, objects.Length);
      int randomX = Random.Range(-6, 6);
      int randomY = Random.Range(-6, 6);
      Instantiate(objects[randomValue], new Vector2(randomX, randomY), Quaternion.identity);
    }
}