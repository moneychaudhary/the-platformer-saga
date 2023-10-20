using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthBar : MonoBehaviour
{
    private RectTransform healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        UpdateHealthBar(Health.playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage) {
        if(Health.playerHealth - damage >= 0f) {
            Health.playerHealth -= damage;
        } else {
            Health.playerHealth = 0f;
        }
        
        if(Health.playerHealth == 0f) {
            Debug.Log("Player died!");
            SceneManager.LoadScene("GameOver");
        }

        UpdateHealthBar(Health.playerHealth);
    }

    public void UpdateHealthBar(float size) {
        healthBar.localScale = new Vector3(size, 1f);
    }
}
