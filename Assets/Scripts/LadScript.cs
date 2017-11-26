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
        position = transform.parent.Find("EggAnim").transform.position;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (nearBlockade)
        {
            animator.Play("Lad Eating");
            StartCoroutine(DisableBlockade());
            StartCoroutine(ReturnToEgg());
        }
	}

    IEnumerator DisableBlockade()
    {
        yield return new WaitForSecondsRealtime(2);
        nearestBlockade.SetActive(false);
        nearestBlockade = null;
        nearBlockade = false;
    }

    IEnumerator ReturnToEgg()
    {
        yield return new WaitForSecondsRealtime(2);
        agent.SetDestination(position);
        agent.updateRotation = true;
        animator.Play("Walking");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "WoodBlockade")
        {
            nearBlockade = true;
            nearestBlockade = other.gameObject;
        }
        else if (other.gameObject.tag == "Egg")
            gameObject.SetActive(false);
    }
}
