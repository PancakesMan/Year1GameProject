using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour{

    public GameObject deathMenu;
   
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player")
        Debug.Log("Player has entered trigger");
        {
            deathMenu.SetActive(true);
        }
    }

}
