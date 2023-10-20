using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
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

    public void Damage(float damage) {
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
