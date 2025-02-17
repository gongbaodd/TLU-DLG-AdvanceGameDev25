using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the cat
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Adjust for better view
    public float smoothSpeed = 2f; // Controls how smoothly the camera follows

    void LateUpdate()
    {
        if (target == null) return;

        // Compute the target camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between current and target position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make sure the camera keeps its rotation (45° x and y)
        transform.LookAt(target.position); // Adjust if needed for better angles
    }
}
