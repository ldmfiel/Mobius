using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectOrthoCamera : MonoBehaviour {
    new Camera camera;
    public float ScreenWidth;
    public float ScreenHeight;
    public float PixelsPerUnit;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        camera.orthographicSize = ScreenWidth / (((ScreenWidth / ScreenHeight) * 2) * PixelsPerUnit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
