using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbowPickup : MonoBehaviour {

	public GameObject crossbow; 
	public GameObject thirdTalking; 
	public GameObject fog1;

	void OnTriggerStay (Collider other) {
		if (other.tag == "Crossbow") {
			if (Input.GetKeyDown (KeyCode.E))
			{
				crossbow.SetActive (false);
				thirdTalking.SetActive (true);
				fog1.SetActive (false); 
				Debug.Log ("Crossbow picked up!"); 
			}
		}
	}
}
