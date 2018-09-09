using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Scriptable Objects/Game Mode")]
public class GameMode : ScriptableObject {

    public enum Type
    {
        None,
        PlayerControl,
        ObjectPlacement
    }

    [SerializeField]
    [Tooltip("Current game mode")]
    private Type gameModeValue;
    public Type Value
    {
        get { return gameModeValue; }
        set
        {
            gameModeValue = value;
            if (Application.isPlaying)
            {
                Event.Raise();
            }
        }
    }

    [Tooltip("Event to raise when value changed")]
    public GameEvent Event;

    // When value is modified in the inspector
    public void OnValidate()
    {
        Event.Raise();
    }
    
}
