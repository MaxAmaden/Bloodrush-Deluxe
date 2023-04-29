using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation_Image : SpriteAnimation
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

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();

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

                image.sprite = animationFrames[frameCounter];
            }
        }
    }

    public void LoopAnimationSet(int animationSet)
    {
        toAnimate = true;

        if (animationSet != currentAnimationSet)
        {
            currentAnimationSet = animationSet;

            frameCounter = 0;
            animationFrames = animationSets[animationSet].sprites;
        }
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
