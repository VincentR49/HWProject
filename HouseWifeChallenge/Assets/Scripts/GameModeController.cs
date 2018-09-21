using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class that enable / disable scripts related to the game mode
public class GameModeController : MonoBehaviour {

    public GameModeType startingMode;

    [Tooltip("Reference to the current game mode")]
    public GameMode gameMode; 

    public String noneModeKey = "l";
    public String playerControlKey = "p";
    public String playerControlAIKey = "m";
    public String objectPlacementKey = "o";
	
    void Start () {
        ChangeGameMode(startingMode);
    }

    private void Update()
    {
		if (Input.GetKeyDown(noneModeKey))
		{
			ChangeGameMode(GameModeType.None);
		}
		
		if (Input.GetKeyDown(playerControlKey))
		{
			ChangeGameMode(GameModeType.PlayerControl);
		}
		
		if (Input.GetKeyDown(playerControlAIKey))
		{
			ChangeGameMode(GameModeType.PlayerControlAI);
		}
		
		if (Input.GetKeyDown(objectPlacementKey))
		{
			ChangeGameMode(GameModeType.ObjectPlacement);
		}
    }

    private void ChangeGameMode(GameModeType type)
    {
        gameMode.SetValue(type);
    }
}
