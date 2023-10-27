using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealthBar : MonoBehaviour
{
    private RectTransform healthBar;
    private float levelStartTime;
    private float enemyDeathTime;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        levelStartTime = Time.time;
        enemyDeathTime = -1f; // Set to -1 to indicate that the enemy has not died yet
        UpdateHealthBar(Health.enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDeathTime < 0f)
        {
            // If the enemy hasn't died yet, calculate the time since the level started
            elapsedTime = Time.time - levelStartTime;
            //Debug.Log("Time elapsed: " + elapsedTime);
        }
    }

    public void Damage(float damage)
    {
        if (enemyDeathTime < 0f)
        {
            if (Health.enemyHealth - damage >= 0f)
            {
                Health.enemyHealth -= damage;
            }
            else
            {
                GameObject analytics = GameObject.Find("Analytics");
            Debug.Log("Enemy died!");
            Health.enemyHealth = 1f;
            Scene scene = SceneManager.GetActiveScene();
                    Debug.Log(scene.name);
            if (scene.name == "Tutorisl-Level2")
            {
                SceneManager.LoadScene("Tutorial-Level3");
            }
            if (scene.name == "Tutorial-Level3")
            {
                SceneManager.LoadScene("Start");
            }
            if (scene.name == "Level 1") {

                 Debug.Log("Level1 Time" + elapsedTime);

                 if (analytics)
                 {
                   analytics.GetComponent<GoogleFormUploader>().RecordData("entry.2006131830", (int)elapsedTime);
                 }
                 SceneManager.LoadScene("NextLevel2");
            }
            if (scene.name == "Level 2") {

                        if (analytics)
                        {
                            analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1328253810", (int)elapsedTime);
                        }
                        SceneManager.LoadScene("NextLevel3");
            }
            if (scene.name == "Home") {

                        Debug.Log("Level3 Time " + elapsedTime);

                        if (analytics)
                        {
                            analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1883594124", (int)elapsedTime);
                        }

                        SceneManager.LoadScene("Finish");
            }
                Health.enemyHealth = 1f;
                enemyDeathTime = Time.time;
                Debug.Log("Enemy died! Time to death: " + (enemyDeathTime - levelStartTime) + " seconds");
            }
            UpdateHealthBar(Health.enemyHealth);
        }
    }

    public void UpdateHealthBar(float size)
    {
        healthBar.localScale = new Vector3(size, 1f);
    }
}

