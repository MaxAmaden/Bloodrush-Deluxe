using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisePitch : MonoBehaviour
{
    public Vector2 minMaxPitch;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

        Randomise();
    }

    public void Randomise()
    {
        source.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);
    }

    public void RandomiseAndPlay()
    {
        Randomise();

        source.Play();
    }
}
