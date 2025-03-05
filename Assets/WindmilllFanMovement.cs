using UnityEngine;

public class WindmilllFanMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the windmill fan continuously
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
