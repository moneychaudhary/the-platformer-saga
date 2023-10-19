using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log("Enemy hit!");
            Destroy(collision.gameObject);
        }
    }
}
