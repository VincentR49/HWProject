using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GetGameModeText : MonoBehaviour {


    public GameMode gameMode;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void UpdateText()
    {
        string textToShow = "None";
        switch(gameMode.Value)
        {
            case GameMode.Type.PlayerControl:
                textToShow = "Player Controller";
                break;
            case GameMode.Type.ObjectPlacement:
                textToShow = "Object Placement";
                break;
        }
        text.text = textToShow;
    }
}
