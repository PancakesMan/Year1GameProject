using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbowPickup : MonoBehaviour {

	public GameObject crossbow;        // crossbow object in the scene to disable
    public GameObject backCrossbow;    // crossbow object on player model to enable
	public GameObject thirdTalking;    // cutscene to activate
	public GameObject fog1;            // fog to be disabled, allowing player to continue

    Animator animator;                 // animator attached to the player

    void Start()
    {
        animator = GetComponent<Animator>();
    }

	void OnTriggerStay (Collider other) {
		if (other.tag == "Crossbow") {
			if (Input.GetKeyDown (KeyCode.E))
			{
                animator.Play("PickingUpCrossbow");
                Invoke("DisableCrossbowObject", 4);
				fog1.SetActive (false); 
				Debug.Log ("Crossbow picked up!"); 
			}
		}
	}

    void DisableCrossbowObject()
    {
        crossbow.SetActive(false);
        thirdTalking.SetActive(true);
        backCrossbow.SetActive(true);
    }
}
