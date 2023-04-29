using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFXController : MonoBehaviour
{
    [Header("- SCREEN FLASH VARIABLES -")]
    public Image screenFlash_ScreenCover;
    public AnimationCurve screenFlash_SmoothingCurve;

    private Image image_ScreenFlash;

    private float screenFlash_FlashTime_Entry;
    private float screenFlash_FlashTime_Pause;
    private float screenFlash_FlashTime_Exit;
    private Color screenFlash_Color;


    [Header("- SCREEN SHAKE VARIABLES -")]
    public bool screenShake_PermaShake = false;
    public float screenShake_PermaShake_Magnitude = 0.1f;

    [Space]
    public float screenShake_Time = 0;
    public float screenShake_Magnitude = 0.1f;

    private Vector2 screenShake_CurrentOffset;

    CameraController cam;
    private Coroutine fadeThread;


    private void Awake()
    {
        if (Statics.VFX)
        {
            Destroy(this);
            return;
        }
        else Statics.VFX = this;

        DontDestroyOnLoad(gameObject);
    }


    public void FlashScreen(float flashTime_Entry, float flashTime_Pause, float flashTime_Exit, Color color)
    {
        screenFlash_FlashTime_Entry = flashTime_Entry;
        screenFlash_FlashTime_Pause = flashTime_Pause;
        screenFlash_FlashTime_Exit = flashTime_Exit;
        screenFlash_Color = color;

        if (fadeThread != null) StopCoroutine(fadeThread);
        fadeThread = StartCoroutine(ScreenFlash());
    }
    public void FlashScreen(float flashTime_Entry, float flashTime_Pause, float flashTime_Exit)
    {
        FlashScreen(flashTime_Entry, flashTime_Pause, flashTime_Exit, Color.white);
    }
    public void FlashScreen(float flashTime, Color color)
    {
        FlashScreen(flashTime / 3f, flashTime / 3f, flashTime / 3f, color);
    }
    public void FlashScreen(float flashTime)
    {
        FlashScreen(flashTime * 0.2f, flashTime * 0.4f, flashTime * 0.4f, Color.white);
    }
    public void FlashScreen(Color color)
    {
        FlashScreen(0.025f, 0.05f, 0.05f, color);
    }
    public void FlashScreen()
    {
        FlashScreen(0.025f, 0.05f, 0.05f, Color.white);
    }


    private IEnumerator ScreenFlash()
    {
        // Set up screen flash.
        Color color_Visible = new Color(screenFlash_Color.r, screenFlash_Color.g, screenFlash_Color.b, 1);
        Color color_Invisible = new Color(screenFlash_Color.r, screenFlash_Color.g, screenFlash_Color.b, 0);


        // Flash screen.
        float timer = 0;
        while (timer < screenFlash_FlashTime_Entry)
        {
            timer = Mathf.Clamp(timer + Time.unscaledDeltaTime, 0, screenFlash_FlashTime_Entry);

            screenFlash_ScreenCover.color = Color.Lerp(color_Invisible, color_Visible, screenFlash_SmoothingCurve.Evaluate(timer / screenFlash_FlashTime_Entry));

            yield return new WaitForEndOfFrame();
        }

        screenFlash_ScreenCover.color = color_Visible;
        yield return new WaitForSecondsRealtime(screenFlash_FlashTime_Pause);

        timer = screenFlash_FlashTime_Exit;
        while (timer > 0)
        {
            timer = Mathf.Clamp(timer - Time.unscaledDeltaTime, 0, screenFlash_FlashTime_Exit);

            screenFlash_ScreenCover.color = Color.Lerp(color_Invisible, color_Visible, screenFlash_SmoothingCurve.Evaluate(timer / screenFlash_FlashTime_Exit));

            yield return new WaitForEndOfFrame();
        }

        screenFlash_ScreenCover.color = color_Invisible;
    }


    private void ScreenShake_Update()
    {
        if (Statics.isPaused) return;

        // Check camera.
        if (cam == null || !cam.isActiveAndEnabled) cam = Camera.main.GetComponent<CameraController>();
        if (cam == null) return;

        // Reverse previous frame of screen shake.
        else cam.shake = Vector2.zero;


        // Calculate screen shake.
        if (screenShake_PermaShake)
        {
            if (screenShake_Time > 0) screenShake_CurrentOffset = Random.insideUnitCircle * (screenShake_Magnitude + screenShake_PermaShake_Magnitude);
            else screenShake_CurrentOffset = Random.insideUnitCircle * screenShake_PermaShake_Magnitude;
        }
        else if (screenShake_Time > 0) screenShake_CurrentOffset = Random.insideUnitCircle * screenShake_Magnitude;
        else screenShake_CurrentOffset = Vector2.zero;

        screenShake_Time = Mathf.Clamp(screenShake_Time - Time.unscaledDeltaTime, 0, Mathf.Infinity);


        // Apply current frame of screen shake.
        cam.shake = screenShake_CurrentOffset;
    }


    private void Update()
    {
        ScreenShake_Update();
    }
}
