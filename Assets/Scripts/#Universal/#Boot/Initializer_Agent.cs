using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer_Agent : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AnimateProcedures());
    }

    private IEnumerator AnimateProcedures()
    {
#if UNITY_EDITOR
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        Initializer.FinishInitializing();
#else
        yield return new WaitForSecondsRealtime(5.0f);

        Statics.VFX.FlashScreen(0.75f, 0.1f, 1f, Color.black);
        yield return new WaitForSecondsRealtime(0.75f);

        Initializer.FinishInitializing();
#endif
    }
}
