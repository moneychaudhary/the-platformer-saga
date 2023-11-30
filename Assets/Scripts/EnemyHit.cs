using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnemyHit : MonoBehaviour
{
    public EnemyHealthBar enemyHealthBar;
    public float damage = 0.1f;
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
                enemyHealthBar.Damage(damage);
            } else if (enemyColor == Color.blue)
            {
                enemyHealthBar.Damage(damage/2);
            }
            
            if(Math.Round(bulletColor.r, 2) == 0.68 && Math.Round(bulletColor.g, 2) == 0.85 && Math.Round(bulletColor.b, 2) == 0.9 && Math.Round(bulletColor.a, 2) == 1.0) {
                Debug.Log("Freeze!");
                if(enemyColor != Color.blue) {
                    StartCoroutine(Freeze(enemyColor));
                }
            } else if(Math.Round(bulletColor.r, 2) == 1.0 && Math.Round(bulletColor.g, 2) == 0.65 && Math.Round(bulletColor.b, 2) == 0 && Math.Round(bulletColor.a, 2) == 1.0) {
                Debug.Log("Double Damage!");
                enemyHealthBar.Damage(2 * damage);
            } else if(Math.Round(bulletColor.r, 2) == 0.80 && Math.Round(bulletColor.g, 2) == 1.0 && Math.Round(bulletColor.b, 2) == 0.0 && Math.Round(bulletColor.a, 2) == 1.0) {
                Debug.Log("One HIT!");
                enemyHealthBar.Damage(1.0f);
            }
            
            Destroy(collision.gameObject);

        }
    }

    private IEnumerator Freeze(Color enemyColor) {
        GetComponent<EnemyMoveUpDown>().enabled = false;
        GetComponent<EnemyBulletMovement>().enabled = false;
        GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(7f);
        GetComponent<EnemyMoveUpDown>().enabled = true;
        GetComponent<EnemyBulletMovement>().enabled = true;
        Scene scene = SceneManager.GetActiveScene();
        GetComponent<Renderer>().material.color = enemyColor;
    }
    
}
