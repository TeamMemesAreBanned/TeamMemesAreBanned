using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchCamera : MonoBehaviour {

	private Camera camera;
	private int cleared = 0;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.Color;
		cleared = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cleared < 2) {
			camera.clearFlags = CameraClearFlags.Color;
			cleared++;
			return;
		}

		camera.clearFlags = CameraClearFlags.Nothing;
	}
}
