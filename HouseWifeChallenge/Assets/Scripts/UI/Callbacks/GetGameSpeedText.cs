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
        text.text = GetTextFromGameSpeed();
    }
	
	private string GetTextFromGameSpeed()
	{
		float gameSpeed = Time.timeScale;
        if (gameSpeed == normalSpeed.Value) return "Normal Speed";
        if (gameSpeed == 0f) return "Pause";
        if (gameSpeed == fastSpeed.Value) return "Fast Speed";
        if (gameSpeed == veryFastSpeed.Value) return "Very Fast Speed";
        return "Unknown speed";
	}
}
