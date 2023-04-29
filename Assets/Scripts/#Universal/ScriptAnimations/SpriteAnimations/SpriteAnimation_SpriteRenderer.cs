using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation_SpriteRenderer : SpriteAnimation
{
    public bool toAnimate = true;
    public float frameDelay = 0.1f;

    [Space]
    public Sprite[] animationFrames;

    [Space]
    public AnimationSet[] animationSets;

    int frameCounter = 0;
    float timer = 0;

    int currentAnimationSet = -1;

    bool stopAfterAnimation = false;

    [HideInInspector] public SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (animationFrames.Length <= 0 && toAnimate) LoopAnimationSet(0);
        if (animationSets.Length <= 0) animationSets = new AnimationSet[1] { new AnimationSet(animationFrames) };
    }

    void Update()
    {
        if (toAnimate)
        {
            timer += Time.deltaTime;
            if (timer > frameDelay)
            {
                timer -= frameDelay;

                frameCounter++;
                if (frameCounter >= animationFrames.Length)
                {
                    frameCounter = 0;
                    if (stopAfterAnimation)
                    {
                        stopAfterAnimation = false;
                        toAnimate = false;
                    }
                }

                sr.sprite = animationFrames[frameCounter];
            }
        }
    }

    public void LoopAnimationSet(int animationSet, bool forceAnimation = false)
    {
        toAnimate = true;

        if (animationSet != currentAnimationSet)
        {
            currentAnimationSet = animationSet;

            frameCounter = 0;
            animationFrames = animationSets[animationSet].sprites;
        }

        if (forceAnimation) sr.sprite = animationFrames[0];
    }

    public void PlayAnimationSet(int animationSet)
    {
        toAnimate = true;
        frameCounter = 0;
        stopAfterAnimation = true;

        if (animationSet != currentAnimationSet)
        {
            currentAnimationSet = animationSet;
            animationFrames = animationSets[animationSet].sprites;
        }
    }
}

[System.Serializable]
public class AnimationSet
{
    public Sprite[] sprites;

    public AnimationSet(Sprite[] _sprites)
    {
        sprites = _sprites;
    }
}
