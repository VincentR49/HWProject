using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle the change of gameMode and enable / disable the gameModeRelated Monobehaviours
// Faire le dictionnaire dans une classe séparée
public class GameModeChangeHandler : MonoBehaviour {

	[Tooltip("Reference to the current game mode")]
	public GameMode gameMode;
	
	// to simplify ...
	public void EnableGameModeSpecificComponent()
	{
		foreach (KeyValuePair<GameMode.Type, Type[] entry in GameModeSpecificMonoBehaviour.GetDictionary())
		{
			if (entry.Value == null) continue;
			
			bool enable = (entry.Key == gameMode.GetValue());
			foreach (Type type in entry.Value)
			{
				if (type != null && typeof(type).IsSubclassOf (typeof(MonoBehaviour)))
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
