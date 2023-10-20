using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    private RectTransform healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        UpdateHealthBar(Health.enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log("Enemy hit!");
            Destroy(collision.gameObject);
            Damange(0.1f);
        }
    }

    public void Damange(float damage) {
        if(Health.enemyHealth - damage >= 0f) {
            Health.enemyHealth -= damage;
        } else {
            Health.enemyHealth = 0f;
        }

        UpdateHealthBar(Health.enemyHealth);
    }

    public void UpdateHealthBar(float size) {
        healthBar.localScale = new Vector3(size, 1f);
    }
}
