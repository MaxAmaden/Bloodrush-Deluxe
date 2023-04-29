using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation_Glitch : RotationAnimation
{
    public Vector3 maxGlitchRotation;
    public Vector2 glitchDelay;
    public Vector2 glitchDuration;

    Vector3 originalRotation;

    private void Start()
    {
        originalRotation = transform.localRotation.eulerAngles;

        StartCoroutine(GlitchUpdate());
    }

    private IEnumerator GlitchUpdate()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(glitchDelay.x, glitchDelay.y));

            transform.localRotation = Quaternion.Euler(originalRotation.x + Random.Range(-maxGlitchRotation.x, maxGlitchRotation.x), originalRotation.y + Random.Range(-maxGlitchRotation.y, maxGlitchRotation.y), originalRotation.z + Random.Range(-maxGlitchRotation.z, maxGlitchRotation.z));

            yield return new WaitForSecondsRealtime(Random.Range(glitchDuration.x, glitchDuration.y));

            transform.localRotation = Quaternion.Euler(originalRotation);
        }
    }
}