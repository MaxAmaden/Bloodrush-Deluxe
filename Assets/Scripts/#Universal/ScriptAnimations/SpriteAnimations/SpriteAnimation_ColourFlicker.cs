using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation_ColourFlicker : SpriteAnimation
{
    public Color colorA;
    public Color colorB;

    [Space]
    public Vector2 flickerDelayRange = new Vector2(1f, 2f);

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        while (true)
        {
            Color startColor = sr.color;
            Color endColor = Color.Lerp(colorA, colorB, Random.value);

            float duration = Random.Range(flickerDelayRange.x, flickerDelayRange.y);
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                sr.color = Color.Lerp(startColor, endColor, timer / duration);

                yield return null;
            }
        }
    }
}