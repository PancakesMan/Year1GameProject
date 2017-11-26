using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LadScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;

	// Use this for initialization
	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (animator.GetBool("eggHatched"))
        {
            if (agent.velocity == Vector3.zero)
                animator.Play("Lad Idle");
            else
                animator.Play("Walking");
        }

	}
}
