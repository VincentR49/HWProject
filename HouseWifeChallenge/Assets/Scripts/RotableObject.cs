using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enable the rotation of a 2D gameObject
public class RotableObject : MonoBehaviour {

    public enum Orientation
    {
        Right,
        Down,
        Left,
        Top    
    }
    
    public Sprite spriteRight;
    public Sprite spriteLeft;
    public Sprite spriteTop;
    public Sprite spriteDown;
    private SpriteRenderer sprite;
    private Orientation orientation; // current orientation of the gameObject

    private void Start()
    {
        Init();
    }

    void Init ()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite.sprite == spriteLeft)
        {
            orientation = Orientation.Left;
        }
        else if (sprite.sprite == spriteTop)
        {
            orientation = Orientation.Top;
        }
        else if (sprite.sprite == spriteDown)
        {
            orientation = Orientation.Down;
        }
        else // right
        {
            orientation = Orientation.Right;
        }
        UpdateSprite();
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Rotate();
        }
    }

    void Rotate()
    {
        Orientation newOrientation = orientation + 1;
        if (newOrientation > Orientation.Top)
        {
            newOrientation = Orientation.Right;
        }
        orientation = newOrientation;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        Sprite newSprite;
        switch (orientation)
        {
            case Orientation.Left:
                newSprite = spriteLeft;
                break;
            case Orientation.Top:
                newSprite = spriteTop;
                break;
            case Orientation.Down:
                newSprite = spriteDown;
                break;
            case Orientation.Right:
            default:
                newSprite = spriteRight;
                break;
        }
        if (newSprite != null)
        {
            sprite.sprite = newSprite;
        }
    }
}
