using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController_Zoom))]
[RequireComponent(typeof(CameraController_Offset))]
public class CameraController : MonoBehaviour
{
    public Transform target;

    [HideInInspector] public Vector3 offset;
    [HideInInspector] public Vector2 shake;

    [HideInInspector] public List<Transform> objectsAttachedToCamera = new List<Transform>();

    [HideInInspector] public CameraController_Zoom zoomController;
    [HideInInspector] public CameraController_Offset offsetController;

    private void Awake()
    {
        zoomController = GetComponent<CameraController_Zoom>();
        offsetController = GetComponent<CameraController_Offset>();

        if (Statics.CameraController != null) Debug.LogError("[!] There cannot be two CameraControllers at the same time!");
        else Statics.CameraController = this;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Vector3.zero;
        if (target != null) targetPosition = (Vector2)target.position;

        transform.position = targetPosition + offset + (Vector3)shake;

        foreach (Transform objectAttachedToCamera in objectsAttachedToCamera)
        {
            objectAttachedToCamera.position = transform.position;
            objectAttachedToCamera.rotation = transform.rotation;
        }
    }
}
