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

    [Tooltip("Current game mode")]
    [SerializeField]
    private Type value;
    
    [Tooltip("Event to raise when value changed")]
    public GameEvent Event;

    // When value is modified in the inspector
    public void OnValidate()
    {
        Event.Raise();
    }

    public void SetValue (Type value)
    {
        Debug.Log("Set new game mode");
        this.value = value;
        Event.Raise();
    }

    public Type GetValue()
    {
        return value;
    }
}
