using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Base class for any houseObject
[RequireComponent(typeof(SpriteRenderer))]
public class HouseObject : MonoBehaviour {

    public static GameObject houseObjects;
	
	private void Awake()
	{
		if (houseObjects == null)
		{
			houseObjects = new GameObject("HouseObjects");
		}
        this.transform.parent = houseObjects.transform;
    }
	
    public void Reset()
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<MovableObject>();
		gameObject.AddComponent<RotableObject>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Objects";
    }
}