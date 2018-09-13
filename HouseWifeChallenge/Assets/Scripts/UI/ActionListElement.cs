using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionListElement : MonoBehaviour {

    public Action action;
    private Text text;
    private Image background;
    
	void Start () {
        background = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = action.name;
	}
	
    public Image GetBackground()
    {
        return background;
    }
}
