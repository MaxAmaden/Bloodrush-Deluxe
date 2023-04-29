using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation_Spin : RotationAnimation
{
    public bool toRotate;
    public Vector3 rotationSpeed;

    [Space]
    public Vector2 uniformRandomRange = Vector2.one;

    [Space]
    public Vector3 randomAxisMultiplierLower = Vector3.one;
    public Vector3 randomAxisMultiplierUpper = Vector3.one;

    Vector3 currentAngle;

    void OnEnable()
    {
        currentAngle = transform.rotation.eulerAngles;

        rotationSpeed *= Random.Range(uniformRandomRange.x, uniformRandomRange.y);

        rotationSpeed.x *= Random.Range(randomAxisMultiplierLower.x, randomAxisMultiplierUpper.x);
        rotationSpeed.y *= Random.Range(randomAxisMultiplierLower.y, randomAxisMultiplierUpper.y);
        rotationSpeed.z *= Random.Range(randomAxisMultiplierLower.z, randomAxisMultiplierUpper.z);
    }

    void Update()
    {
        if (toRotate)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(currentAngle);
        }
    }
}
