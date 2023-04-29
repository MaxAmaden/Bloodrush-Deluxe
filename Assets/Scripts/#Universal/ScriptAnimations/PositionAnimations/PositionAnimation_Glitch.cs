using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimation_Glitch : PositionAnimation
{
    public Vector2 glitchMagnitude;
    public Vector2 glitchDelay;
    public Vector2 glitchDuration;

    Vector2 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;

        StartCoroutine(GlitchUpdate());
    }

    private IEnumerator GlitchUpdate()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(glitchDelay.x, glitchDelay.y));

            transform.localPosition = originalPosition + Random.insideUnitCircle * Random.Range(glitchMagnitude.x, glitchMagnitude.y);

            yield return new WaitForSecondsRealtime(Random.Range(glitchDuration.x, glitchDuration.y));

            transform.localPosition = originalPosition;
        }
    }
}