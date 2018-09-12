using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to any object that can ve interactible 
// Action container
public class Interactible : MonoBehaviour
{
	public Action action;
	public GameObjectSet interactibleObjectList;
	
	public void Start()
	{
		if (interactibleObjectList != null)
		{
			interactibleObjectList.Add(gameObject);
		}
	}
	
	public void OnDestroy()
	{
		if (interactibleObjectList != null)
		{
			interactibleObjectList.Remove(gameObject);
		}
	}
}
