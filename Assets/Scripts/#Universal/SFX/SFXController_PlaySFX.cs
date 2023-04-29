using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController_PlaySFX : MonoBehaviour
{
    public SoundEffects SFX = SoundEffects.none;

    [Header("CUSTOM SFX")]
    public SoundEffect customSFX;

    [Space]
    public bool playOnEnable = true;
    public bool disableAfterPlay = false;

    private void OnEnable()
    {
        if (playOnEnable) Activate();
    }

    public void Activate()
    {
        if (SFX == SoundEffects.none) Statics.SFX.PlaySound(customSFX);
        else Statics.SFX.PlaySound(SFX);

        if (disableAfterPlay) gameObject.SetActive(false);
    }
}