using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController_PlaySFXRandomly : MonoBehaviour
{
    public SoundEffect[] sounds;
    public Vector2 minMaxInterval;

    private void Start()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minMaxInterval.x, minMaxInterval.y));
            Statics.SFX.PlaySound(sounds[Random.Range(0, sounds.Length)]);
        }
    }
}
