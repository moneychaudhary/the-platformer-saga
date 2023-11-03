using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject [] objects;
    private GameObject lastObject;
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
            yield return new WaitForSeconds(Random.Range(4, 8));
            if (!lastObject) {
                SpawnObject();
            }
            else {
                Destroy(lastObject);
            }
        }
    }

    void SpawnObject()
    {
      int randomValue = Random.Range(0, objects.Length);
      int randomX = Random.Range(-26, 8);
      int randomY = Random.Range(-12, 12);
      lastObject = Instantiate(objects[randomValue], new Vector2(randomX, randomY), Quaternion.identity);
    }
}