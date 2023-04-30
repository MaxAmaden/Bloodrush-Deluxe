using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public int packagesToDeliver = 1;

    public GameObject[] targetRings;
    public TMP_Text text;
    public SpriteAnimation_ColourPulse pulse;

    [Space]
    public ParticleSystem[] particles_Score;
    public ParticleSystem[] particles_Complete;

    [HideInInspector] public bool active = false;

    private void Awake()
    {
        Deactivate();
    }

    public void Deactivate(bool complete = false)
    {
        active = false;

        text.text = "";
        foreach (var targetRing in targetRings) targetRing.SetActive(false);
        pulse.enabled = false;

        if (complete) { foreach (var particleSystem in particles_Complete) particleSystem.Play(); }
    }

    public void Activate()
    {
        active = true;

        text.text = "" + packagesToDeliver;
        foreach (var targetRing in targetRings) targetRing.SetActive(true);
        pulse.enabled = true;
    }

    public void Score()
    {
        packagesToDeliver--;
        text.text = "" + packagesToDeliver;

        foreach (var particleSystem in particles_Score) particleSystem.Play();

        if (packagesToDeliver <= 0) Deactivate(true);
    }
}
