using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SwimmingControl : PlayerControl {

    private bool jumpPressed = false;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    private Transform m_GroundCheck;
    private Transform m_CeilingCheck;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;

    [SerializeField] private LayerMask m_WhatIsGround;

    const float k_GroundedRadius = .15f;
    const float k_CeilingRadius = .01f;

    public float jumpVelocity = 10f;
    public float gravity = 1f;
    public float maxSpeed = 3f;
    public float sinkScale = 2f;
    public float upScale = 0.8f;
    public float maxGroundSpeed = 1.5f;

    private void Awake() {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    private void Start() {
        m_Rigidbody2D.gravityScale = gravity;
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
        float verticalSpeed = Mathf.Sign(m_Rigidbody2D.velocity.y) * Mathf.Min(maxSpeed, Mathf.Abs(m_Rigidbody2D.velocity.y));
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, verticalSpeed);
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        // Read the inputs.
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool shouldSink = v < 0;
        bool upPressed = v > 0;
        Move(h, jumpPressed, shouldSink, upPressed);
        jumpPressed = false;
    }

    private void Move(float horizontalMove, bool jump, bool sink, bool upPressed) {
        if (sink) {
            m_Rigidbody2D.gravityScale = sinkScale * gravity;
        } else if (upPressed) {
            m_Rigidbody2D.gravityScale = upScale * gravity;
        } else {
            m_Rigidbody2D.gravityScale = gravity;
        }

        if (jump) {
            Vector2 velocity = m_Rigidbody2D.velocity;
            velocity.y = jumpVelocity;
            m_Rigidbody2D.velocity = velocity;
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        m_Anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Move the character
        float horizontal = horizontalMove * (isGrounded ? maxGroundSpeed : maxSpeed);
        m_Rigidbody2D.velocity = new Vector2(horizontal, m_Rigidbody2D.velocity.y);

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

		if (!jumpPressed) {
            // Read the jump input in Update so button presses aren't missed.
            jumpPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

}
