using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideTrigger : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void OnTriggerExit(Collider other)
    {
        if (other.tag == "Suicide")
        {
            animator.Play("Commit");
        }
    }
}
