using System;
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
	private Dictionary<GameMode, System.Type[]> scriptsDict;
    private GameMode currentMode;
   
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
		InitScriptDict();
    }

    public void Start()
    { 
        DisableAllSpecificScripts();
        SetCurrentGameMode(GameMode.PlayerPhase);
    }

	// Set the dictionary containing the scripts specific to each gameMode
    private void InitScriptDict()
    {
        scriptsDict = new Dictionary<GameMode, Type[]>();
        scriptsDict.Add(GameMode.PlayerPhase, new Type[]
		{ 
			typeof(PlayerController)
		});
        scriptsDict.Add(GameMode.ObjectPlacement, new Type[] 
		{ 
			typeof(Movable),
			typeof(Rotable)
		});
    }
	
	// Change the current gameMode and enable / disable the related scripts
	public void SetCurrentGameMode(GameMode newGameMode)
	{
		GameMode oldGameMode = currentMode;
        foreach (Type type in scriptsDict[oldGameMode])
        {
            EnableScriptsOfType(false, type);
        }

        foreach (Type type in scriptsDict[newGameMode])
        {
            EnableScriptsOfType(true, type);
        }
        currentMode = newGameMode;
        Debug.Log("GameMode changed: " + newGameMode);
	}
	
    private void EnableScriptsOfType(bool enable, Type type)
    {
        MonoBehaviour[] scripts = GetAllInstancesOfMonoScript(type);
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = enable;
        }
    }

    private void DisableAllSpecificScripts()
    {
        foreach (KeyValuePair<GameMode, Type[]> entry in scriptsDict)
        {
            foreach (Type type in entry.Value)
            {
                EnableScriptsOfType(false, type);
            }
        }
    }


    // Get all the instances of the given MonoBehaviour type
    // Return an error if the type if not a MonoBehaviour
    private MonoBehaviour[] GetAllInstancesOfMonoScript(Type monoType)
	{
		if (monoType.IsSubclassOf(typeof(MonoBehaviour)))
		{
            MonoBehaviour[] instances = FindObjectsOfType(monoType) as MonoBehaviour[];
            return instances; 
		}
		else
		{
            Debug.Log("Error, not Monobehaviour script");
            return null;
        }
	}

    public GameMode GetGameMode()
    {
        return currentMode;
    }

}
