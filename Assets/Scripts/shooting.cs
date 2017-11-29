using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class shooting : MonoBehaviour {

	public Camera mainCam;                          // Camera for First Person
    public GameObject ThirdPersonObject;            // ThirdPersonObject reference
    public AudioClip ShootingSound;                 // Sound to play when crossbow is shooting

    [HideInInspector]
    public bool coated;                             // Is the arrow coated?

    private Animator xbowAnimator;                  // Animator for the crossbow
    private ThirdPersonUserControl_Custom player;   // Control script on the player object
    private _31Toggle ChangeModeController;         // Change Mode script on the player
    private float shooting_cd;                      // Cooldown for shooting

    void Start()
    {
        xbowAnimator = GetComponent<Animator>();
        player = ThirdPersonObject.GetComponent<ThirdPersonUserControl_Custom>();
        ChangeModeController = GameObject.Find("PlagueDoctorPrefab").GetComponent<_31Toggle>();
        shooting_cd = 0.0f;
    }

    void Update ()
	{
        shooting_cd -= Time.deltaTime;

        // If the player left clicked
        if (Input.GetMouseButtonDown(0) && shooting_cd <= 0.0f) {
            // Reset shooting cooldown
            shooting_cd = 3.6f;

            // Set the xbowFired bool to true so the firing animation plays
            xbowAnimator.SetBool("xbowFired", true);
            GetComponent<AudioSource>().PlayOneShot(ShootingSound);

            // Add a cooldown to the Change Mode script so you
            // can't change mode until animation is finished
            ChangeModeController.cd = -3.6f;

            // Reset the crossbow
            Invoke("ResetXbow", 0.5f);
        }
	}

    void ResetXbow()
    {
        // Set xbowFired to false so the animation can play again
        xbowAnimator.SetBool("xbowFired", false);

        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition); // Raycast to where the arrow is pointing
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.tag + " was hit");
            if (hit.collider.tag == "Boulder")
            {
                Debug.Log("You shot the boulder!");
                hit.rigidbody.useGravity = true;                 // Enable gravity on Boulders so they fall
            }
            else if (hit.collider.tag.Contains("WoodBlockade") && coated)
            {
                // Make the Lad following the player move to the blockade you shot
                player.followingLad.SetDestination(hit.collider.transform.position);

                // Make the Lad play it's walking animation
                player.followingLad.transform.GetComponent<Animator>().Play("Walking");

                // Make the Lad no kinematic so it can collide with the blockade
                player.followingLad.transform.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        // Set coated to false
        // coating must be re-applied after every shot
        coated = false;

        // Hide the text on screen that says the arrow is coated
        player.HideText();
    }
}


