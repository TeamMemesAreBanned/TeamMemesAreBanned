using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			PlayerControl playerControl = other.GetComponent<PlayerControl>();
			if (playerControl != null) {
				playerControl.Die();
			}
		}
	}
}
