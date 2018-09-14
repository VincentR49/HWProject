using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ActionListDraggableElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField]
    private Action action;
    public Color dragColor = Color.red;

    public delegate void DragEndAction(ActionListDraggableElement source); // use delegate to let the listener implements the method
    public static event DragEndAction OnDragEnd;

    private Text text;
    private RawImage background;
    private Color beforeDragColor;
    private int index = 0;

	private void Awake () {
        background = GetComponent<RawImage>();
        text = transform.GetChild(0).GetComponent<Text>();
        UpdateContent();
    }

    private void Start()
    {
        ResetPosition();
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
        beforeDragColor = background.color;
        background.color = dragColor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        background.color = beforeDragColor;
        OnDragEnd?.Invoke(this); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
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


