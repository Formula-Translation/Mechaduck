using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The object the camera will orbit around.
    public float rotationSpeed = 2.0f;
    public float distance = 5.0f;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, distance, 0); // Adjust the Y offset as needed.
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Calculate the desired camera position.
        Vector3 desiredPosition = target.position + offset;

        // Interpolate smoothly between the current position and the desired position.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Make the camera look at the target object.
        transform.LookAt(target);
    }
}
