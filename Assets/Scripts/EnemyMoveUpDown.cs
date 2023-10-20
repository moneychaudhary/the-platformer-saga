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
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Sin(Time.time + freq) * amp);
    }
}
