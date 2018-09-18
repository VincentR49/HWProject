using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Actions/Eat")]
public class Eat : Action {

    public override bool Cancel(GameObject player, GameObject interactibleObject)
    {
        Debug.Log(player + " stopped eating food at " + interactibleObject);
        return true;
    }

    public override bool Execute(GameObject player, GameObject interactibleObject)
    {
        Debug.Log(player + " starts to eat food at " + interactibleObject);
        return true;
    }

    public override bool Finish(GameObject player, GameObject interactibleObject)
    {
        Debug.Log(player + " finished to eat food at " + interactibleObject);
        return true;
    }
}
