using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Rotable : TileBasedBehaviour {

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

    // Destruction and creation of a new collider
    private void ResetCollider()
    {
        Debug.Log("Reset Collider, old width: " + Width + ", old height: " + Height);
        bool isTrigger = Cd2D.isTrigger;
        foreach (BoxCollider2D bc2D in GetComponents<BoxCollider2D>())
        {
            Destroy(bc2D); // to be sure to delet all the colliders
        }
        gameObject.AddComponent<BoxCollider2D>();
        Cd2D.isTrigger = isTrigger;
        Debug.Log("new width: " + Width + ", new height: " + Height);
    }
}
