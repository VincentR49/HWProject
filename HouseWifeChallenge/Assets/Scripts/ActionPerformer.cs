using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Abstract class defining the basics methods to interract with interactible objects
public abstract class ActionPerformer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker; 
	public static int minInteractionDistance = 1,
	
	
	// Execute the action. Cancel current action if one acion is currently running and is different from the given action.
	public void StartAction(Action action, GameObject interactiveObject)
	{
		if (action == null) return;
		if (!actionTracker.ActionIsRunning()) // no action is running
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
	
	// Execute at each frame
	protected void UpdateCurrentActionState()
	{
		if (actionTracker.ActionIsRunning())
		{
			if (!IsCloseEnough(actionTracker.interactible))
			{
				CancelAction (actionTracker.action, actionTracker.interactible);
			}
			else
			{
				UpdateActionTrackerProgress();
			}
		}
	}
	
	protected bool IsCloseEnough (GameObject interactiveObject)
	{
		return GetDistance (To2D(transform.position), To2D(interactiveObject.transform.position)) <= minInteractionDistance;
	}

	// Update the current progress of the action and finish the action if necessary
	private void UpdateActionTrackerProgress()
	{
		actionTracker.progress += Time.deltaTime;
		if (actionTracker.progress >= actionTracker.action.duration)
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
		actionTracker.progress = 0;
	}
	
	protected virtual void FinishAction(Action action, GameObject interactiveObject)
	{
		action.Finish (gameObject, interactiveObject);
		actionTracker.status = Status.Finished;
		actionTracker.progress = action.duration;
	}
	
	protected virtual void CancelAction(Action action, GameObject interactiveObject)
	{
		action.Cancel (gameObject, interactiveObject);
		actionTracker.progress = 0;
		actionTracker.status = Status.Waiting;
	}
}