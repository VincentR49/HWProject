using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CreateAssetMenu(menuName ="Scriptable Objects/Tile Map")]
public class TileMapVariable : ScriptableObject
{
    public Tilemap Value;
    public string defautWorldMapName = "Ground";

    public void OnEnable()
    {
        if (Value == null)
        {
            Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();
            foreach (Tilemap tm in tilemaps)
            {
                if (tm.name == defautWorldMapName)
                {
                    Value = tm;
                    break;
                }
            }
        }
    }

}
