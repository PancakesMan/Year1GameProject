using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbowPickup : MonoBehaviour {

	public GameObject crossbow;
    public GameObject backCrossbow;
	public GameObject thirdTalking; 
	public GameObject fog1;

    Animator animator;

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
