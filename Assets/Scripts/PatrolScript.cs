using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{

    public List<GameObject> waypoints;
    public float waitTimeBetweenWaypoints = 0.0f;
    public float viewingAngle = 120.0f;
    public float viewingDistance = 50.0f;

    private float timeWaited = 0.0f;
    private int waypointTarget = 0;
    private bool playerInSight = false;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject[] players = new GameObject[2];

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players[0] = GameObject.Find("ThirdPersonController");

        Transform[] trans = GameObject.Find("MultipurposeCameraRig").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
            if (t.gameObject.name == "crossbow2")
                players[1] = t.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, waypoints[waypointTarget].transform.position) < 1.0f)
        {
            animator.Play("Idle");
            timeWaited += Time.deltaTime;
            if (timeWaited >= waitTimeBetweenWaypoints)
            {
                animator.Play("Walking");
                timeWaited = 0.0f;
                waypointTarget += 1;
                waypointTarget %= waypoints.Count;
            }
        }
        else
            NavigateTo();
    }

    void NavigateTo()
    {
        GameObject player = players[0].activeSelf ? players[0] : players[1];
        Vector3 targetDirection = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        if (angleToPlayer < viewingAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection.normalized, out hit, viewingDistance))
                playerInSight = (hit.collider.gameObject.tag == "Player");
        }

        if (playerInSight)
        {
            agent.SetDestination(player.transform.position);
            animator.Play("Running");
        }
        else
        {
            agent.SetDestination(waypoints[waypointTarget].transform.position);
            animator.Play("Walking");
        }
    }
}
