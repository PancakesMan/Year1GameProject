using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EggScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = true;
		}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = false;
    }
}
