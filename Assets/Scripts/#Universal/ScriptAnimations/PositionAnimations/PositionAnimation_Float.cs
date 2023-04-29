using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimation_Float : PositionAnimation
{
    public bool toFloat = true;
    public float floatSpeed;
    public float floatHeight;

    [Space]
    public float verticalFloatModifier = 1;
    public float horizontalFloatModifier = 0;

    [Space]
    public FloatMode mode;
    public bool additionalMovement = false;
    public bool consistent = false;

    [Space]
    public bool randomiseFloatStart;
    public Vector2 randomSpeedModifierRange = Vector2.one;
    public Vector2 randomHeightModifierRange = Vector2.one;

    [Space]
    public float progress = 0;

    bool hasInitialised = false;

    Vector3 startPosition = -Vector3.one;

    void OnEnable()
    {
        if (consistent && hasInitialised) return;
        else hasInitialised = true;

        startPosition = transform.localPosition;

        floatSpeed *= Random.Range(randomSpeedModifierRange.x, randomSpeedModifierRange.y);
        floatHeight *= Random.Range(randomHeightModifierRange.x, randomHeightModifierRange.y);

        if (randomiseFloatStart) progress = Random.value;

        if (mode == FloatMode.floatFromBottom)
        {
            startPosition.y += floatHeight;
            progress += 0.5f;
        }
    }

    void Update()
    {
        if (toFloat)
        {
            progress += floatSpeed * Time.deltaTime;

            if (mode == FloatMode.bounce) progress %= 0.5f;
            else progress %= 1;

            if (additionalMovement) transform.localPosition += new Vector3((Mathf.Sin(progress * 2 * Mathf.PI) * floatHeight) * horizontalFloatModifier, (Mathf.Sin(progress * 2 * Mathf.PI) * floatHeight) * verticalFloatModifier, 0);
            else transform.localPosition = startPosition + new Vector3((Mathf.Sin(progress * 2 * Mathf.PI) * floatHeight) * horizontalFloatModifier, (Mathf.Sin(progress * 2 * Mathf.PI) * floatHeight) * verticalFloatModifier, 0);
        }
    }
}

public enum FloatMode { floatFromBottom, floatFromMid, bounce }