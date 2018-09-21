using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Manager to change the game speed
public class GameSpeedManager : MonoBehaviour {

	[Tooltip("Reference to the floatVariable corresponding to normal speed")]
	public FloatVariable normalSpeed;
	
	[Tooltip("Reference to the floatVariable corresponding to fast speed")]
	public FloatVariable fastSpeed;
	
	[Tooltip("Reference to the floatVariable corresponding to very fast speed")]
	public FloatVariable veryFastSpeed;

    public GameEvent gameSpeedChanged;


    // Controls to change the speed
    public String pauseKey = "space";
    public String normalSpeedKey = "1";
	public String fastSpeedKey = "2";
	public String veryFastSpeedKey = "3";

    void Start () {
        ChangeGameSpeed(normalSpeed.Value);
    }

    private void Update()
    {
		if (Input.GetKeyDown (pauseKey))
		{
			ChangeGameSpeed (0f);
		}
		
		if (Input.GetKeyDown (normalSpeedKey))
		{
			ChangeGameSpeed (normalSpeed.Value);
		}
		
		if (Input.GetKeyDown (fastSpeedKey))
		{
			ChangeGameSpeed (fastSpeed.Value);
		}
		
		if (Input.GetKeyDown (veryFastSpeedKey))
		{
			ChangeGameSpeed (veryFastSpeed.Value);
		}
    }

	public void ChangeGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;
        Debug.Log("Changed game speed to " + newSpeed);
        gameSpeedChanged.Raise();
    }
}
