using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GetGameModeText : MonoBehaviour {

    public GameMode gameMode;
    private Text text;

    public void Awake()
    {
        text = GetComponent<Text>();
    }

    public void UpdateText()
    {
        text.text = gameMode.GetValue().GetString();
    }
}
