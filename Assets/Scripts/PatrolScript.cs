using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{
    public List<GameObject> waypoints;                 // List of waypoints to patrol between
    public float waitTimeBetweenWaypoints = 0.0f;      // How long the agent waits at each waypoint
    public float viewingAngle = 120.0f;                // Angle infront of the agent it can see
    public float viewingDistance = 50.0f;              // Distance infront of agent it can see

    private float timeWaited = 0.0f;                   // Time waited at current waypoint
    private int waypointTarget = 0;                    // target waypoint in list of waypoints
    private bool playerInSight = false;                // Can I see the player?

    private NavMeshAgent agent;                        // NavMeshAgent of enemy patrolling
    private Animator animator;                         // Animator of enemy patrolling
    private GameObject[] players = new GameObject[2];  // Array of player objects for player detection

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players[0] = GameObject.Find("ThirdPersonController");

        // Loop to find the disabled first person player
        Transform[] trans = GameObject.Find("MultipurposeCameraRig").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
            if (t.gameObject.name == "crossbow2")
                players[1] = t.gameObject;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the agent is at a waypoint
        if (Vector3.Distance(agent.transform.position, waypoints[waypointTarget].transform.position) < 1.0f)
        {
            // Play the Idle animation
            animator.Play("Idle");

            // Increase time waited at waypoint
            timeWaited += Time.deltaTime;

            // If it's waited at the waypoint long enough
            if (timeWaited >= waitTimeBetweenWaypoints)
            {
                // Play the walking animation
                animator.Play("Walking");

                // Reset time waited at a waypoint
                timeWaited = 0.0f;

                // Change the target waypoint to the next one in the list
                waypointTarget += 1;
                waypointTarget %= waypoints.Count;
            }
        }
        else
            NavigateTo(); // If it's not at a waypoint, Navigate to the next one
    }

    void NavigateTo()
    {
        // Get the active player object from the players array
        GameObject player = players[0].activeSelf ? players[0] : players[1];

        // Get direction to the currently active player
        Vector3 targetDirection = player.transform.position - transform.position;

        // Get the angle to the currently active player
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        // If the player is within the viewing angle of the enemy
        if (angleToPlayer < viewingAngle * 0.5f)
        {
            RaycastHit hit; // Raycast to the player's position with the distance the enemy can see
            if (Physics.Raycast(transform.position, targetDirection.normalized, out hit, viewingDistance))
                playerInSight = (hit.collider.gameObject.tag == "Player"); // If the raycast hits the player, the player is in sight
        }

        if (playerInSight)
        {
            // Set the destination to the player
            agent.SetDestination(player.transform.position);

            // Play the running animation
            animator.Play("Running");
        }
        else
        {
            // Set the destination to the next waypoint
            agent.SetDestination(waypoints[waypointTarget].transform.position);

            // Play the walking animation
            animator.Play("Walking");
        }
    }
}
