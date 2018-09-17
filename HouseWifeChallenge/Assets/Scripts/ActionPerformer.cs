using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Abstract class defining the basics methods to interract with interactible objects
[RequireComponent(typeof(PlayerController))]
public abstract class ActionPerformer : MonoBehaviour
{
	[Tooltip("Reference to the object storing the information about the current action")]
	public ActionTracker actionTracker;

    [Tooltip("Distance minimal from the object and performer to be able to interact")]
    public double minInteractionDistance = 1.3;

	// Check the status of the action tracker and execute, cancel or update the current action
	protected void UpdateActionState()
	{
		if (actionTracker.ActionIsRunning())
		{
			// if the action is running, update current progress
			if (IsCloseEnough(actionTracker.interactible))
			{
				actionTracker.progress += Time.deltaTime;
				if (actionTracker.progress >= actionTracker.action.duration)
				{
					FinishAction();
				}
			}
			else // if we are too far from the object, the current action is canceled
			{
				CancelAction ();
			}
		}
		else if (actionTracker.ActionWaitToBeExecuted() 
				 && IsCloseEnough(actionTracker.interactible))
		{
			ExecuteAction();
		}
		else
		{
			// do nothing
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

	// Initiate action. Cancel current action if one acion is currently running and is different from the given action.
	public void InitAction(Action action, GameObject interactiveObject)
	{
		if (actionTracker.ActionIsRunning()) // no action running
		{
			CancelAction ();
		}
		InitActionTracker(action, interactiveObject);
	}
	
	// Initialisation of the action tracker
	// Set all the information about the currentAction
	public void InitActionTracker (Action action, GameObject interactiveObject)
	{
		actionTracker.action = action;
		actionTracker.performer = gameObject;
		actionTracker.interactible = interactiveObject;
		actionTracker.progress = 0;
		actionTracker.status = ActionTracker.Status.Waiting;
	}
	
	// Execute the action stored in the actionTracker
	protected virtual void ExecuteAction()
	{
        actionTracker.status = ActionTracker.Status.Running;
		actionTracker.action.Execute (gameObject, interactiveObject); // execute action
	}
	
	// Finish the action stored in the actionTracker
	protected virtual void FinishAction()
	{
		actionTracker.status = ActionTracker.Status.Finished;
		actionTracker.progress = action.duration;
		actionTracker.action.Finish (gameObject, actionTracker.interactible); // finish action
	}
	
	// Cancel the action stored in the actionTracker
	protected virtual void CancelAction()
	{
		actionTracker.progress = 0;
		actionTracker.status = ActionTracker.Status.Waiting;
		actionTracker.action.Cancel (gameObject, actionTracker.interactible); // cancel action
	}
}