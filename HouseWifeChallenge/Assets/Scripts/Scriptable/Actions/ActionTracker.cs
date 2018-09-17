using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Track the information about the action state
// TODO: private attributes ?
[CreateAssetMenu(menuName ="Scriptable Objects/Actions/Action Tracker")]
public class ActionTracker : ScriptableObject
{
	[Tooltip("Action currently running. Null if no action is running")]
    public Action action;
	
	[Tooltip("Current progress of the running action in second")]
	public float progress = 0;
	
	[Tooltip("Object performing the action (player)")]
	public GameObject performer; 
	
	[Tooltip("Object with wich the performer interacts")]
	public GameObject interactible;
	
	[Tooltip("Current Status of the action")]
	public Status status;
	
	public enum Status
	{
		Waiting, // action selected but waiting to be executed
		Running, // action currently running
		Finished // action finished (inactive)
	}
	
	public bool ActionIsRunning()
	{
		return action != null && status == Status.Running;
	}
	
	public bool ActionWaitToBeExecuted()
	{
		return action != null && status == Status.Waiting;
	}

    public void Reset()
    {
        action = null;
        progress = 0;
        performer = null;
        interactible = null;
        status = Status.Waiting;
    }
}

