using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FitCameraToScreenRatio : MonoBehaviour
{
    public float baseCameraZoom = 5f;

    private Vector2Int screenSize;

    private CameraController_Zoom zoomManager;

    private void Start()
    {
        zoomManager = Camera.main.GetComponent<CameraController_Zoom>();

        screenSize = new Vector2Int(Screen.width, Screen.height);
        UpdateScaling();
    }

    private void Update()
    {
        if (Screen.width != screenSize.x || Screen.height != screenSize.y)
        {
            screenSize = new Vector2Int(Screen.width, Screen.height);
            UpdateScaling();
        }
    }

    private void UpdateScaling()
    {
        float intendedScreenHeight = screenSize.x / 1920f * 1080f;
        if (screenSize.y > intendedScreenHeight) Statics.screenWidthAdjustment = 1f / (screenSize.y / intendedScreenHeight);
        else Statics.screenWidthAdjustment = 1f;

        zoomManager.RemoveModifier_Static("RatioScaling", false);
        zoomManager.AddModifier_Static(new CameraController_Zoom_Modifier("RatioScaling", CameraZoom_Type.additive, baseCameraZoom / Statics.screenWidthAdjustment));
    }
}