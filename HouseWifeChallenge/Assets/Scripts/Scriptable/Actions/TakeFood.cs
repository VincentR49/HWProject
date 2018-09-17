using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Actions/Take Food")]
public class TakeFood : Action
{
	// Precise specific attributes later
    public override bool Execute(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " is taking food from " + interactibleObject.name);
        return true;
	}
	
	public override bool Cancel(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has canceled taking food from " + interactibleObject.name);
        return true;
    }
	
	public override bool Finish(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has finished taking food from " + interactibleObject.name);
        return true;
    }
}