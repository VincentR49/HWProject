using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generate the actions for the player
public class ActionGenerator : MonoBehaviour {

    public ActionSet toDoList;
    public GameEvent toDoListHasChanged;
    [Range(0,1)]
    [Tooltip("Number of action per second")]
    public float generationRate = .5f;

    public List<Action> possibleActions;

    private int ActionNumber => possibleActions == null ? 0 : possibleActions.Count;
    private float lastGenerationTime = 0f;
    Random rnd;

    private void Start()
    {
        rnd = new Random();
        toDoList.Items.Clear();
        GenerateNewAction(); 
    }

    private void Update()
    {
        lastGenerationTime += Time.deltaTime;
        if (lastGenerationTime > 1 / generationRate)
        {
            GenerateNewAction();
        }
    }


    private void GenerateNewAction()
    {
        Action action = GetRandomAction();
        if (action != null)
        {
            toDoList.Add(action);
            lastGenerationTime = 0;
            toDoListHasChanged.Raise();
            Debug.Log("Action " + action.name + " generated.");
        }
    }

    private Action GetRandomAction()
    {
        if (ActionNumber == 0)
        {
            return null;
        }
        else
        {
            Action prefab = possibleActions[Random.Range(0, ActionNumber)];
            return Instantiate(prefab);
        }
    }  

}
