using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GetGameSpeedText : MonoBehaviour {

    [Tooltip("Reference to the floatVariable corresponding to normal speed")]
	public FloatVariable normalSpeed;
	
	[Tooltip("Reference to the floatVariable corresponding to fast speed")]
	public FloatVariable fastSpeed;
	
	[Tooltip("Reference to the floatVariable corresponding to very fast speed")]
	public FloatVariable veryFastSpeed;

	private Text text;
	
    public void Awake()
    {
        text = GetComponent<Text>();
    }

	// A lancer lorsque la vitesse de jeu change
    public void UpdateText()
    {
        text.text = getTextFromGameSpeed();
    }
	
	private String getTextFromGameSpeed()
	{
		float gameSpeed = Time.timeScale;
		switch (gameSpeed)
		{
			case normalSpeed: return "Normal Speed";
			case 0f: return "Pause";
			case fastSpeed: return "Fast Speed";
			case veryFastSpeed: return "Very Fast Speed";
		}
	}
}
