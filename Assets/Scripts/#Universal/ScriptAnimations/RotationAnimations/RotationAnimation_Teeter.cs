using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation_Teeter : RotationAnimation
{
    public float speed;
    public float tiltMagnitude;

    [Space]
    public bool randomiseStart;
    public bool startHalfway;
    public float startOffset = 0f;

    [Space]
    public float overrideBaseRotation;

    float prog = 0;

    Vector3 baseRotation;

    private void Awake()
    {
        if (randomiseStart) prog = Random.Range(0, 2 * Mathf.PI);
        else if (startHalfway) prog = Mathf.PI;

        prog += startOffset;
    }

    private void OnEnable()
    {
        if (overrideBaseRotation != 0) baseRotation = new Vector3(0, 0, overrideBaseRotation);

        overrideBaseRotation = 0;
    }

    private void Update()
    {
        prog += Time.deltaTime * speed;

        transform.localRotation = Quaternion.Euler(baseRotation + new Vector3(0, 0, Mathf.Sin(prog) * tiltMagnitude));
    }
}
