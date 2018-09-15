using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store the component scripts related to the given gameObject
public class GameModeBehavioursLibrary : ScriptableObject {

    [SerializeField]
    [Tooltip("Component name specific to ObjectPlacement mode")]
	private string[] objectPlacement;

    [SerializeField]
    [Tooltip("Component name specific to PlayerControl manual mode")]
	private string[] playerControl;

    [SerializeField]
    [Tooltip("Component name specific to PlayerControl AI mode")]
    private string[] playerControlAI;

    private Type[] ObjectPlacementScripts => ConvertStringArrayToType (objectPlacement);
	private Type[] PlayerControlScripts => ConvertStringArrayToType (playerControl);
    private Type[] PlayerControlAIScripts => ConvertStringArrayToType(playerControlAI);

    public Dictionary<GameModeType, Type[]> GetDictionary()
    {
        return new Dictionary<GameModeType, Type[]>
        {
            { GameModeType.PlayerControl, ConvertStringArrayToType (playerControl) },
            { GameModeType.ObjectPlacement,  ConvertStringArrayToType(objectPlacement)  },
            { GameModeType.PlayerControlAI,  ConvertStringArrayToType(playerControlAI)  }
        }; ;
    }

    private static Type ConvertStringToType(String str)
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetType(str);
    }

    private static Type[] ConvertStringArrayToType(String[] strings)
    {
        if (strings == null) return null;
        List<Type> types = new List<Type>();
        foreach (String str in strings)
        {
            Type type = ConvertStringToType(str);
            if (type == null)
            {
                Debug.Log("Error: couldnt convert the string to type: " + str);
            }
            else
            {
                types.Add(type);
            }
        }
        return types.ToArray();
    }
}