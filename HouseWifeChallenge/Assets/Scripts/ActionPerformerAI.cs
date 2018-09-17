using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using UnityEngine.Tilemaps;

// Attach this to the player object in order to perform actions automatically from a given list
// Basic AI:
// on Sélectionne la première action de la liste comme action en cours
// On détecte le plus proche object permettant de faire l'action
// On se déplace vers cet objet
// On execute l'action
// Une fois l'action terminée, on l'enlève de la liste et le met dans la liste terminée

// TODO: que faire lorsque l'action sélectionnée ne peut être exécutée? -> passer à la suivante dans la liste
[RequireComponent(typeof(PlayerController))]
public class ActionPerformerAI : ActionPerformer
{
	// Note: has reference to ActionTracker by heritage
	[Tooltip("List of task to perform")]
	public ActionSet toDoActions;

    [Tooltip("List of completed tasks")]
    public ActionSet finishedActions;

    [Tooltip("List containing the reference to all the interactible object on the scene")]
	public GameObjectSet interactibleObjects;

    public GameEvent toDoListHasChanged;

	PlayerController playerController;

	// mettre ces attributs dans le actionTracker
	bool Busy => actionTracker.ActionIsRunning() || actionTracker.ActionWaitToBeExecuted();

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}

    private void Start()
    {
        actionTracker.Reset();
    }

    private void Update()
	{
        UpdateActionState();
        
        if (!Busy)
		{
            Action action = SelectFirstAction(); // take the first actiojn from the toDoList
			if (action != null)
			{
				StartAction (action); // Init action tracker and start to move the object 
			}
        }
    }

	// Start the selected action
	// Initiation the actionTracker and move to the closest object containing this action
    private void StartAction (Action action)
    {
		// search object in the scene that can do this action
        GameObject[] objects = ScanInteractibleObjects(action); 
        if (objects != null)
        {
            GameObject interactiveObject = GetClosestObject(objects);
			InitAction(action, interactiveObject);
			Debug.Log("Start action " + action.name);
            playerController.MoveToObject(interactiveObject);
        }
        else
        {
            Debug.Log("Couldnt find any object able to perform this action. Action impossible");
        } 
    }

	// Take the first action from the current toDoList
	// Return null if the list is empty
    private Action SelectFirstAction()
    {
        if (toDoActions.Items.Count > 0)
        {
            Action action = toDoActions.Items[0];
            if (action != null)
            {
                Debug.Log("Selected current action " + action.name);
            }
			return action;
        }
		else
		{
            return null;
		}
    }

	protected override void ExecuteAction ()
	{
		base.ExecuteAction ();
	}

    protected override void FinishAction ()
	{
		base.FinishAction ();
		MarkActionAsFinished (actionTracker.action);
    }

	private void MarkActionAsFinished(Action action)
	{
		toDoActions.Remove (action);
		finishedActions.Add (action);
        toDoListHasChanged.Raise();
    }

	
    protected override void CancelAction ()
	{
		base.CancelAction ();
    }
	
	// Execute when the doToListChanged
	public void OnToDoListChanged()
	{
		// Check if the first action changed
		// If so, canceled the current action and start a new one
		Action action = SelectFirstAction();
		if (action != null && action != actionTracker.action)
		{
			CancelAction(); // cancel current action
			StartAction (action); // start a new action
		}
	}

	// Get the closest object from the player in the list
	private GameObject GetClosestObject (GameObject[] gameObjects)
	{
		GameObject closestObject = null;
		double minDistance = Double.MaxValue;
		foreach (GameObject go in gameObjects)
		{
			double distance = GetDistance(To2D(transform.position), To2D(go.transform.position));
			if (distance < minDistance)
			{
				closestObject = go;
				minDistance = distance;
			}
		}
		return closestObject;
	}
	
	// Detect in the map the interactible objects containing the given action
	private GameObject[] ScanInteractibleObjects (Action action)
	{
		if (interactibleObjects.Items == null) return null;
		List<GameObject> gameObjects = new List<GameObject>();
		foreach (GameObject obj in interactibleObjects.Items)
		{
			Interactible interactible = obj.GetComponent<Interactible>();
			if (interactible != null)
			{
				if (interactible.action.GetType().Equals(action.GetType()))
				{
                    gameObjects.Add(obj);
				}
			}
			else
			{
				Debug.Log("Error: non interactible detected in the interactible object list.");
			}
		}
		return gameObjects.ToArray();
	}
}