using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectsAfterTime : MonoBehaviour
{
    public List<GameObjectWithTime> objectsToEnable;

    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        for (int i = 0; i < objectsToEnable.Count; i++)
        {
            if (timer >= objectsToEnable[i].time)
            {
                objectsToEnable[i].gameObject.SetActive(true);
                objectsToEnable.RemoveAt(i);

                i--;
            }
        }
    }
}

[System.Serializable]
public class GameObjectWithTime
{
    public GameObject gameObject;
    public float time;
}