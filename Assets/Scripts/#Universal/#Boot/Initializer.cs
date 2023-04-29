using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initializer
{
    private static string startScene = "";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void StartInitializing()
    {
        startScene = SceneManager.GetActiveScene().name;
        if (startScene != "BootScreen") SceneManager.LoadScene("BootScreen", LoadSceneMode.Single);
    }

    public static void FinishInitializing()
    {
        if (startScene == "BootScreen") SceneManager.LoadScene(1);
        else SceneManager.LoadScene(startScene);
    }
}