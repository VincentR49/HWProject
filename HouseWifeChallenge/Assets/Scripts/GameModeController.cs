using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class that enable / disable scripts related to the game mode
public class GameModeController : MonoBehaviour {

    public GameMode.Type startingMode;
    public GameMode gameMode;
    public String playerControlKey = "p";
    public String objectPlacementKey = "o";

    void Start () {
        ChangeGameMode(startingMode);
    }

    private void Update()
    {
        if (Input.GetKeyDown(playerControlKey))
        {
            ChangeGameMode(GameMode.Type.PlayerControl);
        }
        if (Input.GetKeyDown(objectPlacementKey))
        {
            ChangeGameMode(GameMode.Type.ObjectPlacement);
        }
    }


    private void ChangeGameMode(GameMode.Type type)
    {
        gameMode.SetValue(type);
    }
}
