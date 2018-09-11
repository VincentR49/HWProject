using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.
using static Utils;

// Attach this to the player object in order to perform actions
public class ActionPerfomer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker; 
	private int lastUpdateTime;
	
	private void Update()
	{
		Action currentAction = actionTracker.action;
		
		if (actionTracker.action != null)
		{
			actionTracker.currentProgress += Time.deltaTime;
			if (actionTracker.currentProgress >= actionTracker.action)
			{
				FinishAction(currentAction, actionTracker.interactible);
			}
		}

		// TODO: to simplify (put inside function)
		if (Input.GetMouseButtonDown(1))
		{
			GameObject interactiveObject = GetColliderAtPosition(To2D(Input.mousePosition)); 
			if (interactiveObject != null)
			{
				Debug.Log("Clicked on object " + interactiveObject.name + " at position " + Input.mousePosition.ToString());
				Interactible interactible = gameObject.GetComponent<Interactible>();
				if (interactible != null)
				{
					// Check if an action is currently running
					Action objectAction = interactible.action;
					
					if (currentAction == null) // no action is running
					{
						ExecuteAction (action, interactiveObject);
					}
					else 
					{
						if (currentAction != objectAction)
						{
							CancelAction (currentAction, null); // reference to the object in interaction ??
						}
						else
						{
							// do nothing
						}
					}
				}
			}
		}
	}
	
	// Methods relative to action execution
	private void ExecuteAction(Action action, GameObject interactiveObject)
	{
		action.Execute (gameObject, interactiveObject); 
		actionTracker.isRunning = true;
	}
	
	private void FinishAction(Action action)
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