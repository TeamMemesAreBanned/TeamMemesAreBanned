using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.instance.LoadNextLevel();
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
