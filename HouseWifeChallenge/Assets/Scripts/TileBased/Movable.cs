using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


// Enable to move a 2D gameObject by clicking on it
public class Movable : TileBasedObject {
 
    public Color moveColorOk = new Color(0, 1, 0, 0.5f);
    public Color moveColorNOk = new Color(1, 0, 0, 0.5f);
    private Color initColor;
    private Vector2 initPosition;
    bool isMoving;

    new public void Awake()
    {
        base.Awake();
        initColor = sprite.color;
    }

    public void Update()
    {
        if (isMoving)
        {
            OnMove();
        }
    }

    public void OnMouseDown()
    {
        if (enabled)
        {
            if (isMoving)
            {
                OnEndMove();
            }
            else
            {
                OnBeginMove();
            }
        }
    }

    public void OnBeginMove()
    {
        isMoving = true;
        initPosition = new Vector2(transform.position.x, transform.position.y);
        DisableColliders();
    }

    public void OnMove()
    {
        Vector3Int cellPosition = GetCellPositionFromMouseInput();
        sprite.color = IsPositionFree(cellPosition) ? moveColorOk : moveColorNOk;
        Vector3 cellWorldPosition = WorldMap.GetCellCenterWorld(cellPosition);
        transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, transform.position.z);
    }

    public void OnEndMove()
    {
        Vector3Int cellPosition = GetCellPositionFromMouseInput();
        if (IsPositionFree(cellPosition))
        {
            EnableColliders();
            isMoving = false;
            sprite.color = initColor;
        }
    }

	// TO change -> change the Vector3 to Vector3
    public Vector3Int GetCellPositionFromMouseInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return WorldMap.WorldToCell(mousePosition);
    }

    private void DisableColliders()
    {
        Cd2D.isTrigger = true;
    }

    private void EnableColliders()
    {
        Cd2D.isTrigger = false;
    }
}
