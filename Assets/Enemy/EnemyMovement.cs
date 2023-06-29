using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] Waypoints;
    public float DelayOnPoint = 0.5f;
    
    private int currentWaypointIndex;   

    private NavMeshAgent navAgent;  
    private Animator animator;

    private FieldOfView fieldOfView;

    public bool CanSeePlayer;
    public bool IsMoved = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        
        currentWaypointIndex = 0;
    }

    public void StartMove()
    {
        IsMoved = true;
        MoveToWaypoint(Waypoints[currentWaypointIndex]);
    }

    void Update()
    {
        if (IsMoved) PatrolMoving();

        CanSeePlayer = fieldOfView.canSeePlayer;
    }
    

    void PatrolMoving()
    {
        // Если враг достиг текущей точки, перемещаем его к следующей
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            currentWaypointIndex++;
            
            // Если достигнут конец массива точек, враг возвращается к первой точке
            if (currentWaypointIndex >= Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            MoveToWaypoint(Waypoints[currentWaypointIndex]);
            
            animator.SetBool("isPatrolling", false);
            StopMove();
        }
    }

    public void StopMove()
    {
        navAgent.SetDestination(transform.position);
    }

    void MoveToWaypoint(Transform waypoint)
    {
        // Перемещение врага к указанной точке
        navAgent.SetDestination(waypoint.position);
    }
}