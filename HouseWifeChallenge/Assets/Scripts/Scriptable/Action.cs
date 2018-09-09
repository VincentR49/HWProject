using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{

    public new string name;
    public string description;
    public Sprite icon;
    public int durationMs;

    public abstract bool Execute(GameObject player, GameObject interactibleObject);

}

