using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;
using UnityEngine.Tilemaps;

// Attach this to the player object in order to perform actions automatically from a given list
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PathFindingManager))]
public class ActionPerformerAI : ActionPerformer
{
	// Note: has reference to ActionTracker by heritage
	[Tooltip("List of task to perform")]
	public ActionSet toDoActions;

    [Tooltip("List of completed tasks")]
    public ActionSet finishedActions;

    [Tooltip("List containing the reference to all the interactible object on the scene")]
	public GameObjectSet interactibleObjects;
	
	PlayerController playerController;
    PathFindingManager pathFindingManager;

	bool busy = false;
    private Action currentAction;
    private GameObject targetObject; // object target of the action

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
        pathFindingManager = GetComponent<PathFindingManager>();
	}
	
	private void Update()
	{
        UpdateCurrentActionState();
        // TODO: à spécifier
        // Si not busy et que la liste d'action en cours et non vide
        // on Sélectionne la première action de la liste comme action en cours
        // On détecte le plus proche object permettant de faire l'action
        // On se déplace vers cet objet
        // On execute l'action
        // Une fois l'action terminée, on l'enlève de la liste
        if (!busy)
		{
            SelectCurrentAction();
            StartCurrentAction();
        }
        else // busy mode
        {
            if (currentAction != null && targetObject != null)
            {
                if (IsCloseEnough(targetObject))
                {
                    StartAction(currentAction, targetObject);
                }
            }
            else
            {
                ResetCurrentAction();
            }
        }
    }

    private void StartCurrentAction()
    {
        GameObject[] objects = ScanInteractibleObjects(currentAction); // search object that can do this action
        if (objects != null)
        {
            Debug.Log("Start action " + currentAction.name);
            GameObject interactiveObject = GetClosestObject(objects);
            playerController.MoveToObject(interactiveObject);
            targetObject = interactiveObject;
            busy = true;
        }
        else
        {
            ResetCurrentAction();
            Debug.Log("Couldnt find any object able to perform this action. Action impossible");
        } 
    }

    private void SelectCurrentAction()
    {
        ResetCurrentAction();
        if (toDoActions.Items.Count == 0)
        {
            currentAction = null;
        }
        else
        {
            currentAction = toDoActions.Items[0];
            Debug.Log("Selected current action " + currentAction.name);
        }
    }

	protected override void ExecuteAction (Action action, GameObject interactiveObject)
	{
		base.ExecuteAction (action, interactiveObject);
		// TODO à spécifier
	}

    protected override void FinishAction (Action action, GameObject interactiveObject)
	{
		base.FinishAction (action, interactiveObject);
        ResetCurrentAction();

    }

    protected override void CancelAction (Action action, GameObject interactiveObject)
	{
		base.CancelAction (action, interactiveObject);
        ResetCurrentAction();
    }
	
    private void ResetCurrentAction()
    {
        currentAction = null;
        targetObject = null;
        busy = false;
    }

	
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
				if (interactible.action == action)
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