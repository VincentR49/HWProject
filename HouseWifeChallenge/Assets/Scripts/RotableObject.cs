using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Enable the rotation of a 2D gameObject
[RequireComponent(typeof(Collider2D))]
public class RotableObject : TileBasedObject {

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
    private Orientation orientation; // current orientation of the gameObject

    new public void Start()
    {
        base.Start();
		InitSprite();
    }

	private void InitSprite()
	{
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
        if (enabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Rotate();
            }
        } 
    }

    Orientation GetNextOrientation()
    {
        Orientation newOrientation = orientation + 1;
        if (newOrientation > Orientation.Top)
        {
            newOrientation = Orientation.Right;
        }
        return newOrientation;
    }

    void Rotate()
    {
        Orientation oldOrientation = orientation;
        orientation = GetNextOrientation();
        UpdateSprite();
        if (!IsPositionFree(CurrentCell))
        {
            Debug.Log("Rotation canceled go back to old Orientation: ");
            orientation = oldOrientation;
            UpdateSprite(); 
        }
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
        if (newSprite != null && newSprite != sprite.sprite)
        {
            sprite.sprite = newSprite;
			ResetCollider();
        }
    }
}
