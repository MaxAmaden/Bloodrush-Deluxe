using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeUntilDestruction = 2;

    [Space]
    public float spriteFadeBuffer;
    public SpriteRenderer[] spritesToFade;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spriteFadeBuffer)
        {
            if (spritesToFade != null)
            {
                foreach (SpriteRenderer sprite in spritesToFade)
                {
                    Color newColor = sprite.color;
                    newColor.a = Mathf.Lerp(1, 0, (timer - spriteFadeBuffer) / (timeUntilDestruction - spriteFadeBuffer));
                    sprite.color = newColor;
                }
            }
        }

        if (timer >= timeUntilDestruction) Destroy(gameObject);
    }
}