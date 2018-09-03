using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeTogle : MonoBehaviour {

    public Toggle playerControlToggle;
    public Toggle objectMovementToggle;
    private GameManager game;

    public void Start()
    {
        game = GameManager.instance;
        switch (game.GetGameMode())
        {
            case GameManager.GameMode.PlayerPhase:
                playerControlToggle.isOn = true;
                break;
            case GameManager.GameMode.ObjectPlacement:
            default:
                objectMovementToggle.isOn = true;
                break;
        }
        playerControlToggle.onValueChanged.AddListener(delegate { OnPlayerControl_ChangeValue(); });
        objectMovementToggle.onValueChanged.AddListener(delegate { OnObjectPlacement_ChangeValue(); });
    }


    public void OnPlayerControl_ChangeValue()
    {
        if (playerControlToggle.isOn)
        {
            game.SetCurrentGameMode(GameManager.GameMode.PlayerPhase);
        }
    }

    public void OnObjectPlacement_ChangeValue()
    {
        if (objectMovementToggle.isOn)
        {
            game.SetCurrentGameMode(GameManager.GameMode.ObjectPlacement);
        }
    }
}
