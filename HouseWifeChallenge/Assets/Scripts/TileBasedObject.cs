using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class TileBasedObject : MonoBehaviour {

    protected BoxCollider2D Cd2D => GetComponent<BoxCollider2D>(); 
    protected float Width => sprite.bounds.size.x;
    protected float Height => sprite.bounds.size.y;
    protected Tilemap worldMap;
    protected SpriteRenderer sprite;
    protected Vector3Int CurrentCell => worldMap.WorldToCell(transform.position);

    // Use this for initialization
    public void Start () {
        worldMap = GameManager.instance.worldMap;
        sprite = GetComponent<SpriteRenderer>();   
    }

    // Destruction and creation of a new collider
    protected void ResetCollider()
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

    public void DisableColliders()
    {
        Cd2D.isTrigger = true;
    }

    public void EnableColliders()
    {
        Cd2D.isTrigger = false;
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
                isOk &= HasCollider(cellPosition + Vector3Int.right * x + Vector3Int.up * y);
            }
        }
        return isOk;
    }

    // Check if there is a collider in the current cell
    public bool HasCollider(Vector3Int cellPosition)
    {
        if (!worldMap.HasTile(cellPosition))
        {
            return false;
        }
        Vector3 cellWorldPosition = worldMap.GetCellCenterWorld(cellPosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cellWorldPosition, worldMap.cellSize.x / 4);
        if (colliders.Length == 1 && colliders[0].gameObject == gameObject)
        {
            return true;
        }
        return colliders.Length == 0;
    }
}
