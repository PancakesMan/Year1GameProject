using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeScript : MonoBehaviour {

    void Start()
    {
        Debug.Log("BlockadeScript active");
    }

	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " entered blockade trigger");
        if (other.tag == "Lad")
        {
            other.GetComponent<LadScript>().nearestBlockade = gameObject;
            other.GetComponent<LadScript>().nearBlockade = true;
        }
    }
}
