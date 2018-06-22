using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BouncingControl : PlayerControl {

    private bool isGrounded = false;
    private bool isFacingRight = true;

    private Transform m_GroundCheck;
    private Transform m_CeilingCheck;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;

    [SerializeField] private LayerMask m_WhatIsGround;

    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .01f;

    public float bounceVelocity = 10f;
    public float maxSpeed = 8f;
    public bool airControl = false;

    private void Awake() {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
		if (isDead) {
			return;
		}

		isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
        m_Anim.SetBool("Ground", isGrounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        // Read the inputs.
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        Move(h);
    }

    private void Move(float horizontalMove) {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        m_Anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        float horizontal = m_Rigidbody2D.velocity.x;
        if (isGrounded || airControl) {
            if (horizontalMove < 0) {
                horizontal = -maxSpeed;
            } else if (horizontalMove > 0) {
                horizontal = maxSpeed;
            } else {
                horizontal = 0f;
            }
        }

        float vertical = m_Rigidbody2D.velocity.y;
        if (isGrounded) {
            vertical = bounceVelocity;
        }

        m_Rigidbody2D.velocity = new Vector2(horizontal, vertical);

        // If the input is moving the player right and the player is facing left...
        if (horizontalMove > 0 && !isFacingRight) {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalMove < 0 && isFacingRight) {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	protected override void Update() {
		base.Update();
	}

}
