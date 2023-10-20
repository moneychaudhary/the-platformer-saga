using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public EnemyHealthBar enemyHealthBar;
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
            Color bulletColor = collision.gameObject.GetComponent<Renderer>().material.color;
            Color enemyColor = transform.GetComponent<Renderer>().material.color;
            if (bulletColor == enemyColor)
            {
                enemyHealthBar.Damage(0.1f);
            }
            Destroy(collision.gameObject);

        }
    }

    
}
