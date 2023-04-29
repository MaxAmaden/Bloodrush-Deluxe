using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCameraZoom : MonoBehaviour
{
    public float intendedBaseZoom = 5f;

    Camera cam;
    CameraController_Zoom zoomManager;
    FitCameraToScreenRatio fitCamera;

    private void Awake()
    {
        cam = Camera.main;
        zoomManager = cam.GetComponent<CameraController_Zoom>();
        fitCamera = cam.GetComponent<FitCameraToScreenRatio>();
    }

    private void LateUpdate()
    {
        if (Camera.main != cam)
        {
            cam = Camera.main;
            zoomManager = cam.GetComponent<CameraController_Zoom>();
            fitCamera = cam.GetComponent<FitCameraToScreenRatio>();
        }

        transform.localScale = Vector3.one * cam.orthographicSize / zoomManager.GetModifier("RatioScaling").value * (fitCamera.baseCameraZoom / intendedBaseZoom);
    }
}
