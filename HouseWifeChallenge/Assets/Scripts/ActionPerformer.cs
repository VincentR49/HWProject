using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Attach this to the player object in order to perform actions
public class ActionPerformer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker; 
	private int lastUpdateTime;
	
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

		// TODO: to simplify (put inside function)
		if (Input.GetMouseButtonDown(1))
		{
            Vector2 mousePosition = To2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GameObject interactiveObject = GetColliderAtPosition(mousePosition); 
			if (interactiveObject != null)
			{
				Debug.Log("Clicked on object " + interactiveObject.name + " at position " + mousePosition.ToString());
				Interactible interactible = interactiveObject.GetComponent<Interactible>();
				if (interactible != null)
				{
					// Check if an action is currently running
					Action objectAction = interactible.action;
					if (actionTracker.action == null) // no action is running
					{
						ExecuteAction (objectAction, interactiveObject);
					}
					else 
					{
						if (actionTracker.action != objectAction)
						{
							CancelAction (actionTracker.action, null); // reference to the object in interaction ??
							ExecuteAction (objectAction, interactiveObject);
						}
					}
				}
			}
		}
	}
	
	private void UpdateActionTracker()
	{
		actionTracker.currentProgress += Time.deltaTime;
		if (actionTracker.currentProgress >= actionTracker.action.duration)
		{
			FinishAction(actionTracker.action, actionTracker.interactible);
		}
	}
	
	// Methods relative to action execution
	private void ExecuteAction(Action action, GameObject interactiveObject)
	{
		action.Execute (gameObject, interactiveObject); 
		actionTracker.action = action;
		actionTracker.performer = gameObject;
		actionTracker.interactible = interactiveObject;
	}
	
	private void FinishAction(Action action, GameObject interactiveObject)
	{
		action.Finish (gameObject, interactiveObject);
		actionTracker.Reset();
	}
	
	private void CancelAction(Action action, GameObject interactiveObject)
	{
		action.Cancel (gameObject, interactiveObject);
		actionTracker.Reset();
	}
}