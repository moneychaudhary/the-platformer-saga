using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Adjust the smoothness of the camera follow

    void LateUpdate()
    {
        if (target != null)
        {
            // Only follow the player's y-position
            float desiredY = target.position.y;
            float smoothedY = Mathf.Lerp(transform.position.y, desiredY, smoothSpeed);

            // Maintain the camera's x-position
            float currentX = transform.position.x;

            // Set the new camera position
            transform.position = new Vector3(currentX, smoothedY, transform.position.z);
        }
    }
}
