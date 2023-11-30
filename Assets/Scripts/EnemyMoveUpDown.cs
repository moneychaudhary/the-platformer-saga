using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveUpDown : MonoBehaviour
{
    [SerializeField] private float freq = 4f;
    [SerializeField] private float amp = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Health.enemyHealth = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float newYPosition = Mathf.Sin(Time.time + freq) * amp;
        GameObject lava = GameObject.FindWithTag("LavaScalePoint");
       
        if (lava)
        {
            float lavaYPosition = lava.transform.position.y + 3.5f;
            if (newYPosition < lavaYPosition)
            {
                Debug.Log("hello");
                newYPosition = lavaYPosition;
            }
        }
        
        transform.position = new Vector2(transform.position.x, newYPosition);
    }
}
