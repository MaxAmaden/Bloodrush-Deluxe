using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public FadeObject tutorial1;
    public FadeObject tutorial2;
    public FadeObject tutorial3;

    private void Start()
    {
        StartCoroutine(Go());

        IEnumerator Go()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            tutorial1.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(3f);
            while (!Input.GetMouseButton(0)) yield return null;

            tutorial1.FadeOut();
            yield return new WaitForSecondsRealtime(1f);
            tutorial2.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(3f);
            while (!Input.GetMouseButtonUp(1)) yield return null;

            tutorial2.FadeOut();
            yield return new WaitForSecondsRealtime(1f);
            tutorial3.gameObject.SetActive(true);
        }
    }
}
