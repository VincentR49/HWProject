using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Store the component scripts related to the given gameObject
public class GameModeBehaviours : ScriptableObject {
	
	[Serializable]
	[Tooltip("Component name specific to ObjectPlacement mode")]
	private String[] objectPlacement;
	
	[Serializable]
	[Tooltip("Component name specific to PlayerControl mode")]
	private Array[] playerControl;
	
	private Type[] ObjectPlacementScripts => ConvertStringArrayToType (objectPlacement);
	private Type[] PlayerControlScripts => ConvertStringArrayToType (playerControl);
	
	private static Type ConvertStringToType (String str)
	{
		return System.Reflection.Assembly.GetExecutingAssembly().GetType (str);
	}
	
	private static Type[] ConvertStringArrayToType (String[] strings)
	{
		if (strings == null) return null;
		List<Type> types = new List<Type>();
		foreach (String str in strings)
		{
			types.Add (ConvertStringToType(str));
		}
		return types.ToArray();
	}
	
	public Type[] GetTypes (GameMode.Type gameModeType)
	{
		switch (gameModeType)
		{
			case PlayerControl:   return PlayerControlScripts;
			case ObjectPlacement: return ObjectPlacementScripts;
			default:              return null
		}
	}
}