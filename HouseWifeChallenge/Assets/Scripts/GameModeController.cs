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

    private Dictionary<GameModeType, String> dictionaryKeyGameMode;

    void Start () {
        ChangeGameMode(startingMode);
        InitKeyDictionary();
    }

    private void Update()
    {
        foreach (KeyValuePair<GameModeType, String> entry in dictionaryKeyGameMode)
        {
            if (Input.GetKeyDown(entry.Value))
            {
                ChangeGameMode(entry.Key);
            }
        }
    }

    private void InitKeyDictionary()
    {
        dictionaryKeyGameMode = new Dictionary<GameModeType, string>
        {
            { GameModeType.None,  noneModeKey},
            { GameModeType.PlayerControl,  playerControlKey},
            { GameModeType.PlayerControlAI,  playerControlAIKey},
            { GameModeType.ObjectPlacement,  objectPlacementKey}
        };
    }

    private void ChangeGameMode(GameModeType type)
    {
        gameMode.SetValue(type);
    }
}
