using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CreateAssetMenu(menuName ="Scriptable Objects/Tile Map")]
public class TileMapVariable : ScriptableObject
{
    public Tilemap Value;
}
