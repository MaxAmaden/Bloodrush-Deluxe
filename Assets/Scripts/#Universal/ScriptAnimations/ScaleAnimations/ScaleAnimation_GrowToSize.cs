using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation_GrowToSize : MonoBehaviour
{
    public float timeToGrow = 1f;
    public Vector3 growToScale = Vector3.one;

    Vector3 startScale;

    Coroutine animationThread = null;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    public void Grow()
    {
        if (animationThread != null) StopCoroutine(animationThread);
        animationThread = StartCoroutine(Go(growToScale));
    }

    public void Shrink()
    {
        if (animationThread != null) StopCoroutine(animationThread);
        animationThread = StartCoroutine(Go(startScale));
    }

    private IEnumerator Go(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;

        float prog = 0f;
        while (prog < 1f)
        {
            prog += Time.deltaTime * (1f / timeToGrow);

            transform.localScale = Vector3.Lerp(startScale, targetScale, prog);

            yield return null;
        }
    }
}
