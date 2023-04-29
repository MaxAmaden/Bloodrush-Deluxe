using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimation_Shake : PositionAnimation
{
    public float noiseStrength;

    public bool toAddNoise = true;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (toAddNoise && transform.parent != null) transform.localPosition = (Vector2)(originalPos + Random.insideUnitSphere * noiseStrength);
        else
        {
            toAddNoise = false;
            transform.localPosition = originalPos;
        }
    }
}
