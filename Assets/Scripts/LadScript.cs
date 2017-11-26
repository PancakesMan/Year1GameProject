using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LadScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 position;

    public bool nearBlockade = false;
    [HideInInspector]
    public GameObject nearestBlockade;

	// Use this for initialization
	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (nearBlockade)
        {
            animator.Play("Lad Eating");
            nearestBlockade.SetActive(false);
            nearestBlockade = null;
            nearBlockade = false;
        }
	}
}
