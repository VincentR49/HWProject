using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Base class for any houseObject
[RequireComponent(typeof(SpriteRenderer))]
public class HouseObject : MonoBehaviour {

    public GameObjectSet houseObjects;

    private void Awake()
	{
        houseObjects.Add(this.gameObject);
        Init();
    }


    public void OnDestroy()
    {
        houseObjects.Remove(this.gameObject);
    }

    private void Init()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Objects";
    }
}