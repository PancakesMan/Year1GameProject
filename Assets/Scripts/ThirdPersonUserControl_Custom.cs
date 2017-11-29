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

        private bool m_Jump;                      // Is player jumping? 
		private bool m_crouching = false;         // Is player crouching?
        private float crouch_cd = 0.25f;          // Cooldown for crouching toggle
        private float footstep_cd;                // Cooldown for playing footstep sounds

        public float FOOTSTEP_CD = 0.25f;         // Constant to reset footstep_cd to
        public bool userHasControl = true;        // Used to disable player controls
        public GameObject text;                   // Text object to display
        public List<AudioClip> footsteps;         // List of footstep sounds
        [HideInInspector]
        public NavMeshAgent followingLad;         // Lad that's following the player
        [HideInInspector]
        public EggScript egg;                     // The egg the player is currently standing near
        [HideInInspector]
        public bool collidingWithEgg;             // Are we colliding with an egg?


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

            // Get the AudioSource component
            audio = GetComponent<AudioSource>();

            // Set footstep_cd to the cooldown constant
            footstep_cd = FOOTSTEP_CD;
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
            // Show the text object
            text.SetActive(true);
        }

        public void HideText()
        {
            // Hide the text object
            text.SetActive(false);
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            crouch_cd -= Time.deltaTime;   // Remove time from crouch_cd
            footstep_cd -= Time.deltaTime; // Remove time from footstep_cd

            // if player pressed E and it's colliding with an egg
			if (Input.GetKey(KeyCode.E) && collidingWithEgg)
            {
                // if the egg you're colliding with exists
                if (egg != null)
                {
                    // And a Lad hasn't hatched from the egg already
                    if (egg.LadHatched == false)
                    {
                        // Make the egg hatch the Lad
                        egg.LadHatched = true;
                        egg.HatchLad(3.0f);

                        // lad following the player is the Lad from the egg
                        followingLad = egg.Lad;
                    }
                }
                // Play egg stabbing animation
				m_Animator.SetBool ("collidingWithEgg", true);

                // Make egg stabbing animation stop playing
				Invoke("StopStabbingEgg", 3);

                // The arrow is now coated
                ShootingScript.coated = true;

                // Show the Arrow Coated text
                ShowText();
			}
			    


            // Read User Inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            // If you press LeftControl and you are able to crouch/uncrouch
			if (Input.GetKey (KeyCode.LeftControl) && crouch_cd < 0.0f)
            {
                // Toggle the crouch property
				m_crouching = !m_crouching;

                // Reset the crouch cooldown
                crouch_cd = 0.25f;
			}

            // If you're pressing LeftShift, move more slowly
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

            // If the player is moving and footstep_cd has passed
            if (m_Move != Vector3.zero && footstep_cd <= 0.0f)
            {
                // Reset the footstep cooldown
                footstep_cd = FOOTSTEP_CD;

                // Get a random sound to play, except the first one
                int n = UnityEngine.Random.Range(1, footsteps.Count);

                // Set the sound to play to be the selected one
                audio.clip = footsteps[n];

                // Play the selected audio
                audio.PlayOneShot(audio.clip);

                // Move the audio at index 0 up
                // and move the played audio to index 0
                // so it's not played next time
                footsteps[n] = footsteps[0];
                footsteps[0] = audio.clip;
            }
            
            // Move the character
		    m_Character.Move(m_Move * (userHasControl ? 1 : 0), m_crouching, m_Jump && userHasControl);
            m_Jump = false;
        }
    }
}
