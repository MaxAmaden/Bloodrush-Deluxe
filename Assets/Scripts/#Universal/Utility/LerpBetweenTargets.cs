using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBetweenTargets : MonoBehaviour
{
    public float timeToComplete;

    public Transform startTarget;
    public Transform endTarget;

    [Space]
    public bool playOnEnable;

    Transform start;
    Transform end;

    public void Go()
    {
        start = startTarget;
        end = endTarget;

        StopCoroutine("_Go");
        StartCoroutine("_Go");
    }

    public void GoInReverse()
    {
        start = endTarget;
        end = startTarget;

        StopCoroutine("_Go");
        StartCoroutine("_Go");
    }

    private IEnumerator _Go()
    {
        float timer = 0;
        while (timer < timeToComplete)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, timeToComplete);
            transform.position = Vector2.Lerp(start.position, end.position, timer / timeToComplete);

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnEnable()
    {
        Go();
    }
}
