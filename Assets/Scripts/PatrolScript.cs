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
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, waypoints[waypointTarget].transform.position) < 1.0f)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= waitTimeBetweenWaypoints)
            {
                timeWaited = 0.0f;
                waypointTarget += 1;
                waypointTarget %= waypoints.Count;
            }
        }
        NavigateTo();
    }

    void NavigateTo()
    {
        Vector3 targetDirection = player.GetComponent<CapsuleCollider>().transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        if (angleToPlayer < viewingAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection.normalized, out hit, viewingDistance))
                playerInSight = (hit.collider.gameObject.tag == "Player");
        }

        if (playerInSight)
            agent.SetDestination(player.transform.position);
        else
            agent.SetDestination(waypoints[waypointTarget].transform.position);
    }
}
