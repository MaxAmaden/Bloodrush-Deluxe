using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningCutscene : MonoBehaviour
{
    public FadeObject text0;
    public FadeObject text1;
    public FadeObject text2;
    public FadeObject text3;
    public TMP_Text text3_Text;
    public FadeObject text4;
    public GameObject text5;
    public GameObject text6;

    private void Start()
    {
        StartCoroutine(Go());

        IEnumerator Go()
        {
            // Panel 1.

            yield return new WaitForSeconds(1f);

            text0.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            text0.FadeOut();

            yield return new WaitForSeconds(2f);





            text1.gameObject.SetActive(true);

            yield return new WaitForSeconds(4f);

            text2.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            text3.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            text3_Text.text = "0:59";
            yield return new WaitForSeconds(1f);
            text3_Text.text = "0:58";
            yield return new WaitForSeconds(1f);
            text3_Text.text = "0:57";
            yield return new WaitForSeconds(1f);
            text3_Text.text = "0:56";

            text1.FadeOut();
            text2.FadeOut();
            text3.FadeOut();



            yield return new WaitForSeconds(2f);

            text4.gameObject.SetActive(true);

            yield return new WaitForSeconds(4f);

            text5.gameObject.SetActive(true);

            Statics.VFX.screenShake_Magnitude = 0.2f;
            Statics.VFX.screenShake_Time = 0.2f;

            Statics.SFX.PlaySound(SoundEffects.SlowMo);

            yield return new WaitForSeconds(2f);

            text6.gameObject.SetActive(true);

            Statics.VFX.screenShake_Magnitude = 0.2f;
            Statics.VFX.screenShake_Time = 0.2f;

            Statics.SFX.PlaySound(SoundEffects.FastMo);

            yield return new WaitForSeconds(4f);




            Statics.VFX.FlashScreen(2f, 0.1f, 1f, Color.black);

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(2);
        }
    }
}
