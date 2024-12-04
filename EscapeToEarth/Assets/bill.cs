using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    void LateUpdate()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }

        // Make the health bar face the camera
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
