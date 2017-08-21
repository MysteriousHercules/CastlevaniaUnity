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
        private Animator m_Anim;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if(m_Anim == null)
            {
                m_Anim = GetComponent<Animator>();
            }
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
           
            // Read the inputs.
            bool crouch = false;
            
            if (Input.GetAxis("Vertical") < 0)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
            
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            if (m_Anim != null)
            {
                m_Anim.SetBool("Crouch", crouch);
            }
            m_Jump = false;
        }
    }
}
