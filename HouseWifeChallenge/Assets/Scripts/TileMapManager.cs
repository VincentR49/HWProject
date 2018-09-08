using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapManager : MonoBehaviour {

    public TileMapVariable tileMap;
	
	void Awake () {
        tileMap.Value = GetComponent<Tilemap>();
	}
	
}
