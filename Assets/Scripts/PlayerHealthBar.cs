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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar(Health.playerHealth);
    }

    public void Damage(float damage) {
        if(Health.playerHealth - damage >= 0f) {
            Health.playerHealth -= damage;
        } else {
            Health.playerHealth = 0f;
        }
        
        if(Health.playerHealth == 0f) {

            GameObject analytics = GameObject.Find("Analytics");
            GameObject player = GameObject.Find("Player");
            GameObject platform = GameObject.Find("Color Platform");

            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Level 1")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1642877412", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                }
            }
            else if (scene.name == "Level 2")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1606956756", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                }
            }
            else if (scene.name == "Level 3")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1606956756", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                }
            }
            else if (scene.name == "Level 4")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.517618210", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1784052201", (int)analytics.GetComponent<GoogleFormUploader>().dissapearCount);
                }
            }
            else if (scene.name == "Level 5")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.813673215", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1784052201", (int)analytics.GetComponent<GoogleFormUploader>().dissapearCount);
                }
            }
            else if (scene.name == "Home")
            {

                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1984991129", (int)player.GetComponent<PlayerAimHand>().emptyBulletCount);
                    analytics.GetComponent<GoogleFormUploader>().RecordData("entry.1784052201", (int)analytics.GetComponent<GoogleFormUploader>().dissapearCount); ;
                }

            }

            Debug.Log("Player died!");

            SceneManager.LoadScene("GameOver");
        }

        UpdateHealthBar(Health.playerHealth);
    }

    public void UpdateHealthBar(float size) {
        healthBar.localScale = new Vector3(size, 1f);
    }
}
