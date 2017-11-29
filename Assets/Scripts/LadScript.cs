using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LadScript : MonoBehaviour {
    private NavMeshAgent agent;           // NavMeshAgent on the Lad
    private Animator animator;            // Animator on the Lad
    private Vector3 position;             // Initial position of the Lad

    public bool nearBlockade = false;     // Is the Lad near a blockade?
    public bool specialBlockade = false;  // Is the Lad near a special blockade?
    public bool eatenBlockade = false;    // Has the lad eaten a blockade?
    private GameObject nearestBlockade;   // Blockade object to eat

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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (nearBlockade)
        {
            animator.Play("Lad Eating");        // Play the Lad Eating animation
            StartCoroutine(DisableBlockade());  // Start coroutine to disable blockade
            StartCoroutine(ReturnToEgg());      // Start coroutine to make Lad return to the egg
        }
	}

    // Coroutine to disable blockade after eating animation
    IEnumerator DisableBlockade()
    {
        yield return new WaitForSecondsRealtime(2);
        nearestBlockade.SetActive(false);
        nearestBlockade = null;
        eatenBlockade = true;
        nearBlockade = false;
    }

    // Coroutine to make Lad return to egg after eating animation
    IEnumerator ReturnToEgg()
    {
        yield return new WaitForSecondsRealtime(2);
        agent.SetDestination(position);
        animator.Play("Walking");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "WoodBlockade")
        {
            nearBlockade = true;                  // Lad is near a blockade
            nearestBlockade = other.gameObject;   // The blockade the Lad is near what we're colliding with
        }
        else if (other.gameObject.tag == "SpecialWoodBlockade")
        {
            GetComponent<Rigidbody>().isKinematic = true; // Make the Lad kinematic so it can't be pushed
            specialBlockade = true;                       // Lad is near a special blockade
            agent.isStopped = true;                       // Disable Lad's NavmeshAgent
            animator.Play("Lad Eating");                  // Play the Lad's eating animation
        }
        else if (other.gameObject.tag == "Egg" && eatenBlockade)
            gameObject.SetActive(false);                  // Lad "returns" to it's egg once it's eaten a blockade
    }
}
