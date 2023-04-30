using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeObject : MonoBehaviour
{
    [Tooltip("Time taken to complete the fade animation in seconds.")] public float timeToFade = 0.5f;
    public bool unscaledTime = false;

    [Space]
    [Tooltip("Fade in the object when it is enabled.")] public bool toFadeInOnEnable = false;
    [Tooltip("Disable the object after its fade out is completed.")] public bool disableAfterFadeOut = false;

    float prog = 0;

    SpriteRenderer[] sprites = null;
    Image[] images = null;
    TMP_Text[] texts = null;

    Dictionary<SpriteRenderer, Color> sprite__OriginalColor = new Dictionary<SpriteRenderer, Color>();
    Dictionary<Image, Color> image__OriginalColor = new Dictionary<Image, Color>();
    Dictionary<TMP_Text, Color> text__OriginalColor = new Dictionary<TMP_Text, Color>();

    Coroutine fadeThread;


    private void OnEnable()
    {
        if (toFadeInOnEnable) FadeIn();
    }


    public void FadeIn()
    {
        if (fadeThread != null) StopCoroutine(fadeThread);
        else prog = 0;

        fadeThread = StartCoroutine(_FadeIn());
    }
    private IEnumerator _FadeIn()
    {
        SetUpComponents();

        while (prog < 1)
        {
            if (unscaledTime) prog = Mathf.Clamp01(prog + Time.unscaledDeltaTime * (1 / timeToFade));
            else prog = Mathf.Clamp01(prog + Time.deltaTime * (1 / timeToFade));

            foreach (SpriteRenderer sprite in sprites) sprite.color = new Vector4(sprite__OriginalColor[sprite].r, sprite__OriginalColor[sprite].g, sprite__OriginalColor[sprite].b, Mathf.Lerp(0, sprite__OriginalColor[sprite].a, prog));
            foreach (Image image in images) image.color = new Vector4(image__OriginalColor[image].r, image__OriginalColor[image].g, image__OriginalColor[image].b, Mathf.Lerp(0, image__OriginalColor[image].a, prog));
            foreach (TMP_Text text in texts) text.color = new Vector4(text__OriginalColor[text].r, text__OriginalColor[text].g, text__OriginalColor[text].b, Mathf.Lerp(0, text__OriginalColor[text].a, prog));

            yield return new WaitForEndOfFrame();
        }
    }


    public void FadeOut()
    {
        if (fadeThread != null) StopCoroutine(fadeThread);
        else prog = 1;

        fadeThread = StartCoroutine(_FadeOut());
    }
    private IEnumerator _FadeOut()
    {
        SetUpComponents();

        while (prog > 0)
        {
            if (unscaledTime) prog = Mathf.Clamp01(prog - Time.unscaledDeltaTime * (1 / timeToFade));
            else prog = Mathf.Clamp01(prog - Time.deltaTime * (1 / timeToFade));

            foreach (SpriteRenderer sprite in sprites) sprite.color = new Vector4(sprite__OriginalColor[sprite].r, sprite__OriginalColor[sprite].g, sprite__OriginalColor[sprite].b, Mathf.Lerp(0, sprite__OriginalColor[sprite].a, prog));
            foreach (Image image in images) image.color = new Vector4(image__OriginalColor[image].r, image__OriginalColor[image].g, image__OriginalColor[image].b, Mathf.Lerp(0, image__OriginalColor[image].a, prog));
            foreach (TMP_Text text in texts) text.color = new Vector4(text__OriginalColor[text].r, text__OriginalColor[text].g, text__OriginalColor[text].b, Mathf.Lerp(0, text__OriginalColor[text].a, prog));

            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }


    private void SetUpComponents()
    {
        // Get components.
        sprites = GetComponentsInChildren<SpriteRenderer>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TMP_Text>();


        // Check if there are no usable components.
        if (sprites.Length == 0 && images.Length == 0 && texts.Length == 0)
        {
            Debug.LogError("In order for FadeOnEnable to work, the object must have at least one SpriteRenderer, Image or TMP_Text component!");
            Destroy(this);
        }


        // Get original colours for any new components found.
        foreach (SpriteRenderer sprite in sprites)
        {
            if (!sprite__OriginalColor.ContainsKey(sprite)) sprite__OriginalColor.Add(sprite, sprite.color);
        }

        foreach (Image image in images)
        {
            if (!image__OriginalColor.ContainsKey(image)) image__OriginalColor.Add(image, image.color);
        }

        foreach (TMP_Text text in texts)
        {
            if (!text__OriginalColor.ContainsKey(text)) text__OriginalColor.Add(text, text.color);
        }
    }
}
