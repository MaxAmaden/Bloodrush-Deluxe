using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation_ColourPulse : SpriteAnimation
{
    public Color pulseColor;
    public float pulseSpeed;

    [Space]
    public bool unscaledTime;

    Color startColor;

    SpriteRenderer sr;

    float timer = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    private void Update()
    {
        if (unscaledTime) timer += Time.unscaledDeltaTime * pulseSpeed;
        else timer += Time.deltaTime * pulseSpeed;

        sr.color = Color.Lerp(startColor, pulseColor, Mathf.Sin(timer));
    }
}