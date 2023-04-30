using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownPackage : MonoBehaviour
{
    public FadeObject throwLine;

    public void ThrowPackage(Vector2 from, Vector2 to)
    {
        StartCoroutine(ThrowPackage());

        IEnumerator ThrowPackage()
        {
            // Set up line.
            transform.position = from;
            throwLine.transform.localRotation = Quaternion.Euler(0, 0, 360f - Statics.Maths.GetAngleFromVectorDirection((to - from).normalized));

            // Animate.
            float prog = 0f;
            float speed = 1f / 0.025f;
            Vector3 startScale = new Vector3(0f, 0f, 1f);
            Vector3 endScale = new Vector3(0.05f, Vector2.Distance(from, to), startScale.z);
            AnimationCurve curve = Curves.GetCurve(Curves.Curve.FastStartSlowEnd);
            while (prog < 1f)
            {
                prog += Time.unscaledDeltaTime * speed;

                throwLine.transform.localScale = Vector3.Lerp(startScale, endScale, curve.Evaluate(prog));

                yield return null;
            }

            yield return new WaitForSecondsRealtime(0.1f);

            throwLine.FadeOut();
            yield return new WaitForSecondsRealtime(throwLine.timeToFade);

            Destroy(gameObject);
        }
    }
}