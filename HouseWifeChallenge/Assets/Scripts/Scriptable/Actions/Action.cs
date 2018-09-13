﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public new string name;
    public string description;
	
	[Tooltip("Icon appearing while performing the action")]
    public Sprite icon;
	
	[Tooltip("Action duration in second")]
    [Range(0,5)]
    public float duration;
	
    public abstract bool Execute(GameObject player, GameObject interactibleObject);
	public abstract bool Cancel(GameObject player, GameObject interactibleObject);
	public abstract bool Finish(GameObject player, GameObject interactibleObject);
}

