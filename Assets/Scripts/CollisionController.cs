using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject childObject;
    public float disappearDelay = 3f;
    private bool isPlayerOnPlatform = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Hitting platform.");
            isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Staying on platform.");
            if (isPlayerOnPlatform)
            {
                disappearDelay -= Time.deltaTime;
                Debug.Log(disappearDelay);
                if (disappearDelay <= 0 && childObject != null)
                {
                    childObject.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Not hitting platform.");
            isPlayerOnPlatform = false;
            disappearDelay = 3f; // Reset the delay when the player exits the platform
        }
    }
}
