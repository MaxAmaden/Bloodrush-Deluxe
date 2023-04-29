using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FitCanvasToScreenRatio : MonoBehaviour
{
    private Vector2Int screenSize;

    private RectTransform rect;

    private void Start()
    {
        rect = transform as RectTransform;

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
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1920f);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080f);
    }
}
