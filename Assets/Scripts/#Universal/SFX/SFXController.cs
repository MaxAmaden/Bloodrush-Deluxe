using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{
    public AudioMixer volumeMixer;
    public GameObject defaultAudioSourceGO;

    [Space]
    public List<SoundEffect> allSoundEffects;

    private List<AudioSource> allAudioSources = new List<AudioSource>();

    const int audioSourceLimit = 60;

    private void Awake()
    {
        if (Statics.SFX)
        {
            Destroy(this);
            return;
        }
        else Statics.SFX = this;

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource PlaySound(SoundEffect SFX)
    {
        // Get an audio source to play the SFX.
        AudioSource selectedAudioSource = null;

        // Check for an available audio source, and cancel this entire request if sound is already on cooldown.
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                if (audioSource.clip == SFX.audioClip && audioSource.time < 0.05f) return null;
                else continue;
            }

            selectedAudioSource = audioSource;
            break;
        }

        // If no available audio source exists, and there is room for a new source, then create one.
        if (!selectedAudioSource && allAudioSources.Count < audioSourceLimit)
        {
            selectedAudioSource = CreateNewAudioSource();
            allAudioSources.Add(selectedAudioSource);
        }

        // If audio sources are capped out, attempt to replace an old SFX (of the same type).
        if (!selectedAudioSource && allAudioSources.Count >= audioSourceLimit) selectedAudioSource = GetAndStopOldestAudioSource(SFX.audioClip);

        // If still no available audio source and audio sources are capped out, replace an old SFX (of any type).
        if (!selectedAudioSource && allAudioSources.Count >= audioSourceLimit) selectedAudioSource = GetAndStopOldestAudioSource();


        // Play the sound effect.
        selectedAudioSource.clip = SFX.audioClip;
        selectedAudioSource.loop = SFX.toLoop;
        selectedAudioSource.volume = SFX.volume;
        selectedAudioSource.pitch = Random.Range(SFX.randomPitchRange.x, SFX.randomPitchRange.y);

        selectedAudioSource.Play();

        return selectedAudioSource;
    }
    public AudioSource PlaySound(SoundEffects SFX)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        return PlaySound(selectedSFX);
    }
    public AudioSource PlaySound(SoundEffects SFX, float volume)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        selectedSFX.volume = volume;

        return PlaySound(selectedSFX);
    }
    public AudioSource PlaySound(SoundEffects SFX, Vector2 pitchRange)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        selectedSFX.randomPitchRange = pitchRange;

        return PlaySound(selectedSFX);
    }
    public AudioSource PlaySound(SoundEffects SFX, float volume, Vector2 pitchRange)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        selectedSFX.volume = volume;
        selectedSFX.randomPitchRange = pitchRange;

        return PlaySound(selectedSFX);
    }
    public AudioSource PlaySound(string SFX)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        return PlaySound(selectedSFX);
    }
    public AudioSource PlaySound(string SFX, float volume)
    {
        SoundEffect selectedSFX = GetSoundEffect(SFX);
        if (selectedSFX == null) return null;

        selectedSFX.volume = volume;

        return PlaySound(selectedSFX);
    }


    public void ClearSounds()
    {
        foreach (AudioSource audioSource in allAudioSources) audioSource.Stop();
    }

    public void StopSound(SoundEffects SFX)
    {
        SoundEffect soundEffectToStop = GetSoundEffect(SFX);

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (!audioSource.isPlaying) continue;
            if (audioSource.clip == soundEffectToStop.audioClip) audioSource.Stop();
        }
    }

    public void FadeInSFXChannel(float fadeTime = 2f, float delay = 0f)
    {
        StartCoroutine(_FadeInSFXChannel(fadeTime, delay));
    }
    private IEnumerator _FadeInSFXChannel(float fadeTime = 2f, float delay = 0f)
    {
        yield return new WaitForSecondsRealtime(delay);

        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime;

            volumeMixer.SetFloat("SFX_Volume", Mathf.Lerp(-80f, 0f, timer / fadeTime));

            yield return new WaitForEndOfFrame();
        }
    }


    private AudioSource GetAndStopOldestAudioSource(AudioClip clip = null)
    {
        float oldestTime = -1f;
        AudioSource oldestAudioSource = null;
        foreach (AudioSource checkAudioSource in allAudioSources)
        {
            if (!checkAudioSource.isPlaying) continue;
            if (clip != null && checkAudioSource.clip != clip) continue;

            if (checkAudioSource.time > oldestTime)
            {
                oldestTime = checkAudioSource.time;
                oldestAudioSource = checkAudioSource;
            }
        }

        if (oldestAudioSource != null) oldestAudioSource.Stop();

        return oldestAudioSource;
    }


    private AudioSource CreateNewAudioSource()
    {
        GameObject newAudioSourceGO = Instantiate(defaultAudioSourceGO);

        newAudioSourceGO.transform.parent = defaultAudioSourceGO.transform.parent;
        newAudioSourceGO.name = "SFX - " + allAudioSources.Count;

        return newAudioSourceGO.GetComponent<AudioSource>();
    }

    private SoundEffect GetSoundEffect(SoundEffects identifier)
    {
        foreach (SoundEffect soundEffect in allSoundEffects)
        {
            if (soundEffect.identifier == identifier) return soundEffect;
        }

        Debug.LogError("[!] No SFX profile found for '" + identifier.ToString() + "'!");
        return null;
    }
    private SoundEffect GetSoundEffect(string identifier)
    {
        identifier = identifier.ToLower();
        foreach (SoundEffect soundEffect in allSoundEffects)
        {
            if (soundEffect.identifier.ToString().ToLower() == identifier) return soundEffect;
        }

        Debug.LogError("[!] No SFX profile found for '" + identifier + "'!");
        return null;
    }
}


[System.Serializable]
public class SoundEffect
{
    public SoundEffects identifier;
    public AudioClip audioClip;

    public Vector2 randomPitchRange = Vector2.one;
    public bool toLoop = false;
    public float volume = 0.7f;
}


public enum SoundEffects { none, throwPackage, scorePackage, PrepareThrow, SlowMo, FastMo, Crash }