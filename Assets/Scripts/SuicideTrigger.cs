using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;                           // Animator for the player
    private ThirdPersonUserControl_Custom controller;    // Controller for the player
    private _31Toggle fpsToggle;                         // Change Mode toggle script on the player
    private bool playerTriggered = false;                // Has trigger been entered by the player?

    public int scene;                                 // Next Scene to load after suiciding
    public float delay = 5.0f;                           // Delay to move to next scene after starting suicide animation

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("ThirdPersonController");

        animator = player.GetComponent<Animator>();
        controller = player.GetComponent<ThirdPersonUserControl_Custom>();
        fpsToggle = GameObject.Find("PlagueDoctorPrefab").GetComponent<_31Toggle>();
    }

    void Update()
    {
        if (playerTriggered && Input.GetKey(KeyCode.E))
        {
            // Remove player's control
            controller.userHasControl = false;
            fpsToggle.active = false;

            // Play the Commit animation
            animator.Play("Commit");

            // Load the final scene after delay time
            Invoke("LoadFinalScene", delay);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerTriggered = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerTriggered = false;
    }

    void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
