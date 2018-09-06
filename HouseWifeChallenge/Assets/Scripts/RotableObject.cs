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
    private Dictionary<Orientation, Sprite> spriteDic;

    new public void Start()
    {
        base.Start();
        spriteDic = new Dictionary<Orientation, Sprite>
        {
            { Orientation.Right, spriteRight },
            { Orientation.Left, spriteLeft },
            { Orientation.Top, spriteTop },
            { Orientation.Down, spriteDown }
        };
        InitSprite();
    }

	private void InitSprite()
	{
        orientation = Orientation.Right;
        foreach (KeyValuePair<Orientation, Sprite> keySet in spriteDic)
        {
            if (keySet.Value == sprite.sprite)
            {
                orientation = keySet.Key;
                break;
            }
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
        Sprite newSprite = spriteDic[orientation];
        if (newSprite != null && newSprite != sprite.sprite)
        {
            sprite.sprite = newSprite;
			ResetCollider();
        }
    }
}
