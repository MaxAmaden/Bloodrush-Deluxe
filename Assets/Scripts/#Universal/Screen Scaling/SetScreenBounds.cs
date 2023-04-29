using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenBounds : MonoBehaviour
{
    private void Awake()
    {
        Statics.screenBounds = GetComponent<Collider2D>();
        Destroy(this);
    }
}
