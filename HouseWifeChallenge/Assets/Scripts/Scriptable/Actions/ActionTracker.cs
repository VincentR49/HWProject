using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Track the information about the action state
// TODO: private attributes ?
public class ActionTracker : ScriptableObject
{
	[Tooltip("Action currently running. Null if 
    Action action;
	
	[Tooltip("Current progress of the running action in second")]
	public int currentProgress = 0;
	
	[Tooltip("Object performing the action (player)")]
	public GameObject performer; 
	
	[Tooltip("Object with wich the performer interacts")]
	public GameObject interactible;
	
	
	public void OnEnable()
	{
		Reset();
	}
	
	public void Reset()
	{	
		action = null;
		currentProgress = 0;
		performer = null;
		interactible = null;
	}
}

