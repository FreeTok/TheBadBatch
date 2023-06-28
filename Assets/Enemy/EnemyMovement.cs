using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;   
    private int currentWaypointIndex;   

    private NavMeshAgent navAgent;  
    private Animator animator;

    public bool IsMoved = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentWaypointIndex = 0;
    }

    public void StartMove()
    {
        IsMoved = true;
        MoveToWaypoint(waypoints[currentWaypointIndex]);
    }

    void Update()
    {
        if (!IsMoved) return;
        
        // Если враг достиг текущей точки, перемещаем его к следующей
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            currentWaypointIndex++;
            
            // Если достигнут конец массива точек, враг возвращается к первой точке
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            MoveToWaypoint(waypoints[currentWaypointIndex]);
            
            StopMove();
        }
    }

    void StopMove()
    {
        animator.SetBool("isPatrolling", false);
        navAgent.SetDestination(transform.position);
    }

    void MoveToWaypoint(Transform waypoint)
    {
        // Перемещение врага к указанной точке
        navAgent.SetDestination(waypoint.position);
    }
}