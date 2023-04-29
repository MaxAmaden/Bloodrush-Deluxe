using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeObject_FadeOutOnEnable : MonoBehaviour
{
    public FadeObject[] objectsToFadeOut;

    private void OnEnable()
    {
        foreach (FadeObject objectToFadeOut in objectsToFadeOut)
        {
            if (objectToFadeOut != null && objectToFadeOut.isActiveAndEnabled) objectToFadeOut.FadeOut();
        }

        gameObject.SetActive(false);
    }
}