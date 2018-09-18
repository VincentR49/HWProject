using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Actions/Cooking")]
public class Cook : Action
{
	// Precise specific attributes later
    public override bool Execute(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " started cooking with " + interactibleObject.name);
        return true;
	}
	
	public override bool Cancel(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has canceled cooking with " + interactibleObject.name);
        return true;
    }
	
	public override bool Finish(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has finished cooking with " + interactibleObject.name);
        return true;
    }

}