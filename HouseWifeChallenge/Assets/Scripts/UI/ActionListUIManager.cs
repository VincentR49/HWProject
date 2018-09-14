using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionListUIManager : MonoBehaviour {

    public ActionSet toDoList;
    public ActionTracker currentTracker;
    public GameEvent toDoListChanged;

    public Color currentActionColor = Color.yellow;
    public Color toDoColor = Color.cyan;

    public GameObject actionListElementPrefab;

    private List<GameObject> uiElements;
    private List<GameObject> placeHolders;
    private ActionSet lastToList;

    void OnEnable()
    {
        ActionListDraggableElement.OnDragEnd += OnListElementDragEnd;
    }


    void OnDisable()
    {
        ActionListDraggableElement.OnDragEnd -= OnListElementDragEnd;
    }

    private void Start()
    {
        InitPlaceHolders();
        UpdateTaskListUI();
    }

    void OnListElementDragEnd (ActionListDraggableElement source)
    {
        // Check if the UI element is included in the rect of another placeHolder
        // if this placeHolder is different, swap the two elements in the list
        RectTransform sourceRect = source.gameObject.GetComponent<RectTransform>();
        Debug.Log("On drag End, source: " + source.gameObject.name + " position: " + sourceRect);
        
        int originalIndex = source.GetIndex();
        for (int index = 0; index < placeHolders.Count; index++)
        {
            if (index == originalIndex) continue;
            RectTransform placeHolderRect = placeHolders[index].GetComponent<RectTransform>();
            if (placeHolderRect.Overlaps(sourceRect, true))
            {
                SwapListElement(originalIndex, index);
                return;
            }
        }
        source.ResetPosition();
    }

    public void SwapListElement(int index1, int index2)
    {
        if (toDoList != null)
        {
            Debug.Log("Swap list elements");
            toDoList.SwapPosition(index1, index2);
            toDoListChanged.Raise();
        } 
    }



    private void InitPlaceHolders()
    {
        placeHolders = new List<GameObject>();
        // get children of the object
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            placeHolders.Add(child);
            child.GetComponent<RawImage>().color = new Color(0, 0, 0, 0); // set place Holder invisible
        }
    }

    private void Update()
    {
        
    }

    // TODO: to optimise
    public void UpdateTaskListUI()
    {
        ClearTaskListUI();
        if (toDoList != null && placeHolders != null)
        {
            int index = 0;
            int nActions = toDoList.Items.Count;
            foreach (GameObject placeHolder in placeHolders)
            {
                if (index >= nActions) break;
                GameObject obj = Instantiate (actionListElementPrefab, placeHolder.transform);
                ActionListDraggableElement actionListElement = obj.GetComponent<ActionListDraggableElement>();
                actionListElement.SetAction (toDoList.Items[index]);
                actionListElement.SetIndex (index);
                index++;
            }
        }
    }

    // clean all the children of the placeHolders
    private void ClearTaskListUI()
    {
        if (placeHolders != null)
        {
            foreach (GameObject placeHolder in placeHolders)
            {
                int nChildren = placeHolder.transform.childCount;
                for (int i = 0; i < nChildren; i++)
                {
                    Destroy(placeHolder.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
