using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

    public int pixelRes = 64;

    // Pixel perfect size
    public void UpdateCameraSize()
    {
        GetComponent<Camera>().orthographicSize = Screen.height / pixelRes / 2;
    }

}
