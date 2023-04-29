using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimation_Circle : PositionAnimation
{
    public float secondsToCompleteRevolution;
    public float radius;

    [Space]
    public bool randomStart = false;
    public bool randomDirection = false;

    float t = 0;
    float speed;

    Vector2 originalPosition;

    void Start()
    {
        speed = (2 * Mathf.PI) / secondsToCompleteRevolution;

        originalPosition = transform.localPosition;

        if (randomStart) t = Random.Range(0, 2 * Mathf.PI);
        if (randomDirection && Random.value >= 0.5f) secondsToCompleteRevolution *= -1f;
    }

    void Update()
    {
        t += speed * Time.deltaTime;
        transform.localPosition = originalPosition + new Vector2(Mathf.Cos(t) * radius, Mathf.Sin(t) * radius);
    }
}
