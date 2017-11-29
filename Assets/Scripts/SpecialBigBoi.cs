using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialBigBoi : MonoBehaviour {

    private List<GameObject> lads; // List of all Lads in the scene

	// Use this for initialization
	void Start () {
        // Get all the Lads in the scene
        lads = new List<GameObject>(GameObject.FindGameObjectsWithTag("Lad"));
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject lad in lads)
		    if (lad.GetComponent<LadScript>().specialBlockade == true) // If the Lad is eating a special blockade
            {
                // Disable the patrol script component
                GetComponent<PatrolScript>().enabled = false;

                // navigate to the Lad eating the special wood blockade
                GetComponent<NavMeshAgent>().SetDestination(lad.transform.position);
                break;
            }
	}

    void OnTriggerEnter(Collider other)
    {
        // If Lad enters trigger
        if (other.gameObject.tag == "Lad")
        {
            // Stop the NavMeshAgent and make the enemy kinematic
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<Rigidbody>().isKinematic = true;

            // Play the picking up animation
            GetComponent<Animator>().Play("PickingUp");

            // Disable the Lad you just hit
            other.gameObject.SetActive(false);

            // Start coroutine to chew the Lad
            StartCoroutine(FinishChewingLad(10));
        }
    }

    IEnumerator FinishChewingLad(float time)
    {
        // Wait for a while
        yield return new WaitForSecondsRealtime(time);

        // Enable PatrolScript and NavMeshAgent
        GetComponent<PatrolScript>().enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;

        // Make RigidBody no longer kinematic
        GetComponent<Rigidbody>().isKinematic = false;

        // Disable the SpecialBigBoi script
        // Each BigBoi can only eat 1 Lad
        GetComponent<SpecialBigBoi>().enabled = false;
    }
}
