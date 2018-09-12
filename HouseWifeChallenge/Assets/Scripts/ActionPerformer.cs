using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Abstract class defining the basics methods to interract with interactible objects
public abstract class ActionPerformer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker; 
	
	// Execute the action. Cancel current action if one acion is currently running and is different from the given action.
	public void StartAction(Action action)
	{
		if (action == null) return;
		if (actionTracker.action == null) // no action is running
		{
			ExecuteAction (action, interactiveObject);
		}
		else 
		{
			if (actionTracker.action != action)
			{
				CancelAction (actionTracker.action, null); // reference to the object in interaction ??
				ExecuteAction (action, interactiveObject);
			}
		}
	}
	
	// Update the current progress of the action and finish the action if necessary
	protected void UpdateActionTracker()
	{
		actionTracker.currentProgress += Time.deltaTime;
		if (actionTracker.currentProgress >= actionTracker.action.duration)
		{
			FinishAction(actionTracker.action, actionTracker.interactible);
		}
	}

	protected virtual void ExecuteAction(Action action, GameObject interactiveObject)
	{
		action.Execute (gameObject, interactiveObject); 
		actionTracker.action = action;
		actionTracker.performer = gameObject;
		actionTracker.interactible = interactiveObject;
	}
	
	protected virtual void FinishAction(Action action, GameObject interactiveObject)
	{
		action.Finish (gameObject, interactiveObject);
		actionTracker.Reset();
	}
	
	protected virtual void CancelAction(Action action, GameObject interactiveObject)
	{
		action.Cancel (gameObject, interactiveObject);
		actionTracker.Reset();
	}
}