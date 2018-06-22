using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool isJumpPressed;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            isJumpPressed = CrossPlatformInputManager.GetButton("Jump");
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            m_Character.Move(h, m_Jump, isJumpPressed);
            m_Jump = false;
        }
    }
}
