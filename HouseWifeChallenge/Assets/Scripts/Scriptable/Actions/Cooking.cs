using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ("Scriptable Objects/Actions/Cooking")]
public class Cooking : Action
{
	// Precise specific attributes later
    public override void Execute(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " started cooking with " + interactibleObject.name);
	}
	
	public override void Cancel(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has canceled cooking with " + interactibleObject.name);
	}
	
	public override void Finish(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has finished cooking with " + interactibleObject.name);
	}
	
}