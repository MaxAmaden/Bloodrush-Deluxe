using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation_FadeSprite : SpriteAnimation
{
    public float fadeTime;

    float timer = 0;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer = Mathf.Clamp(timer + Time.deltaTime, 0, fadeTime);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - (timer / fadeTime));

        if (timer > fadeTime) Destroy(this);
    }
}
