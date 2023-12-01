using UnityEngine;

public class SceneDuplicator : MonoBehaviour
{
    public GameObject platformPrefab;
    private bool hasPlayerCollided = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player") && !hasPlayerCollided)
        {
            // Load the prefab on top of the trigger object
            Instantiate(platformPrefab, transform.position + new Vector3(16.6f, 12.4f, 0), Quaternion.identity);

            // Set the flag to true to prevent further spawning
            hasPlayerCollided = true;
        }
    }
}

