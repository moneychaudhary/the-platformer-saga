using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    public float disappearDelay = 3f; // Time in seconds before the platform disappears
    public float reappearDelay = 2f;
    private bool playerOnPlatform = false;
    private bool visiblePlatform = true;
    private Transform childTransform;

    private void Start()
    {
        // Find the child GameObject with the specified name
        childTransform = transform.Find("Platform");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // disappearDelay -= Time.deltaTime;

        if (collision.gameObject.name == "Player")
        {
            disappearDelay -= Time.deltaTime;
            Debug.Log(disappearDelay);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (visiblePlatform)
        {
            disappearDelay = 3f;
        }
        else
        {
            reappearDelay -= Time.deltaTime;
            if (reappearDelay <= 0)
            {
                collision.gameObject.SetActive(true);
            }
        }
    }
}
