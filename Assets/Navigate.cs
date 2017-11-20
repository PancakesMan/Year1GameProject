using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour {

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    Animator m_Animator;
    bool isWalking;
    public bool updatePosition;
    public bool updateRotation;
    public float yRotation;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
        
        //  if (isWalking == false)
        //       m_Animator.SetBool("isWalking", false);
    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
        m_Animator.SetBool("isWalking", true);
        if (isWalking == true) {
            Debug.Log("Walking");
        }
        yRotation += Input.GetAxis("Horizontal");
        transform.eulerAngles = new Vector3(10, yRotation, 0);
    }
}
