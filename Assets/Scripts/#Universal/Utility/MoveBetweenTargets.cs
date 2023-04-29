using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTargets : MonoBehaviour
{
    public float speed;
    Transform target;
    public Transform pointA;
    public Transform pointB;

    private void Start()
    {
        target = pointA;
    }

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
       if(target == pointA)
        {
            target = pointB;
        }
        else
        {
            target = pointA;
        }
    }
}