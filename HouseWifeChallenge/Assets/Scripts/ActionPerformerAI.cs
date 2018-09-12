using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Attach this to the player object in order to perform actions automatically from a given list
[RequireComponent(typeof(PlayerController))]
public class ActionPerformerAI : ActionPerformer
{

	// Note: has reference to ActionTracker by heritage
	
	[Tooltip("List of task to perform")]
	public ActionSet actionList;
	
	[Tooltip("List containing the reference to all the interactible object on the scene")]
	public GameObjectSet interactibleObjects;
	
	PlayerController playerController;
	bool isBusy = false;
	
	private void Start()
	{
		playerController = GetComponent<PlayerController>();
	}
	
	private void Update()
	{
		if (!actionTracker.action != null !isBusy)
		{
			
		}
	}
	
	private override void ExecuteAction (Action action, GameObject interactiveObject)
	{
		base.ExecuteAction (action, interactiveObject);
		// TODO à spécifier
	}
	
	private override void FinishAction (Action action, GameObject interactiveObject)
	{
		base.FinishAction (action, interactiveObject);
		// TODO à spécifier
	}
	
	private override void CancelAction (Action action, GameObject interactiveObject)
	{
		base.CancelAction (action, interactiveObject);
		// TODO à spécifier
	}
}