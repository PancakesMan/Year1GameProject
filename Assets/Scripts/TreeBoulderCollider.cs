using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoulderCollider : MonoBehaviour {

    // Tree object to make fall down
    public GameObject tree;

	void OnTriggerEnter(Collider other)
    {
        // If a boulder collided with this trigger
        if (other.tag == "Boulder")
            // Make the tree play it's falling animation
            tree.GetComponent<Animator>().SetBool("treeFalling", true);
    }
}
