using UnityEngine;

public class WindmillFanMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Speed of rotation
    public Transform player;           // Reference to the player
    public float activationDistance = 30f; // Distance at which the fan starts rotating

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // If the player is within range, rotate the fan
            if (distance <= activationDistance)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
