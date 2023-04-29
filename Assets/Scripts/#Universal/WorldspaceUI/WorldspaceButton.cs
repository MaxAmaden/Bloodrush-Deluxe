using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class WorldspaceButton : MonoBehaviour
{
    public bool startDisabled = false;

    [Space]
    public WorldspaceButton_ColorMode colorMode = WorldspaceButton_ColorMode.set;
    public Color hoverColor = Color.white;
    public Color clickColor = Color.white;
    public Color disabledColor = Color.grey;

    [Space]
    public SpriteRenderer[] sprites = new SpriteRenderer[0];
    public TMP_Text[] texts = new TMP_Text[0];

    [Space]
    public UnityEvent onClick;


    internal Dictionary<SpriteRenderer, Color> baseColor_Sprite = new Dictionary<SpriteRenderer, Color>();
    internal Dictionary<TMP_Text, Color> baseColor_Text = new Dictionary<TMP_Text, Color>();

    internal Dictionary<SpriteRenderer, Color> hoverColor_Sprite = new Dictionary<SpriteRenderer, Color>();
    internal Dictionary<TMP_Text, Color> hoverColor_Text = new Dictionary<TMP_Text, Color>();

    internal Dictionary<SpriteRenderer, Color> clickColor_Sprite = new Dictionary<SpriteRenderer, Color>();
    internal Dictionary<TMP_Text, Color> clickColor_Text = new Dictionary<TMP_Text, Color>();

    internal Dictionary<SpriteRenderer, Color> disabledColor_Sprite = new Dictionary<SpriteRenderer, Color>();
    internal Dictionary<TMP_Text, Color> disabledColor_Text = new Dictionary<TMP_Text, Color>();


    internal bool isHovering = false;
    internal bool isClicking = false;

    internal WorldspaceButton_State state = WorldspaceButton_State.none;

    internal bool canClickWithoutHover = false;


    private void Awake()
    {
        _Awake();
    }
    public virtual void _Awake()
    {
        // Prepare colors.
        foreach (SpriteRenderer sprite in sprites) baseColor_Sprite.Add(sprite, sprite.color);
        foreach (TMP_Text text in texts) baseColor_Text.Add(text, text.color);

        if (colorMode == WorldspaceButton_ColorMode.set)
        {
            foreach (SpriteRenderer sprite in sprites) hoverColor_Sprite.Add(sprite, hoverColor);
            foreach (TMP_Text text in texts) hoverColor_Text.Add(text, hoverColor);

            foreach (SpriteRenderer sprite in sprites) clickColor_Sprite.Add(sprite, clickColor);
            foreach (TMP_Text text in texts) clickColor_Text.Add(text, clickColor);

            foreach (SpriteRenderer sprite in sprites) disabledColor_Sprite.Add(sprite, disabledColor);
            foreach (TMP_Text text in texts) disabledColor_Text.Add(text, disabledColor);
        }
        else if (colorMode == WorldspaceButton_ColorMode.multiply)
        {
            foreach (SpriteRenderer sprite in sprites) hoverColor_Sprite.Add(sprite, sprite.color * hoverColor);
            foreach (TMP_Text text in texts) hoverColor_Text.Add(text, text.color * hoverColor);

            foreach (SpriteRenderer sprite in sprites) clickColor_Sprite.Add(sprite, sprite.color * clickColor);
            foreach (TMP_Text text in texts) clickColor_Text.Add(text, text.color * clickColor);

            foreach (SpriteRenderer sprite in sprites) disabledColor_Sprite.Add(sprite, sprite.color * disabledColor);
            foreach (TMP_Text text in texts) disabledColor_Text.Add(text, text.color * disabledColor);
        }



        // Set to disabled?
        if (startDisabled) ChangeState(WorldspaceButton_State.disabled);
    }

    private void Start()
    {
        _Start();
    }
    public virtual void _Start()
    {

    }

    private void Update()
    {
        if (!enabled) return;

        _Update();
    }
    public virtual void _Update()
    {
        // Do nothing if disabled.
        if (state == WorldspaceButton_State.disabled) return;

        // Update button color.
        if (isClicking && (isHovering || canClickWithoutHover)) ChangeState(WorldspaceButton_State.clicked);
        else if (isHovering) ChangeState(WorldspaceButton_State.hovered);
        else ChangeState(WorldspaceButton_State.none);
    }

    private void OnEnable()
    {
        isHovering = false;
        isClicking = false;
    }

    public void OnMouseEnter()
    {
        if (!enabled) return;

        isHovering = true;
        _OnMouseEnter();
    }
    public virtual void _OnMouseEnter()
    {

    }

    public void OnMouseExit()
    {
        if (!enabled) return;

        isHovering = false;
        _OnMouseExit();
    }
    public virtual void _OnMouseExit()
    {

    }

    public void OnMouseDown()
    {
        if (!enabled) return;

        isClicking = true;

        if (state == WorldspaceButton_State.disabled) return;

        _OnMouseDown();
    }
    public virtual void _OnMouseDown()
    {

    }

    public void OnMouseUp()
    {
        if (!enabled) return;

        isClicking = false;

        if (!isHovering && !canClickWithoutHover) return;
        if (state == WorldspaceButton_State.disabled) return;

        _OnMouseUp();
    }
    public virtual void _OnMouseUp()
    {
        onClick.Invoke();
    }



    public virtual void ChangeState(WorldspaceButton_State newState)
    {
        // Cancel if already that state.
        if (state == newState) return;

        // Change state.
        state = newState;
        switch (state)
        {
            case WorldspaceButton_State.none:
                foreach (SpriteRenderer sprite in sprites) sprite.color = baseColor_Sprite[sprite];
                foreach (TMP_Text text in texts) text.color = baseColor_Text[text];
                break;

            case WorldspaceButton_State.hovered:
                foreach (SpriteRenderer sprite in sprites) sprite.color = hoverColor_Sprite[sprite];
                foreach (TMP_Text text in texts) text.color = hoverColor_Text[text];
                break;

            case WorldspaceButton_State.clicked:
                foreach (SpriteRenderer sprite in sprites) sprite.color = clickColor_Sprite[sprite];
                foreach (TMP_Text text in texts) text.color = clickColor_Text[text];
                break;

            case WorldspaceButton_State.disabled:
                foreach (SpriteRenderer sprite in sprites) sprite.color = disabledColor_Sprite[sprite];
                foreach (TMP_Text text in texts) text.color = disabledColor_Text[text];
                break;
        }
    }
}

public enum WorldspaceButton_State { none, hovered, clicked, disabled }
public enum WorldspaceButton_ColorMode { set, multiply }