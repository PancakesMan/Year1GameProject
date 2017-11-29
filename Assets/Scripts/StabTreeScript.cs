using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabTreeScript : MonoBehaviour {

    private Animator player; // Animator for the player
    private bool inTrigger = false;

    public GameObject Text;
    public GameObject Barrier;
    public float delay = 1.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("ThirdPersonController").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (inTrigger && Input.GetKey(KeyCode.E))
        {
            player.Play("Carve");
            Invoke("ShowText", delay);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void ShowText()
    {
        Text.SetActive(true);
        Barrier.SetActive(false);
        player.Play("DocGroundedCopy");
    }
}
