using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToCamera : MonoBehaviour
{
    Camera currentCamera = null;

    private void Update()
    {
        // Every frame, check if the main camera has changed.
        if (currentCamera != Camera.main)
        {
            // Get the new main camera.
            currentCamera = Camera.main;

            // Check if it has a CameraController, attaching to it if possible.
            CameraController cameraController = currentCamera.GetComponent<CameraController>();
            if (cameraController == null) Debug.LogError("[!] Cannot attach to camera! The main camera does not have a CameraController component!");
            else cameraController.objectsAttachedToCamera.Add(transform);
        }
    }
}
