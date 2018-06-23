using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    public class InvisibleWhenMovingControl : PlayerControl
    {
        private bool m_Jump;
        private bool isJumpPressed;

        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 800f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = true;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .15f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;
		private SpriteRenderer spriteRenderer;

        public float fadeOutTime = 0.1f;
        public float fadeInTime = 0.1f;

        private float keyPressedTime;
        private float keyReleasedTime;
        private bool isMoving = false;
        private bool isFadingIn = false;
        private bool isFadingOut = false;

        private Color transparentColor = new Color(1f, 1f, 1f, 0f);

        public AudioClip jump1;
        public AudioClip jump2;
        public AudioClip jump3;
        public AudioClip jump4;
        public AudioClip land1;
        public AudioClip land2;
        public AudioClip land3;
        public AudioClip land4;


        private void FixedUpdate() {
			if (isDead) {
                spriteRenderer.enabled = true;
				return;
			}

            bool wasGrounded = m_Grounded;
			m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            if (!wasGrounded && m_Grounded) {
                SoundManager.instance.RandomizeSfx(land1, land2, land3, land4);
            }

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            Move(h, m_Jump);
            m_Jump = false;
        }


        public void Move(float move, bool jump) {

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl) {

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

				if (move > 0) {
					Vector3 theScale = transform.localScale;
					theScale.x = 1;
					transform.localScale = theScale;
				} else if (move < 0) {
					Vector3 theScale = transform.localScale;
					theScale.x = -1;
					transform.localScale = theScale;
				}
			}

            if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0.01f) {
                isFadingIn = false;
                if (isMoving) {
                    keyPressedTime += Time.deltaTime;
                } else {
                    isFadingOut = true;
                    isMoving = true;
                    keyPressedTime = 0f;
                }
            } else {
                isFadingOut = false;
                if (isMoving) {
                    isFadingIn = true;
                    keyReleasedTime = 0f;
                    isMoving = false;
                } else {
                    keyReleasedTime += Time.deltaTime;
                }
            }

			//spriteRenderer.enabled = Mathf.Abs(m_Rigidbody2D.velocity.x) <= 0.01;

            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground")) {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                SoundManager.instance.RandomizeSfx(jump1, jump2, jump3, jump4);
            }

            if (!m_Grounded) {
                if (isJumpPressed) {
                    m_Rigidbody2D.gravityScale = 3;
                } else {
                    m_Rigidbody2D.gravityScale = 6;
                }
            } else {
                m_Rigidbody2D.gravityScale = 3;
            }
        }

        private void Awake()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }


		protected override void Update() {
			base.Update();

			isJumpPressed = CrossPlatformInputManager.GetButton("Jump");
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (isFadingIn) {
                float t = keyReleasedTime / fadeInTime;
                if (t >= 1f) {
                    spriteRenderer.color = Color.white;
                    isFadingIn = false;
                } else {
                    spriteRenderer.color = Color.Lerp(transparentColor, Color.white, t);
                }
            } else if (isFadingOut) {
                float t = keyPressedTime / fadeOutTime;
                if (t >= 1f) {
                    spriteRenderer.color = transparentColor;
                    isFadingOut = false;
                } else {
                    spriteRenderer.color = Color.Lerp(Color.white, transparentColor, keyPressedTime / fadeOutTime);
                }
            }
        }
    }
}
