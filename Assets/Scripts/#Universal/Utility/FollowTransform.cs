﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}
