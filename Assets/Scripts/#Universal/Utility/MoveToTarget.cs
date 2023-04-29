using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float speed;
    public Transform target;

    [Space]
    public bool toDestroyAfterwards;
    public bool toFadeAfterwards;
    public GameObject[] enableGameObjectsAfterwards;

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= speed * Time.deltaTime)
        {
            transform.position = target.position;
            DoThisWhenFinished();
        }
        else transform.position += speed * Time.deltaTime * (target.position - transform.position).normalized;
    }

    public virtual void DoThisWhenFinished()
    {
        foreach (GameObject gameObjectToEnable in enableGameObjectsAfterwards) gameObjectToEnable.SetActive(true);

        if (toDestroyAfterwards) Destroy(gameObject);
        else if (toFadeAfterwards)
        {
            FadeObject fader = GetComponent<FadeObject>();
            if (fader == null) fader = gameObject.AddComponent<FadeObject>();

            fader.FadeOut();
        }
        
        Destroy(this);
    }
}