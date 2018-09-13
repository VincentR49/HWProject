using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListUIManager : MonoBehaviour {

    public ActionSet toDoList;
    public ActionSet finishedList;
    public ActionTracker currentTracker;
    public GameObject actionListElementPrefab;

    public Color finishedColor = Color.green;
    public Color currentActionColor = Color.yellow;
    public Color toDoColor = Color.cyan;

    private List<GameObject> uiElements;



    private void Start()
    {
        
    }

    private void Update()
    {
        
    }



}
