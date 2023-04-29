using UnityEngine;

public class RotationAnimation_Maintain : RotationAnimation
{
    public Vector3 overrideRotation;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(overrideRotation);
    }
}
