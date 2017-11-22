using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EggScript : MonoBehaviour {

    public GameObject moonWhite;
    public GameObject moonGreen;


    public bool arrowCoated = false;

	// Update is called once per frame
	void Update () {
        if (arrowCoated == true)
        {
            moonGreen.SetActive(true);
            moonWhite.SetActive(false);
        }
        else if (arrowCoated == false)
        {
            moonWhite.SetActive(true);
            moonGreen.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Arrow Coated!");
                arrowCoated = true;
            }
        }
    }
        

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<ThirdPersonUserControl_Custom>().collidingWithEgg = false;
    }
}
