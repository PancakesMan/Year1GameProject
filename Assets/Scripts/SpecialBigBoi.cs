using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialBigBoi : MonoBehaviour {

    private GameObject lad;

	// Use this for initialization
	void Start () {
        lad = GameObject.FindGameObjectWithTag("Lad");
	}
	
	// Update is called once per frame
	void Update () {
		if (lad.GetComponent<LadScript>().specialBlockade == true)
        {
            GetComponent<PatrolScript>().enabled = false;
            GetComponent<NavMeshAgent>().SetDestination(lad.transform.position);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lad")
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Animator>().Play("PickingUp");
            other.gameObject.SetActive(false);
            StartCoroutine(FinishChewingLad(10));
        }
    }

    IEnumerator FinishChewingLad(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GetComponent<PatrolScript>().enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SpecialBigBoi>().enabled = false;
    }
}
