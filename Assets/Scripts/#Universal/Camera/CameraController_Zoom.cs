using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController_Zoom : MonoBehaviour
{
    List<CameraController_Zoom_Modifier> zoomModifiers_Static = new List<CameraController_Zoom_Modifier>();
    List<CameraController_Zoom_Modifier> zoomModifiers_OneFrame = new List<CameraController_Zoom_Modifier>();

    Camera cam;

    Coroutine applyZoom_Thread = null;
    float applyZoom_Value = 1f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void AddModifier_Static(CameraController_Zoom_Modifier newModifier)
    {
        zoomModifiers_Static.Add(newModifier);
        UpdateZoom();
    }
    public void RemoveModifier_Static(string identifier, bool failIsError = true)
    {
        CameraController_Zoom_Modifier toRemove = GetModifier(identifier);

        if (toRemove != null) zoomModifiers_Static.Remove(toRemove);
        else if (failIsError) Debug.LogError("No static zoom modifier with ID '" + identifier + "' found!");
    }

    public CameraController_Zoom_Modifier GetModifier(string identifier)
    {
        foreach (CameraController_Zoom_Modifier zoomModifier in zoomModifiers_Static)
        {
            if (zoomModifier.identifier == identifier) return zoomModifier;
        }

        return null;
    }

    public void AddModifier_OneFrame(CameraController_Zoom_Modifier newModifier)
    {
        zoomModifiers_OneFrame.Add(newModifier);
        UpdateZoom();
    }


    private void UpdateZoom()
    {
        float finalZoom = 0;

        // Add additive zoom modifiers first.
        foreach (CameraController_Zoom_Modifier zoomModifier in zoomModifiers_OneFrame)
        {
            if (zoomModifier.type == CameraZoom_Type.additive) finalZoom += zoomModifier.value;
        }
        foreach (CameraController_Zoom_Modifier zoomModifier in zoomModifiers_Static)
        {
            if (zoomModifier.type == CameraZoom_Type.additive) finalZoom += zoomModifier.value;
        }

        // Then multiply by multiplicative zoom modifiers.
        foreach (CameraController_Zoom_Modifier zoomModifier in zoomModifiers_OneFrame)
        {
            if (zoomModifier.type == CameraZoom_Type.multiplicative) finalZoom *= zoomModifier.value;
        }
        foreach (CameraController_Zoom_Modifier zoomModifier in zoomModifiers_Static)
        {
            if (zoomModifier.type == CameraZoom_Type.multiplicative) finalZoom *= zoomModifier.value;
        }

        // Clear 'OneFrame' modifiers.
        zoomModifiers_OneFrame = new List<CameraController_Zoom_Modifier>();

        // If zoom has changed, then apply new zoom!
        if (cam.orthographicSize != finalZoom) cam.orthographicSize = finalZoom;
    }


    public Coroutine ApplyZoom(float zoomMultiplier, float timeToComplete, Curves.Curve curve = Curves.Curve.FastStartSlowEnd)
    {
        // Invert zoom multiplier (so that lower than 1 zooms in and higher than 1 zooms out - as should be intuitive).
        zoomMultiplier = 1f / zoomMultiplier;

        if (applyZoom_Thread != null) StopCoroutine(applyZoom_Thread);
        applyZoom_Thread = StartCoroutine(_ApplyZoom(zoomMultiplier, timeToComplete, curve));

        return applyZoom_Thread;
    }
    private IEnumerator _ApplyZoom(float zoomMultiplier, float timeToComplete, Curves.Curve curveType)
    {
        RemoveModifier_Static("AUTO_ApplyZoom", false);

        float startZoom = applyZoom_Value;

        float timer = 0f;
        AnimationCurve curve = Curves.GetCurve(curveType);
        while (timer < timeToComplete)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, timeToComplete);

            applyZoom_Value = Mathf.LerpUnclamped(startZoom, zoomMultiplier, curve.Evaluate(timer / timeToComplete));
            AddModifier_OneFrame(new CameraController_Zoom_Modifier("AUTO_ApplyZoom", CameraZoom_Type.multiplicative, applyZoom_Value));

            yield return null;
        }

        if (zoomMultiplier != 1f) AddModifier_Static(new CameraController_Zoom_Modifier("AUTO_ApplyZoom", CameraZoom_Type.multiplicative, zoomMultiplier));

        applyZoom_Thread = null;
    }

    public Coroutine StopApplyZoom(float timeToComplete, Curves.Curve curve = Curves.Curve.FastStartSlowEnd)
    {
        if (applyZoom_Thread != null) StopCoroutine(applyZoom_Thread);
        applyZoom_Thread = StartCoroutine(_StopApplyZoom(timeToComplete, curve));

        return applyZoom_Thread;
    }
    private IEnumerator _StopApplyZoom(float timeToComplete, Curves.Curve curveType)
    {
        RemoveModifier_Static("AUTO_ApplyZoom", false);

        float startZoom = applyZoom_Value;

        float timer = 0f;
        AnimationCurve curve = Curves.GetCurve(curveType);
        while (timer < timeToComplete)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, timeToComplete);

            applyZoom_Value = Mathf.LerpUnclamped(startZoom, 1f, curve.Evaluate(timer / timeToComplete));
            AddModifier_OneFrame(new CameraController_Zoom_Modifier("AUTO_ApplyZoom", CameraZoom_Type.multiplicative, applyZoom_Value));

            yield return null;
        }

        UpdateZoom();
        applyZoom_Thread = null;
    }
}


    [System.Serializable]
public class CameraController_Zoom_Modifier
{
    public string identifier;
    public CameraZoom_Type type;
    public float value;

    public CameraController_Zoom_Modifier(string identifier, CameraZoom_Type type = CameraZoom_Type.additive, float value = 0)
    {
        this.identifier = identifier;
        this.type = type;
        this.value = value;
    }
}

public enum CameraZoom_Type { additive, multiplicative }