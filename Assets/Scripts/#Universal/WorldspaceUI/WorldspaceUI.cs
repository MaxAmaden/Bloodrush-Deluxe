using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldspaceUI : MonoBehaviour
{
    public Transform holder;

    List<GameObject> tempAttachedObjects = new List<GameObject>();


    private void Awake()
    {
        Statics.WorldspaceUI = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += SceneChange;
    }

    private void SceneChange(Scene scene, Scene next)
    {
        // Destroy and clear temporarily attached objects.
        foreach (GameObject tempAttachedObject in tempAttachedObjects)
        {
            if (tempAttachedObject != null) Destroy(tempAttachedObject);
        }
        tempAttachedObjects = new List<GameObject>();
    }


    public void AttachToUI(GameObject toAttach, bool maintainWorldSpacePosition = false, bool permanent = false)
    {
        if (!permanent) tempAttachedObjects.Add(toAttach);

        toAttach.transform.SetParent(holder, maintainWorldSpacePosition);
    }
}
