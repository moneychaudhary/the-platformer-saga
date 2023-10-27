using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if(Health.enemyHealth == 0f) {
            Debug.Log("Enemy died!");
            Health.enemyHealth = 1f;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Tutorisl-Level2")
            {
                SceneManager.LoadScene("Tutorial-Level3");
            }
            if (scene.name == "Tutorial-Level3")
            {
                SceneManager.LoadScene("Start");
            }
            if (scene.name == "Level 1") {
                SceneManager.LoadScene("NextLevel2");
            }
            if (scene.name == "Level 2") {
                SceneManager.LoadScene("NextLevel3");
            }
            if (scene.name == "Home") {
                SceneManager.LoadScene("Finish");
            }
        }

        UpdateHealthBar(Health.enemyHealth);
    }

    public void UpdateHealthBar(float size) {
        healthBar.localScale = new Vector3(size, 1f);
    }
}
