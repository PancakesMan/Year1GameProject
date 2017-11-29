using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour{

    // DeathMenu object to be enabled upon death
    public GameObject deathMenu;
   
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player")
        {
            Debug.Log("Player has entered trigger");
            deathMenu.SetActive(true);

            // Enable moving the cursor frely
            // and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
