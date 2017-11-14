using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shooting : MonoBehaviour {

	public Camera mainCam;
	public GameObject camera1;
	public GameObject camera2;
	public Rigidbody boulder; 
	public Animator tree;
	public GameObject crosshair; 
	public bool zoomed = false;
	public bool arrowCoated = false; 
	public GameObject woodBlockade; 
	public GameObject moonWhite;
	public GameObject moonGreen; 

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay (Input.mousePosition); 
			if (Physics.Raycast (ray, out hit)) {

			
				if (hit.collider.tag == "Boulder") {
					Debug.Log ("You shot the boulder!");
					boulder.useGravity = true;  
					hit.rigidbody.useGravity = true;
					Invoke ("TreeFalling", 3); 
				}
				if (hit.collider.tag == "WoodBlockade" && arrowCoated == true) {
					woodBlockade.SetActive (false); 
					hit.collider.gameObject.SetActive(false);
				}

				arrowCoated = false;
			}
					
		}

		if (Input.GetMouseButtonDown (1) && zoomed == false) {
			mainCam.transform.position = camera2.transform.position; 
			crosshair.SetActive (true);
			zoomed = true;
		} else if (Input.GetMouseButtonDown (1) && zoomed == true) {
			mainCam.transform.position = camera1.transform.position; 
			crosshair.SetActive (false);
			zoomed = false;
		}

		if (arrowCoated == true) {
			moonGreen.SetActive (true);
			moonWhite.SetActive (false);
		} else if (arrowCoated == false) {
			moonWhite.SetActive (true); 
			moonGreen.SetActive (false); 
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Egg") {
			if (Input.GetKeyDown (KeyCode.E))
			{
				Debug.Log ("Arrow Coated!"); 
				arrowCoated = true;
		}
	  }
	}
		
	void TreeFalling()
	{
		tree.SetBool ("treeFalling", true); 
	}

	}


