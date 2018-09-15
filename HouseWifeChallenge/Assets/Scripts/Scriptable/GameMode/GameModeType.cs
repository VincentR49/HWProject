using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModeType {
    None,
    PlayerControl,
    PlayerControlAI,
    ObjectPlacement
}

// Extension method
static class StuffMethods
{

    public static string GetString (this GameModeType type)
    {
        switch (type)
        {
            case GameModeType.PlayerControl: return "Player control";
            case GameModeType.PlayerControlAI: return "Player control AI";
            case GameModeType.ObjectPlacement: return "Object Placement";
            default: return "None";
        }
    }
}
