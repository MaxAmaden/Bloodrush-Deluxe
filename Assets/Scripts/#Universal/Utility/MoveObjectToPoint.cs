using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToPoint : MonoBehaviour
{
    public bool targetIsLocal;
    public AnimationCurve curve;

    Coroutine moveRoutine = null;


    public void Move(Vector3 targetPoint, float timeToReachPoint)
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(Move_(targetPoint, timeToReachPoint));
    }

    private IEnumerator Move_(Vector3 targetPoint, float timeToReachPoint)
    {
        Vector3 startPosition = transform.position;
        if (targetIsLocal) startPosition = transform.localPosition;

        float speed = 1f / timeToReachPoint;
        float prog = 0;
        while (prog < 1)
        {
            prog = Mathf.Clamp01(prog + Time.deltaTime * speed);

            if (targetIsLocal) transform.localPosition = Vector3.LerpUnclamped(startPosition, targetPoint, curve.Evaluate(prog));
            else transform.position = Vector3.LerpUnclamped(startPosition, targetPoint, curve.Evaluate(prog));

            yield return new WaitForEndOfFrame();
        }
    }
}
