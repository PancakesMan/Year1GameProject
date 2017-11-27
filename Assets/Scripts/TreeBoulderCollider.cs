using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoulderCollider : MonoBehaviour {

    public GameObject tree;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boulder")
            tree.GetComponent<Animator>().SetBool("treeFalling", true);
    }
}
