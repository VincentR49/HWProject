using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manage the Action list UI panel
// In charge displaying and editing the actions ToDoList
public class ActionListUIManager : MonoBehaviour {

    public ActionSet toDoList;
    public ActionTracker currentTracker;
    public GameEvent toDoListChanged;
    public GameObject actionListElementPrefab;

    private List<GameObject> uiElements;
    private List<GameObject> placeHolders;
    private ActionSet lastToList;
    private GameObject tempDragContainer;
	private bool onDrag = false;
	
    private ActionListDraggableElement DraggedElement
    {
        get
        {
            if (tempDragContainer == null) return null;
            return tempDragContainer.transform.GetChild(0).gameObject.GetComponent<ActionListDraggableElement>();
        }
    }

    void OnEnable()
    {
        ActionListDraggableElement.DragBeginEventDelegate += OnListElementDragBegin;
        ActionListDraggableElement.DragEndEventDelegate += OnListElementDragEnd;
    }

    void OnDisable()
    {
        ActionListDraggableElement.DragBeginEventDelegate -= OnListElementDragBegin;
        ActionListDraggableElement.DragEndEventDelegate -= OnListElementDragEnd;
    }

    private void Start()
    {
        InitPlaceHolders();
        UpdateTaskListUI();
    }
	
    // Create an empty object at the end of the children tree hierarchy, to be visible over all the other elements
    private void CreateTempDragContainer()
    {
        DestroyTempDragContainer(); // to be sure
        Debug.Log("Create drag element container");
        tempDragContainer = new GameObject("Temp Drag Container");
        tempDragContainer.transform.SetParent (gameObject.transform);
        tempDragContainer.transform.SetAsLastSibling(); 
    }

    private void DestroyTempDragContainer()
    {
        Debug.Log("Destroy drag element container");
        if (tempDragContainer != null)
        {
            Destroy(tempDragContainer);
        }
    }

    void OnListElementDragBegin(ActionListDraggableElement source)
    {
        // Change the container of the UI Element (make it in front of all the other elements)
		onDrag = true;
        CreateTempDragContainer();
        source.transform.SetParent(tempDragContainer.transform);
    }

    void OnListElementDragEnd (ActionListDraggableElement source)
    {
        // Check if the UI element is included in the rect of another placeHolder
        // if this placeHolder is different, swap the two elements in the list
        onDrag = false;
        RectTransform sourceRect = source.gameObject.GetComponent<RectTransform>();
        Debug.Log("On drag End, source: " + source.gameObject.name + " position: " + sourceRect);
        int originalIndex = toDoList.Items.FindIndex(n => n == source.GetAction());
        for (int index = 0; index < placeHolders.Count; index++)
        {
            if (index == originalIndex) continue;
            RectTransform placeHolderRect = placeHolders[index].GetComponent<RectTransform>();
            if (placeHolderRect.Overlaps(sourceRect, true))
            {
                DestroyTempDragContainer();
                MoveListElementTo(originalIndex, index);
                return;
            }
        }
        source.gameObject.transform.SetParent(placeHolders[originalIndex].transform);
        source.ResetPosition();
        DestroyTempDragContainer();
    }

    public void MoveListElementTo(int oldIndex, int newIndex)
    {
        if (toDoList != null)
        {
            Debug.Log("Move list elements from " + oldIndex + " to " + newIndex + ")");
            newIndex = Mathf.Min(newIndex, toDoList.Items.Count - 1);
            toDoList.MoveElementTo(oldIndex, newIndex);
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

	
	public void OnToDoListHasChanged()
	{
		if (onDrag && DraggedElement != null)
		{
			if (!toDoList.Items.Contains(DraggedElement.GetAction()))
			{
                Debug.Log("Destroy temp container during drag");
				DestroyTempDragContainer();
			}
		}
		UpdateTaskListUI();
	}

    // TODO: to optimise
    private void UpdateTaskListUI()
    {
        ClearTaskListUI();
        uiElements = new List<GameObject>();
        if (toDoList != null && placeHolders != null)
        {
            int index = 0;
            int nActions = toDoList.Items.Count;
            foreach (GameObject placeHolder in placeHolders)
            {
                if (index >= nActions) break;
                Action action = toDoList.Items[index];
                if (onDrag && DraggedElement != null && DraggedElement.GetAction() == action)
                {
                    // if the action if on the dragged element, do nothing
                }
                else
                {
                    GameObject obj = Instantiate(actionListElementPrefab, placeHolder.transform);
                    ActionListDraggableElement actionListElement = obj.GetComponent<ActionListDraggableElement>();
                    actionListElement.SetAction(action);
                    uiElements.Add(obj); // store object reference
                }
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
