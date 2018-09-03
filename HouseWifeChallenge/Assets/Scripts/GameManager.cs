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
	private Dictionary<GameMode, System.Type[]> scriptPerModeDic; 
    public GameMode CurrentMode
	{
		get  { return CurrentMode; }
		set { ChangeGameMode(value);}
	}	
    
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
    }

	private void Start()
	{
		Init();
	}
	
    private void Init()
    {
        if (worldMap == null)
        {
            worldMap = GetGroundTileMap();
        }
        worldMap.CompressBounds();
		InitScriptDict();
    }


	// Set the dictionary containing the scripts specific to each gameMode
    private void InitScriptDict()
    {
        modeScriptDict = new Dictionary<GameMode, Type>();
        modeScriptDict.Add(GameMode.PlayerPhase, new Type[]
		{ 
			typeof(PlayerController)
		});
		modeScriptDict.Add(GameMode.ObjectPlacement, new Type[] 
		{ 
			typeof(MovableObject),
			typeof(RotableObject)
		});
    }

	// Get the TileMap named Ground
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
	
	// Change the current gameMode and enable / disable the related scripts
	private void ChangeGameMode(GameMode newGameMode)
	{
		GameMode oldGameMode = CurrentMode;
		MonoBehaviour[] scriptsToDisable = GetAllInstancesOfMonoScript(scriptPerModeDic.Item[oldGameMode]);
		foreach (MonoBehaviour script in scriptsToDisable)
		{
			script.enable = false;
		}
		MonoBehaviour[] scriptsToEnable = GetAllInstancesOfMonoScript(scriptPerModeDic.Item[newGameMode]);
		foreach (MonoBehaviour script in scriptsToEnable)
		{
			script.enable = true;
		}
		CurrentMode = newGameMode;
	}
	
	// Get all the instances of the given MonoBehaviour type
	// Return an error if the type if not a MonoBehaviour
	private MonoBehaviour[] GetAllInstancesOfMonoScript(Type monoType)
	{
		if (monoType.IsSubclassOf(typeof(MonoBehaviour))
		{
			Debug.Log("Error, not Monobehaviour script");
		}
		else
		{
			MonoBehaviour[] instances = FindObjectsOfType(typeof(monoType)) as MonoBehaviour[];
			return instances;
		}
	}
}
