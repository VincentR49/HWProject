using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableAction : ScriptableObject {

    public new string name = "New action";
    public string description = "Action description";
    public Sprite image;
    public int durationMs = 1000;
    public int currentProgressMs = 0;

}
