using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 45.0f; // Degrees per second

    void Update()
    {
        // Rotate the object around its Y axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
