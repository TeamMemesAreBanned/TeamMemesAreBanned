using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {

	public float speed = 5f;
	public bool moveLeft = true;
	public bool dontFall = true;
	public bool onlyMoveWhenVisible = false;

	public Transform leftFallCheck;
	public Transform leftWallCheck;
	public Transform rightFallCheck;
	public Transform rightWallCheck;

	public LayerMask whatIsGround;

	private Rigidbody2D rigidbody2D;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate() {

		if (!spriteRenderer.isVisible && onlyMoveWhenVisible) {
			rigidbody2D.velocity = Vector2.zero;
			return;
		}

		if (dontFall) {
			Collider2D[] leftFallColliders = Physics2D.OverlapPointAll(leftFallCheck.position, whatIsGround);
			Collider2D[] rightFallColliders = Physics2D.OverlapPointAll(rightFallCheck.position, whatIsGround);
			if (leftFallColliders.Length == 0 && rightFallColliders.Length == 0) {
				// Do nothing
			} else if (moveLeft && leftFallColliders.Length == 0) {
				moveLeft = false;
			} else if (!moveLeft && rightFallColliders.Length == 0) {
				moveLeft = true;
			}
		}

		Collider2D[] leftWallColliders = Physics2D.OverlapPointAll(leftWallCheck.position, whatIsGround);
		Collider2D[] rightWallColliders = Physics2D.OverlapPointAll(rightWallCheck.position, whatIsGround);

		if (moveLeft && leftWallColliders.Length > 0) {
			moveLeft = false;
		} else if (!moveLeft && rightWallColliders.Length > 0) {
			moveLeft = true;
		}

		Vector2 velocity = rigidbody2D.velocity;
		velocity.x = moveLeft ? -speed : speed;
		rigidbody2D.velocity = velocity;

		spriteRenderer.flipX = !moveLeft;
	}
}
