using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation_Squash : MonoBehaviour
{
    public float bounce_Speed = 1f;
    public float bounce_Magnitude = 0.2f;

    [Space]
    public bool randomStart = false;

    float bounce_Prog = 0f;

    private void Start()
    {
        if (randomStart) bounce_Prog = Random.Range(0f, Mathf.PI);
    }

    private void Update()
    {
        // Bounce.
        bounce_Prog = (bounce_Prog + (Time.deltaTime * Mathf.PI * bounce_Speed)) % Mathf.PI;

        float setScale = 1f - (bounce_Magnitude * Mathf.Abs(Mathf.Sin(bounce_Prog)));
        transform.localScale = new Vector3(1f - (setScale - 1f), setScale, 1f);
    }
}
