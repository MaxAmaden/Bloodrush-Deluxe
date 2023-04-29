using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation_GrowShrink : ScaleAnimation
{
    public Vector2 scaleMinMax = new Vector2(0.75f, 1.25f);
    public float speed;

    [Space]
    public bool randomStart;

    float t = 0;

    void Start()
    {
        if (randomStart) t = Random.Range(0, 50);
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        transform.localScale = Vector3.one * (((Mathf.Sin(t) + 1) / 2 * (scaleMinMax.y - scaleMinMax.x)) + scaleMinMax.x);
    }
}
