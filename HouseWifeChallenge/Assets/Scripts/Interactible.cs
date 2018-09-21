using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to any object that can ve interactible 
// Action container
public class Interactible : MonoBehaviour
{
	public List<Action> actions;
	public GameObjectSet interactibleObjectList;
	
	public void Start()
	{
		if (interactibleObjectList != null)
		{
            Debug.Log("Object (" + name + ") added to interactible object list (" + interactibleObjectList.name + ")");
			interactibleObjectList.Add(gameObject);
		}
	}
	
	public void OnDestroy()
	{
		if (interactibleObjectList != null)
		{
            Debug.Log("Object (" + name + ") removed from interactible object list (" + interactibleObjectList.name + ")");
            interactibleObjectList.Remove(gameObject);
		}
	}
}
