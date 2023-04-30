using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardUI : MonoBehaviour
{
    public Transform RHand;
    public Transform RHand_ShowTransform;
    public Transform RHand_HideTransform;

    [Space]
    public Transform RHand_HeldPackage;
    public Transform RHand_HeldPackage_ShowTransform;
    public Transform RHand_HeldPackage_HideTransform;
    public Transform RHand_HeldPackage_PreThrowTransform;
    public Transform RHand_HeldPackage_PostThrowTransform;

    Coroutine RHandAnimThread = null;

    public Coroutine Anim_HoldPackage()
    {
        if (RHandAnimThread != null) StopCoroutine(RHandAnimThread);
        return RHandAnimThread = StartCoroutine(HoldPackage());

        IEnumerator HoldPackage()
        {
            yield return Animate_LocalTransformLerp(0.1f, RHand, RHand_ShowTransform, RHand_HideTransform, Curves.GetCurve(Curves.Curve.SlowStartFastEnd), true);

            yield return new WaitForSecondsRealtime(0.1f);

            yield return Animate_LocalTransformLerp(0.25f, RHand_HeldPackage, RHand_HeldPackage_HideTransform, RHand_HeldPackage_ShowTransform, Curves.GetCurve(Curves.Curve.Overshoot_Small), true);
        }
    }
    public Coroutine Anim_HidePackage()
    {
        if (RHandAnimThread != null) StopCoroutine(RHandAnimThread);
        return RHandAnimThread = StartCoroutine(HidePackage());

        IEnumerator HidePackage()
        {
            yield return Animate_LocalTransformLerp(0.3f, RHand_HeldPackage, RHand_HeldPackage_ShowTransform, RHand_HeldPackage_HideTransform, Curves.GetCurve(Curves.Curve.SlowStartFastEnd), true);

            yield return new WaitForSecondsRealtime(0.2f);

            yield return Animate_LocalTransformLerp(0.3f, RHand, RHand_HideTransform, RHand_ShowTransform, Curves.GetCurve(Curves.Curve.Overshoot_Small), true);
        }
    }
    public Coroutine Anim_PreThrowPackage()
    {
        if (RHandAnimThread != null) StopCoroutine(RHandAnimThread);
        return RHandAnimThread = StartCoroutine(PreThrowPackage());

        IEnumerator PreThrowPackage()
        {
            yield return Animate_LocalTransformLerp(0.3f, RHand_HeldPackage, RHand_HeldPackage_ShowTransform, RHand_HeldPackage_PreThrowTransform, Curves.GetCurve(Curves.Curve.SlowStartFastEnd), true);
        }
    }
    public Coroutine Anim_CancelThrowPackage()
    {
        if (RHandAnimThread != null) StopCoroutine(RHandAnimThread);
        return RHandAnimThread = StartCoroutine(CancelThrowPackage());

        IEnumerator CancelThrowPackage()
        {
            yield return Animate_LocalTransformLerp(0.15f, RHand_HeldPackage, RHand_HeldPackage, RHand_HeldPackage_ShowTransform, Curves.GetCurve(Curves.Curve.SlowStartFastEnd), true);
        }
    }
    public Coroutine Anim_ThrowPackage()
    {
        if (RHandAnimThread != null) StopCoroutine(RHandAnimThread);
        return RHandAnimThread = StartCoroutine(ThrowPackage());

        IEnumerator ThrowPackage()
        {
            yield return Animate_LocalTransformLerp(0.15f, RHand_HeldPackage, RHand_HeldPackage_PreThrowTransform, RHand_HeldPackage_PostThrowTransform, Curves.GetCurve(Curves.Curve.SlowStartFastEnd), true);

            yield return new WaitForSecondsRealtime(0.1f);

            yield return Animate_LocalTransformLerp(0.2f, RHand_HeldPackage, RHand_HeldPackage_HideTransform, RHand_HeldPackage_ShowTransform, Curves.GetCurve(Curves.Curve.Overshoot_Small), true);
        }
    }

    IEnumerator Animate_LocalTransformLerp(float duration, Transform target, Transform start, Transform end, AnimationCurve curve, bool unscaledTime = false)
    {
        float prog = 0f;
        float speed = 1f / duration;
        while (prog < 1f)
        {
            if (unscaledTime) prog = Mathf.Clamp01(prog + Time.unscaledDeltaTime * speed);
            else prog = Mathf.Clamp01(prog + Time.deltaTime * speed);

            target.localPosition = Vector3.LerpUnclamped(start.localPosition, end.localPosition, curve.Evaluate(prog));
            target.localRotation = Quaternion.LerpUnclamped(start.localRotation, end.localRotation, curve.Evaluate(prog));
            target.localScale = Vector3.LerpUnclamped(start.localScale, end.localScale, curve.Evaluate(prog));

            yield return null;
        }
    }
}
