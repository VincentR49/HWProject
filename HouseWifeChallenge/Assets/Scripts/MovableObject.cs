using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(Collider2D))]
public class MovableObject : MonoBehaviour {

    private Tilemap worldMap;
    public Color moveColorOk = new Color(0, 1, 0, 0.5f);
    public Color moveColorNOk = new Color(1, 0, 0, 0.5f);
    private SpriteRenderer sprite;
    private Collider2D cd2D;
    private Color initColor;
    private Vector2 initPosition;
    bool isMoving;
    Vector2 size;

    public void Start()
    {
        worldMap = GameManager.instance.worldMap;
        sprite = GetComponent<SpriteRenderer>();
        initColor = sprite.color;
        cd2D = GetComponent<Collider2D>();
        size = new Vector2(cd2D.bounds.size.x, cd2D.bounds.size.y);
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
        if (isMoving)
        {
            OnEndMove();
        }
        else
        {
            OnBeginMove();
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
        sprite.color = IsFreeCell(cellPosition) ? moveColorOk : moveColorNOk;
        Vector3 cellWorldPosition = worldMap.GetCellCenterWorld(cellPosition);
        transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, transform.position.z);
    }

    public void OnEndMove()
    {
        Vector3Int cellPosition = GetCellPositionFromMouseInput();
        if (IsFreeCell(cellPosition))
        {
            EnableColliders();
            isMoving = false;
            sprite.color = initColor;
        }
    }

    public Vector3Int GetCellPositionFromMouseInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return worldMap.WorldToCell(mousePosition);
    }


    public void DisableColliders()
    {
        cd2D.isTrigger = true;
    }

    public void EnableColliders()
    {
        cd2D.isTrigger = false;
    }

    public bool IsFreeCell(Vector3Int cellPosition)
    {
        // Take into account the size of the object
        bool isOk = true;
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                isOk &= HasCollider(cellPosition + Vector3Int.right * x + Vector3Int.up * y);
            }
        }
        return isOk;
    }

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
