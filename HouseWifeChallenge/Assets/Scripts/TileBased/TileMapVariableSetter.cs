using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
// Used to link a tileMapVariable to a Tilemap object
// TODO: change class name -> TileMapSetter
public class TileMapVariableSetter : MonoBehaviour {

	[Tooltip("tileMapVariable in which the TileMap component of this object will be referenced")]
    public TileMapVariable tileMap;
	
	void Awake () {
        tileMap.Value = GetComponent<Tilemap>();
	}
	
}
