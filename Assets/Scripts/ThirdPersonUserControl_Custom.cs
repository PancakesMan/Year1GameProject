using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        private Vector3 m_Move;                   // The world-relative desired move direction, calculated from the camForward and user input.
        private shooting ShootingScript;          // A reference to the player's ShootingScript
        private AudioSource audio;

        private bool m_Jump;                      
		private bool m_crouching = false;
        private float crouch_cd = 0.25f;          // Cooldown for crouching toggle

        public bool userHasControl = true;        // Used to disable player controls
        public GameObject text;
        public List<AudioClip> footsteps;
        [HideInInspector]
        public NavMeshAgent followingLad;
        [HideInInspector]
        public EggScript egg;
        [HideInInspector]
        public bool collidingWithEgg;


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
                m_Cam = Camera.main.transform;
            else
            {
                // We use self-relative controls in this case
                // Which probably isn't what the user wants
                // But hey, we warned them!
                Debug.LogWarning(
                    "Warning: no main camera found." +
                    "Third person character needs a Camera tagged \"MainCamera\", " +
                    "for camera-relative controls.", gameObject);
            }
            // Get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

            // Get the player's Animator
            m_Animator = GetComponent<Animator>();

            // Get the player's Shooting Script
            // which is attached to a sibling's child
            ShootingScript = transform.parent.GetComponentInChildren<shooting>(true);
            audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!m_Jump)
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

		void StopStabbingEgg()
		{
            // Stop egg stabbing animation
			m_Animator.SetBool("collidingWithEgg", false);
		}

        void ShowText()
        {
            text.SetActive(true);
        }

        public void HideText()
        {
            text.SetActive(false);
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            crouch_cd -= Time.deltaTime;

			if (Input.GetKey(KeyCode.E) && collidingWithEgg)
            {
                if (egg != null)
                {
                    if (egg.LadHatched == false)
                    {
                        egg.LadHatched = true;
                        egg.HatchLad(3.0f);

                        followingLad = egg.Lad;
                    }
                }
                // Play egg stabbing animation
				m_Animator.SetBool ("collidingWithEgg", true);

                // Make egg stabbig animation stop playing
				Invoke("StopStabbingEgg", 3);

                // The arrow is now coated
                ShootingScript.coated = true;
                ShowText();
			}
			    


            // Read User Inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			if (Input.GetKey (KeyCode.LeftControl) && crouch_cd < 0.0f)
            {
				m_crouching = !m_crouching;
                crouch_cd = 0.25f;
			}

            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

            // Calculate move direction to pass to character
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

            if (m_Move != Vector3.zero)
            {
                int n = UnityEngine.Random.Range(1, footsteps.Count);
                audio.clip = footsteps[n];
                audio.PlayOneShot(audio.clip);
                footsteps[n] = footsteps[0];
                footsteps[0] = audio.clip;
            }
            
		    m_Character.Move(m_Move * (userHasControl ? 1 : 0), m_crouching, m_Jump && userHasControl);
            m_Jump = false;
        }
    }
}
