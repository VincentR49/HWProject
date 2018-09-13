using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Abstract class defining the basics methods to interract with interactible objects
public abstract class ActionPerformer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker;

    [Tooltip("Distance minimal from the object and performer to be able to interact")]
    public double minInteractionDistance = 1.3;
	
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
				CancelAction (actionTracker.action, actionTracker.performer); // reference to the object in interaction ??
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
	
    // Check if the current game object is close enough from the interactible object
	protected bool IsCloseEnough (GameObject interactiveObject)
	{
        float radius = (float)minInteractionDistance / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == interactiveObject)
            {
                return true;
            }
        }
        return false;
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
        actionTracker.status = IsCloseEnough(interactiveObject) ? ActionTracker.Status.Running : ActionTracker.Status.Waiting;
	}
	
	protected virtual void FinishAction(Action action, GameObject interactiveObject)
	{
		action.Finish (gameObject, interactiveObject);
		actionTracker.status = ActionTracker.Status.Finished;
		actionTracker.progress = action.duration;
	}
	
	protected virtual void CancelAction(Action action, GameObject interactiveObject)
	{
		action.Cancel (gameObject, interactiveObject);
		actionTracker.progress = 0;
		actionTracker.status = ActionTracker.Status.Waiting;
	}
}