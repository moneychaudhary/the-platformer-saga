using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Text bulletText;
    public Text healthText;
    void Start()
    {
        StartCoroutine(PlayerHealth());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HealthReload"))
        {
            healthText.text = "";
        }

        if (collision.gameObject.CompareTag("BulletReload"))
        {
            bulletText.text = "";
        }
    }

    IEnumerator PlayerHealth()
    {
        yield return new WaitForSeconds(1f);
        Health.playerHealth = 0.5f;
    }
}
