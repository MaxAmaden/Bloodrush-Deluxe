using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownPackage : MonoBehaviour
{
    public FadeObject throwLine;

    private Vector2 temp_HitPoint = Vector2.zero;

    public void ThrowPackage(Vector2 from, Vector2 to)
    {
        temp_HitPoint = from;

        StartCoroutine(ThrowPackage());

        IEnumerator ThrowPackage()
        {
            transform.position = from;
            throwLine.transform.rotation = Quaternion.Euler(0, 0, Statics.Maths.GetAngleFromVectorDirection((to - from).normalized));
            throwLine.transform.localScale = new Vector3(0.1f, Vector2.Distance(from, to), 1f);

            throwLine.FadeOut();
            yield return new WaitForSecondsRealtime(throwLine.timeToFade);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(temp_HitPoint, 0.5f);
    }
}