using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{

    public List<GameObject> waypoints;
    public float waitTimeBetweenWaypoints = 0.0f;
    private float timeWaited = 0.0f;
    private int waypointTarget = 0;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        agent.SetDestination(waypoints[waypointTarget].transform.position);
    }
}
