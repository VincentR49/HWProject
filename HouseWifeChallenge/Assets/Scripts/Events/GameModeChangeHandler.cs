using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle the change of gameMode and enable / disable the gameModeRelated Monobehaviours
// Faire le dictionnaire dans une classe séparée
public class GameModeChangeHandler : MonoBehaviour {

	[Tooltip("Reference to the current game mode")]
	public GameMode gameMode;

    [Tooltip("Reference to the game mode script library")]
    public GameModeBehavioursLibrary gameModeBehavioursLibrary;

    // to simplify ...
    public void EnableDisblaeGameModeRelatedComponent()
	{
        Dictionary<GameMode.Type, Type[]> dict = gameModeBehavioursLibrary.GetDictionary();
        foreach (KeyValuePair<GameMode.Type, Type[]> entry in dict)
        {
            bool enable = gameMode.GetValue() == entry.Key;
            Type[] types = entry.Value;
            if (types == null) continue;
            foreach (Type type in types)
            {
                if (type != null && type.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    MonoBehaviour component = GetComponent(type) as MonoBehaviour;
                    if (component != null)
                    {
                        component.enabled = enable;
                    }
                }
            }
        }
	}
}
