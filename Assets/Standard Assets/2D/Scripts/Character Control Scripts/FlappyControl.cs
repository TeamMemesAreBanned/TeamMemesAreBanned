using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FlappyControl : PlayerControl {

    private bool m_Jump;
    private bool m_Grounded;

    private Transform m_GroundCheck;
    private Transform m_CeilingCheck;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;

    [SerializeField] private LayerMask m_WhatIsGround;

    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .01f;

    public float jumpVelocity = 10f;
    public float speed = 0f;

    private void Awake() {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    private void Start() {
        m_Rigidbody2D.gravityScale = 3f;
        m_Rigidbody2D.velocity = new Vector2(speed, 0f);
    }

    private void FixedUpdate() {
		if (isDead) {
			return;
		}

		m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        // Read the inputs.
        Move(m_Jump);
        m_Jump = false;
    }

    private void Move(bool jump) {
        if (jump) {
            Vector2 velocity = m_Rigidbody2D.velocity;
            velocity.y = jumpVelocity;
            m_Rigidbody2D.velocity = velocity;
        }
    }

	protected override void Update() {
		base.Update();

		if (!m_Jump) {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

}
