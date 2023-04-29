using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseObjectAtSpawn : MonoBehaviour
{
    public Vector3 randomScaleModifier;
    public float randomUniformScaleModifier;

    // DEFUNCT
    [Space]
    public Vector3 randomRotationDegree;

    // DEFUNCT
    [Space]
    public Vector3 randomPosition;

    void Start()
    {
        Vector3 scale = transform.localScale;
        scale.x *= Random.Range(1 - randomScaleModifier.x, 1 + randomScaleModifier.x);
        scale.y *= Random.Range(1 - randomScaleModifier.y, 1 + randomScaleModifier.y);
        scale.z *= Random.Range(1 - randomScaleModifier.z, 1 + randomScaleModifier.z);

        scale *= Random.Range(1 - randomUniformScaleModifier, 1 + randomUniformScaleModifier);
        transform.localScale = scale;

        Vector3 newRotation = transform.eulerAngles;
        newRotation.x += Random.Range(-randomRotationDegree.x, randomRotationDegree.x);
        newRotation.y += Random.Range(-randomRotationDegree.y, randomRotationDegree.y);
        newRotation.z += Random.Range(-randomRotationDegree.z, randomRotationDegree.z);
        transform.rotation = Quaternion.Euler(newRotation);

        Destroy(this);
    }
}
