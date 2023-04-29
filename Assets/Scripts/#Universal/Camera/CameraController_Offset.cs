using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController_Offset : MonoBehaviour
{
    public Vector3 baseOffset = new Vector3(0f, 0f, -10f);

    List<CameraController_Offset_Modifier> offsetModifiers_Static = new List<CameraController_Offset_Modifier>();
    List<CameraController_Offset_Modifier> offsetModifiers_OneFrame = new List<CameraController_Offset_Modifier>();

    CameraController cc;

    Coroutine lookAtThread = null;

    private void Awake()
    {
        cc = Camera.main.GetComponent<CameraController>();
    }

    private void Start()
    {
        AddModifier_Static(new CameraController_Offset_Modifier("BaseOffset", baseOffset));
    }


    public void AddModifier_Static(CameraController_Offset_Modifier newModifier)
    {
        offsetModifiers_Static.Add(newModifier);
        UpdateOffset();
    }

    public void RemoveModifier_Static(string identifier, bool failIsError = true)
    {
        CameraController_Offset_Modifier toRemove = GetModifier(identifier);

        if (toRemove != null) offsetModifiers_Static.Remove(toRemove);
        else if (failIsError) Debug.LogError("No static zoom modifier with ID '" + identifier + "' found!");
    }

    public CameraController_Offset_Modifier GetModifier(string identifier)
    {
        foreach (CameraController_Offset_Modifier zoomModifier in offsetModifiers_Static)
        {
            if (zoomModifier.identifier == identifier) return zoomModifier;
        }

        return null;
    }


    public void AddModifier_OneFrame(CameraController_Offset_Modifier newModifier)
    {
        offsetModifiers_OneFrame.Add(newModifier);
        UpdateOffset();
    }


    private void UpdateOffset()
    {
        Vector3 finalOffset = Vector3.zero;

        // Add offset modifiers together.
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_OneFrame) finalOffset += offsetModifier.value;
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_Static) finalOffset += offsetModifier.value;

        // Clear 'OneFrame' modifiers.
        offsetModifiers_OneFrame = new List<CameraController_Offset_Modifier>();

        // If zoom has changed, then apply new zoom!
        cc.offset = finalOffset;
    }


    public Coroutine LookAt(Transform target, float timeToComplete, Curves.Curve curve = Curves.Curve.FastStartSlowEnd)
    {
        if (lookAtThread != null) StopCoroutine(lookAtThread);
        lookAtThread = StartCoroutine(_LookAt(target, timeToComplete, curve));

        return lookAtThread;
    }
    private IEnumerator _LookAt(Transform target, float timeToComplete, Curves.Curve curveType)
    {
        Vector2 startOffset = cc.offset;
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_OneFrame) startOffset -= (Vector2)offsetModifier.value;
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_Static) startOffset -= (Vector2)offsetModifier.value;

        float timer = 0f;
        AnimationCurve curve = Curves.GetCurve(curveType);
        while (timer < timeToComplete)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, timeToComplete);

            Vector2 targetOffset = target.position - cc.target.position;
            AddModifier_OneFrame(new CameraController_Offset_Modifier("AUTO_LookAt", Vector2.LerpUnclamped(startOffset, targetOffset, curve.Evaluate(timer / timeToComplete))));

            yield return null;
        }

        lookAtThread = StartCoroutine(_MaintainLookAt(target));
    }
    private IEnumerator _MaintainLookAt(Transform target)
    {
        while (true)
        {
            AddModifier_OneFrame(new CameraController_Offset_Modifier("AUTO_LookAt", target.position - cc.target.position));
            yield return null;
        }
    }

    public Coroutine StopLookAt(float timeToComplete, Curves.Curve curve = Curves.Curve.FastStartSlowEnd)
    {
        if (lookAtThread != null) StopCoroutine(lookAtThread);
        lookAtThread = StartCoroutine(_StopLookAt(timeToComplete, curve));

        return lookAtThread;
    }
    private IEnumerator _StopLookAt(float timeToComplete, Curves.Curve curveType)
    {
        Vector2 startOffset = cc.offset;
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_OneFrame) startOffset -= (Vector2)offsetModifier.value;
        foreach (CameraController_Offset_Modifier offsetModifier in offsetModifiers_Static) startOffset -= (Vector2)offsetModifier.value;

        if (startOffset == Vector2.zero) yield break;

        float timer = 0f;
        AnimationCurve curve = Curves.GetCurve(curveType);
        while (timer < timeToComplete)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, timeToComplete);

            AddModifier_OneFrame(new CameraController_Offset_Modifier("AUTO_LookAt", Vector2.LerpUnclamped(startOffset, Vector2.zero, curve.Evaluate(timer / timeToComplete))));

            yield return null;
        }

        UpdateOffset();
    }
}

public class CameraController_Offset_Modifier
{
    public string identifier;
    public Vector3 value;

    public CameraController_Offset_Modifier(string identifier, Vector3 value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
