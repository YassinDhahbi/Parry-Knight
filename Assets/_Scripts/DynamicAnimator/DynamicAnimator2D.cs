using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAnimator2D : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private DynamicAnimation idleAnimation;

    [SerializeField]
    private DynamicAnimation walkingAnimation;

    [SerializeField]
    private DynamicAnimation deathAnimation;

    [SerializeField]
    private DynamicAnimation currentAnimation;

    [SerializeField]
    private int currentSelectedSprite;

    private void Awake()
    {
        SelectIdle();
    }

    // This will be used in the animator
    private void PlaySprite(SpriteRenderer spriteRenderer)
    {
        if (currentAnimation.repeatable && currentSelectedSprite == currentAnimation.listOfAnimationSprites.Count)
        {
            currentSelectedSprite = 0;
        }
        if (currentSelectedSprite >= currentAnimation.listOfAnimationSprites.Count)
        {
            currentSelectedSprite = currentAnimation.listOfAnimationSprites.Count;
            return;
        }
        spriteRenderer.sprite = currentAnimation.listOfAnimationSprites[currentSelectedSprite];
        currentSelectedSprite++;
    }

    [ContextMenu("Test")]
    public void PlayAnimation()
    {
        PlaySprite(spriteRenderer);
    }

    public void SelectIdle()
    {
        currentAnimation = idleAnimation;
        currentSelectedSprite = 0;
    }

    [ContextMenu("Run")]
    public void SelectRun()
    {
        currentAnimation = walkingAnimation;
        currentSelectedSprite = 0;
    }

    [ContextMenu("Die")]
    public void SelectDeath()
    {
        currentAnimation = deathAnimation;
        currentSelectedSprite = 0;
    }
}