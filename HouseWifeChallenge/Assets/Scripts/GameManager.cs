using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public enum GameMode
    {
        ObjectPlacement,
        PlayerPhase,
    }

    public Tilemap worldMap;
    public static GameManager instance = null;
    public GameMode currentMode = GameMode.ObjectPlacement; // make it private + dynamic change

    private Dictionary<GameMode, MonoBehaviour> modeScriptDict; // dictionnaire liant les games modes aux scripts lui étant spécifiques

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        if (worldMap == null)
        {
            worldMap = GetGroundTileMap();
        }
        worldMap.CompressBounds();
        
    }

    private void InitScriptDict()
    {
        modeScriptDict = new Dictionary<GameMode, MonoBehaviour>();
        // A continuer ...
    }


    private static Tilemap GetGroundTileMap()
    {
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();
        foreach (Tilemap tm in tilemaps)
        {
            if (tm.name == "Ground")
            {
                return tm;
            }
        }
        return null;
    }
}
