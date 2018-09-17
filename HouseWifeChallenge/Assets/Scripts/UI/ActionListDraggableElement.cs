using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UI element representing one action
// Enable drag behaviour and generate event when the element is dragged
public class ActionListDraggableElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField]
    private Action action;
    public Color dragColor = Color.red;
    public Color currentActionColor = Color.yellow;
    public ActionTracker currentTracker;


    // Drag events
    public delegate void DragEndAction (ActionListDraggableElement source); // use delegate to let the listener implements the method
    public static event DragEndAction DragEndEventDelegate;
    public delegate void DragAction (ActionListDraggableElement source);
    public static event DragAction DragEventDelegate;
    public delegate void DragBeginAction (ActionListDraggableElement source);
    public static event DragBeginAction DragBeginEventDelegate;

    private Text text;
    private RawImage background;
    private Color initColor;
    private int index = 0;

	private void Awake () {
        background = GetComponent<RawImage>();
        text = transform.GetChild(0).GetComponent<Text>();
        initColor = background.color;
        UpdateContent();
    }

    private void Start()
    {
        ResetPosition();
    }

    private void Update()
    {
        
        if (currentTracker.action == action)
        {
            background.color = currentActionColor;
        }
        else
        {
            background.color = initColor;
        }
        
    }

    public void ResetToInitColor()
    {
        background.color = initColor;
    }

    public void ChangeColor(Color color)
    {
        background.color = color;
    }

    public void SetAction(Action action)
    {
        this.action = action;
        UpdateContent();
    }

    public void UpdateContent()
    {
        if (action != null)
        {
            text.text = action.name;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        background.color = dragColor;
        DragBeginEventDelegate?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetToInitColor();
        DragEndEventDelegate?.Invoke(this); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        DragEventDelegate?.Invoke(this);
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public int GetIndex() => index;

    public void SetIndex(int index)
    {
        this.index = index;
    }
}


