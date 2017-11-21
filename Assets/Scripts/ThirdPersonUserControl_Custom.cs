﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl_Custom : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Animator m_Animator;              // A reference to the Animator for the ThirdPersonCharacter
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;                   // the world-relative desired move direction, calculated from the camForward and user input.
        private bool m_Jump;                      
		private bool m_crouching = false;
        public bool userHasControl = true;

        float crouch_cd = 0.25f;

        [HideInInspector]
        public GameObject followingLad;
        [HideInInspector]
        public bool collidingWithEgg;


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                //m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            m_Animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

		void StopStabbingEgg()
		{
			m_Animator.SetBool ("collidingWithEgg", false);
		}

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            crouch_cd -= Time.deltaTime;

			if (Input.GetKey (KeyCode.E) && collidingWithEgg) {
				m_Animator.SetBool ("collidingWithEgg", true);
				Invoke ("StopStabbingEgg", 3);
			}
			    


            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			if (Input.GetKey (KeyCode.LeftControl) && crouch_cd < 0.0f) {
				m_crouching = !m_crouching;
                crouch_cd = 0.25f;
			}

            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

		    m_Character.Move(m_Move * (userHasControl ? 1 : 0), m_crouching, m_Jump && userHasControl);
            m_Jump = false;
        }
    }
}
