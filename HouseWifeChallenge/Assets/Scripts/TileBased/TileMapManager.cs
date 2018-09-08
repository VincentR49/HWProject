using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
// Used to link a tileMapVariable to a Tilemap object
public class TileMapManager : MonoBehaviour {

    public TileMapVariable tileMap;
	
	void Awake () {
        tileMap.Value = GetComponent<Tilemap>();
	}
	
}
