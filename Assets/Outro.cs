using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public FadeObject text1;
    public FadeObject text0;
    public FadeObject[] stars;

    public GameObject title;
    public GameObject credits;
    public GameObject stuck;

    private void Start()
    {
        StartCoroutine(Go());

        IEnumerator Go()
        {
            Statics.VFX.FlashScreen(0f, 2f, 2f, Color.black);

            yield return new WaitForSecondsRealtime(5f);

            text0.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(4f);

            foreach (FadeObject star in stars)
            {
                Statics.VFX.screenShake_Magnitude = 0.2f;
                Statics.VFX.screenShake_Time = 0.2f;

                star.gameObject.SetActive(true);

                Statics.SFX.PlaySound(SoundEffects.scorePackage);

                yield return new WaitForSecondsRealtime(0.2f);
            }

            yield return new WaitForSecondsRealtime(3f);

            text0.FadeOut();
            text1.FadeOut();
            foreach (FadeObject star in stars) star.FadeOut();

            yield return new WaitForSecondsRealtime(4f);

            title.SetActive(true);
            Statics.VFX.screenShake_Magnitude = 0.3f;
            Statics.VFX.screenShake_Time = 0.2f;

            Statics.SFX.PlaySound(SoundEffects.SlowMo);

            yield return new WaitForSecondsRealtime(2f);

            credits.SetActive(true);
            Statics.VFX.screenShake_Magnitude = 0.3f;
            Statics.VFX.screenShake_Time = 0.2f;

            Statics.SFX.PlaySound(SoundEffects.Crash);

            yield return new WaitForSecondsRealtime(4f);

            stuck.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("Main");
    }
}
