using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Actions/Cut Food")]
public class CutFood : Action
{
	// Precise specific attributes later
    public override bool Execute(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " is cutting food with " + interactibleObject.name);
        return true;
	}
	
	public override bool Cancel(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " canceled cutting food with " + interactibleObject.name);
        return true;
    }
	
	public override bool Finish(GameObject player, GameObject interactibleObject)
	{
		Debug.Log (player.name + " has finished cutting food with " + interactibleObject.name);
        return true;
    }
}