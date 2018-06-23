using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SnakeControl : PlayerControl {

    private Rigidbody2D rb2d;

    public float speed = 5f;

	void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(0f, -speed);
	}

	void FixedUpdate() {

        if (isDead) {
            rb2d.velocity = new Vector2(0f, 0f);
            return;
        }

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        Move(h, v);
		
	}

	public void Move(float horizontal, float vertical) {

        if (horizontal > 0) {
            rb2d.velocity = new Vector2(speed, 0f);
        } else if (horizontal < 0) {
            rb2d.velocity = new Vector2(-speed, 0f);
        } else if (vertical > 0) {
            rb2d.velocity = new Vector2(0f, speed);
        } else if (vertical < 0) {
            rb2d.velocity = new Vector2(0f, -speed);
        }
	    
	}
}
