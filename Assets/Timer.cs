using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text minutes;
    public TMP_Text seconds;
    public TMP_Text fractions;

    float timer = 0f;

    private void Start()
    {
        if (Statics.tutorialMode) Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        minutes.text = "" + Mathf.FloorToInt(timer / 60f);

        seconds.text = "" + Mathf.FloorToInt(timer);
        if (seconds.text.Length == 1) seconds.text = "0" + seconds.text;

        fractions.text = "" + Mathf.FloorToInt((timer % 1f) * 100f);
        if (fractions.text.Length == 1) fractions.text = "0" + fractions.text;
    }
}
