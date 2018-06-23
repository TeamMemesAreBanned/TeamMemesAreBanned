using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwapper : MonoBehaviour {

    private Transform player;
    public Sprite newSprite;
    public float swapPosition = -4f;

	void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update() {
        if (player.position.y < swapPosition) {
            GetComponent<SpriteRenderer>().sprite = newSprite;
        }
	}
}
