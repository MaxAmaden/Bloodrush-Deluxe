using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation_Glitch : ScaleAnimation
{
    public Vector3 maxGlitchScale;
    public Vector2 glitchDelay;
    public Vector2 glitchDuration;

    Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        StartCoroutine(GlitchUpdate());
    }

    private IEnumerator GlitchUpdate()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(glitchDelay.x, glitchDelay.y));

            transform.localScale = new Vector3(originalScale.x + Random.Range(-maxGlitchScale.x, maxGlitchScale.x), originalScale.y + Random.Range(-maxGlitchScale.y, maxGlitchScale.y), originalScale.z + Random.Range(-maxGlitchScale.z, maxGlitchScale.z));

            yield return new WaitForSecondsRealtime(Random.Range(glitchDuration.x, glitchDuration.y));

            transform.localScale = originalScale;
        }
    }
}
