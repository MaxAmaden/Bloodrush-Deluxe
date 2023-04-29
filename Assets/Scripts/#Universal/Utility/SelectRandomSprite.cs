using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomSprite : MonoBehaviour
{
    public Sprite[] randomSprites;

    [Space]
    public SpriteRenderer[] spriteRenderers = new SpriteRenderer[0];

    private void Awake()
    {
        if (spriteRenderers.Length <= 0) spriteRenderers = new SpriteRenderer[] { gameObject.GetComponent<SpriteRenderer>() };
        if (spriteRenderers[0] == null) spriteRenderers = new SpriteRenderer[] { gameObject.GetComponentInChildren<SpriteRenderer>() };
        if (spriteRenderers[0] == null)
        {
            Debug.LogError("No SpriteRenderer found for selecting a random sprite! Deleting component.");
            Destroy(this);
        }
    }

    private void Start()
    {
        Initiate();
    }

    public void Initiate()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers) spriteRenderer.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
    }
}