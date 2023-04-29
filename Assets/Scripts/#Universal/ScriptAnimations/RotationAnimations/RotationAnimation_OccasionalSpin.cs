using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation_OccasionalSpin : MonoBehaviour
{
    public Vector2 spinDelay;

    [Space]
    public float spinSpeed;

    float delay;
    float timer = 0;

    private void Start()
    {
        Spin();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay) Spin();
    }

    private void Spin()
    {
        timer = 0;
        delay = Random.Range(spinDelay.x, spinDelay.y);

        StopCoroutine("CommenceSpin");
        StartCoroutine("CommenceSpin");
    }

    private IEnumerator CommenceSpin()
    {
        float prog = 0;
        while (prog < 1)
        {
            prog = Mathf.Clamp01(prog + Time.deltaTime * spinSpeed);

            transform.localRotation = Quaternion.Euler(0, 0, Mathf.SmoothStep(0, 1, prog) * 360);

            yield return new WaitForEndOfFrame();
        }
    }
}
