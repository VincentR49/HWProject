using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store the component scripts related to the given gameObject
[CreateAssetMenu (menuName = "Scriptable Objects/GameModeBehavioursLibrary")]
public class GameModeBehavioursLibrary : ScriptableObject {

    [SerializeField]
    [Tooltip("Component name specific to ObjectPlacement mode")]
	private string[] objectPlacement;

    [SerializeField]
    [Tooltip("Component name specific to PlayerControl mode")]
	private string[] playerControl;

    private Dictionary<GameMode.Type, Type[]> dictionary;
 
    private void OnEnable()
    {
        dictionary = new Dictionary<GameMode.Type, Type[]>
        {
            { GameMode.Type.PlayerControl, ConvertStringArrayToType (playerControl) },
            { GameMode.Type.ObjectPlacement,  ConvertStringArrayToType(objectPlacement)  }
        };
    }

    public Dictionary<GameMode.Type, Type[]> GetDictionary()
    {
        return dictionary;
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
            types.Add(ConvertStringToType(str));
        }
        return types.ToArray();
    }
}