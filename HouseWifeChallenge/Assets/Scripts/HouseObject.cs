using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for any houseObject
public class HouseObject : MonoBehaviour {

	public static GameObject houseObjects;
	
	private void Awake()
	{
		if (houseObjects == null)
		{
			houseObjects = new GameObject("HouseObjects");
		}
		this.transform.parent = houseObjects;
	}
	
    public void Start()
    {
		gameObject.AddComponent<MovableObject>();
		gameObject.AddComponent<RotableObject>();
		gameObject.AddComponent<BoxCollider2D>();
    }
}