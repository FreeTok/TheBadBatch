using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    float timer;
    private EnemyMovement _enemyMovement;
    
    //Transform player;
    //float chaseRange = 10;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        _enemyMovement = animator.GetComponent<EnemyMovement>();
        //timer = _enemyMovement.DelayOnPoint;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > _enemyMovement.DelayOnPoint)
        {
            if (!animator.GetBool("isShooting"))
            {
                animator.SetBool("isPatrolling", true);
            }
        }
        
        if (_enemyMovement.CanSeePlayer && !animator.GetBool("isPatrolling"))
        {
            animator.SetBool("isShooting", true);
        }

        //float distance = Vector3.Distance(animator.transform.position, player.position);
        //if (distance < chaseRange)
            //animator.SetBool("isChasing", true);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
