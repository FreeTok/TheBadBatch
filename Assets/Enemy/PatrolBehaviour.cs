using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class PatrolBehaviour : StateMachineBehaviour
{
    
    private EnemyMovement _enemyMovement;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Запуск движения
        _enemyMovement = animator.GetComponent<EnemyMovement>();
        _enemyMovement.StartMove();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemyMovement.CanSeePlayer)
        {
            animator.SetBool("isShooting", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyMovement.IsMoved = false;
        _enemyMovement.StopMove();
        Debug.Log("PatrolEnded");
        //agent.SetDestination(agent.transform.position);
    }
}
