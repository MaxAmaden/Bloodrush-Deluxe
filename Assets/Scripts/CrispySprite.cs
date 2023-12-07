using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrispySprite : MonoBehaviour
{
    public float borderWidth = 0.2f;
    public bool randomiseColor;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (randomiseColor) sr.color = Random.ColorHSV(0f, 1f, 0.2f, 0.3f, 1f, 1f, sr.color.a, sr.color.a);

        Transform border = Instantiate(Resources.Load("Prefabs/CrispySprite/Border") as GameObject).transform;
        Transform mask = Instantiate(Resources.Load("Prefabs/CrispySprite/Mask") as GameObject).transform;

        border.SetParent(transform.parent);
        mask.SetParent(transform.parent);

        border.localScale = new Vector3(sr.size.x + borderWidth, sr.size.y + borderWidth, 1f);
        mask.localScale = new Vector3(sr.size.x, sr.size.y, 1f);

        border.SetParent(transform);
        mask.SetParent(transform);

        border.localPosition = Vector2.zero;
        mask.localPosition = Vector2.zero;
    }
}
