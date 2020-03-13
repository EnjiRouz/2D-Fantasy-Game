using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAnimationFix : StateMachineBehaviour {

    /// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().isTakingDamage = true;
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    /// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().isTakingDamage = false;
    }
}