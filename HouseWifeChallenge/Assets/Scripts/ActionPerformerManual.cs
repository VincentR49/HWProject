using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Attach this to the player object in order to perform actions manually by clicking on the object
// TODO: go to the object if the object is too far from the player
[RequireComponent(typeof(PlayerController))]
public class ActionPerformerManual : ActionPerformer
{
	PlayerController playerController;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}
	
	private void Update()
	{
        UpdateActionState();
        if (Input.GetMouseButtonDown(1))
		{
            Vector2 mousePosition = To2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameObject interactiveObject = GetInteractiveObjectAtPosition(mousePosition);
			if (interactiveObject != null)
			{
				InitAction(interactiveObject.GetComponent<Interactible>().actions[0], interactiveObject);
				playerController.MoveToObject(interactiveObject);
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