using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Attach this to the player object in order to perform actions manually by clicking on the object
public class ActionPerformerManual : ActionPerformer
{
	private void Start()
	{
		actionTracker.Reset();
	}

	private void Update()
	{
		if (actionTracker.action != null)
		{
			UpdateActionTracker();
		}
		
		if (Input.GetMouseButtonDown(1))
		{
            Vector2 mousePosition = To2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameObject interactiveObject = GetInteractiveObjectAtPosition(mousePosition);
			if (interactiveObject != null)
			{
				StartAction(interactiveObject.GetComponent<Interactible>().action, interactiveObject);
			}
		}
	}
	
	public GameObject GetInteractiveObjectAtPosition(Vector2 worldPosition)
	{
		GameObject obj = GetColliderAtPosition(worldPosition); 
		if (obj != null)
		{
			Debug.Log("Clicked on object " + obj.name + " at position " + worldPosition.ToString());
			return obj.GetComponent<Interactible>() != null ? obj : null;
		}
		return null;
	}
}