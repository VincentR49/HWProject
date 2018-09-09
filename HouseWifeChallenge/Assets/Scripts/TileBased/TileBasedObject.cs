using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class TileBasedObject : MonoBehaviour
{

    public TileMapVariable worldMap;
    protected BoxCollider2D Cd2D => GetComponent<BoxCollider2D>(); 
    protected float Width => sprite.bounds.size.x;
    protected float Height => sprite.bounds.size.y;
    protected Tilemap WorldMap => worldMap.Value;
    protected SpriteRenderer sprite;
    protected Vector3Int CurrentCell => WorldMap.WorldToCell(transform.position);

    // Use this for initialization
    public void Start () {
        sprite = GetComponent<SpriteRenderer>();   
    }

    // Detect if the given cell is free
    // Take into account the size of the object
    public bool IsPositionFree(Vector3Int cellPosition)
    {
        bool isOk = true; 
        for (int x = 0; x < Mathf.Ceil(Width); x++)
        {
            for (int y = 0; y < Mathf.Ceil(Height); y++)
            {
                isOk &= IsTileFree(cellPosition + Vector3Int.right * x + Vector3Int.up * y);
            }
        }
        return isOk;
    }

    // Check if there is a collider in the current cell
    public bool IsTileFree(Vector3Int cellPosition)
    {
        if (!WorldMap.HasTile(cellPosition))
        {
            return false;
        }
        Vector3 cellWorldPosition = WorldMap.GetCellCenterWorld(cellPosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cellWorldPosition, WorldMap.cellSize.x / 4);
        if (colliders.Length == 1 && colliders[0].gameObject == gameObject)
        {
            return true;
        }
        return colliders.Length == 0;
    }
}
