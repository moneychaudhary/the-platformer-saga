using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
   [SerializeField] private GameObject[] points;
   private int index = 0;

   [SerializeField] private float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Vector2.Distance(
                points[index].transform.position,
                transform.position
            ) < 0.1f
        )
        {
            index ++;
            if (index >= points.Length) {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].transform.position, Time.deltaTime * speed);
    }
}
