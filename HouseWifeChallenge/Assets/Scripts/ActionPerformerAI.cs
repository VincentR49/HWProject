using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Attach this to the player object in order to perform actions automatically from a given list
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PathFindingManager))]
public class ActionPerformerAI : ActionPerformer
{
	// Note: has reference to ActionTracker by heritage
	
	[Tooltip("List of task to perform")]
	public ActionSet actionList;
	
	[Tooltip("List containing the reference to all the interactible object on the scene")]
	public GameObjectSet interactibleObjects;
	
	PlayerController playerController;
	bool busy = false;
	private Action currentAction;
	
	
	private void Start()
	{
		playerController = GetComponent<PlayerController>();
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
        if (!busy && actionList.Items.Count > 0)
		{
		
		
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
		// TODO à spécifier
	}

    protected override void CancelAction (Action action, GameObject interactiveObject)
	{
		base.CancelAction (action, interactiveObject);
		// TODO à spécifier
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
	private GameObject[] GetInteractibleObjects (Action action)
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