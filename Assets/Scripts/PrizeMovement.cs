using UnityEngine;
using UnityEngine.SceneManagement;

public class PrizeMovement : MonoBehaviour
{
    public float horizontalSpeed = 2f;   // Speed of horizontal movement
    public float verticalSpeed = 2f;     // Speed of vertical movement
    public float amplitude = 1f;         // Amplitude of the curve

    private float timeCounter = 0f;

    void Update()
    {
        // Move left and right
        float horizontalMovement = Mathf.Sin(timeCounter) * amplitude;
        float verticalMovement = verticalSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0f));

        // Update time counter for the sine wave
        timeCounter += Time.deltaTime * horizontalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Finish");
        }
    }
}
