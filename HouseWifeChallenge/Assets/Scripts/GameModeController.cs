using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class that enable / disable scripts related to the game mode
public class GameModeController : MonoBehaviour {

    public GameMode.Type startingMode;
    public GameMode gameMode;

    // Use this for initialization
    void Start () {
        ChangeGameMode(startingMode);
    }


    private void ChangeGameMode(GameMode.Type type)
    {
        gameMode.Value = type;
    }
}
