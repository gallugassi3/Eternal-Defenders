using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { Basic, Fast, None }

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private int healthPoints = 4;

    [Header("Movement")]

    [SerializeField] private float turnSpeed = 10;

    [SerializeField] private Transform[] waypoints;
    private int waypointIndex;
    private float totalDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints();
        CollectTotalDistance();
    }



    private void Update()
    {
        FaceTarget(agent.steeringTarget);
        // Check if the agent is close to current target point
        if (agent.remainingDistance < .5f)
        {
            // Set the destination to the next waypoints
            agent.SetDestination(GetNextWaypoint());
        }
    }

    public float DistanceToFinishLine()
    {
        return totalDistance + agent.remainingDistance;
    }

    private void CollectTotalDistance()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            float distance = Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
            totalDistance += distance;
        }
    }

    private void FaceTarget(Vector3 newTarget)
    {
        // Calculate the direction from current position to the new target
        Vector3 directionToTarget = newTarget - transform.position;
        directionToTarget.y = 0; // Ignore any difference in the vertical position // Removes vertical component

        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
        // Smoothly rotate from the current rotation to the target rotation at the defined speed
        // Time.deltaTime makes it frame rate independent
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);

    }

    private Vector3 GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Length)
        {
            return transform.position;
        }
        Vector3 targetPoint = waypoints[waypointIndex].position;

        if (waypointIndex > 0)
        {
            float distance = Vector3.Distance(waypoints[waypointIndex].position, waypoints[waypointIndex - 1].position);
            totalDistance -= distance;
        }

        waypointIndex++;

        return targetPoint;
    }

    public Vector3 CenterPoint()
    {
        return centerPoint.position;
    }
    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
    public void TakeDamage(int damage)
    {
        healthPoints = healthPoints - damage;
        if (healthPoints < 0)
        {
            Destroy(gameObject);
        }
    }
}
