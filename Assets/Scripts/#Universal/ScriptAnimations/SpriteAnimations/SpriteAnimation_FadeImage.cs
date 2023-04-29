using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation_FadeImage : SpriteAnimation
{
    public float fadeTime;
    public AnimationCurve fadeCurve;

    [Space]
    public float delay;
    public bool destroyAfterFade;

    private void Start()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        yield return new WaitForSeconds(delay);

        Image image = GetComponent<Image>();
        Color startColor = new Color(image.color.r, image.color.g, image.color.b, 1);
        Color endColor = new Color(image.color.r, image.color.g, image.color.b, 0);

        float prog = 0;
        float fadeTimer = 0;

        while (fadeTimer < fadeTime)
        {
            fadeTimer += Time.unscaledDeltaTime;
            prog = Mathf.Clamp01(fadeTimer / fadeTime);

            if (fadeCurve != null) image.color = Color.Lerp(startColor, endColor, fadeCurve.Evaluate(prog));
            else image.color = Color.Lerp(startColor, endColor, prog);

            yield return new WaitForEndOfFrame();
        }

        if (destroyAfterFade) Destroy(gameObject);
    }
}
