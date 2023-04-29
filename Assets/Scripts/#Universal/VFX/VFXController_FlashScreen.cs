using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController_FlashScreen : MonoBehaviour
{
    public Color color = Color.white;

    [Space]
    public float entryTime = 0.025f;
    public float pauseTime = 0.05f;
    public float exitTime = 0.05f;


    public void Activate()
    {
        if (Statics.VFX != null) Statics.VFX.FlashScreen(entryTime, pauseTime, exitTime, color);
    }
}
