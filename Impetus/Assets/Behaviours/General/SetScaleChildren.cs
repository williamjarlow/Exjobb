using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScaleChildren : StateMachineBehaviour {

    [SerializeField]
    Vector3 scale;

    [SerializeField]
    [Header("Optional Parameters")]
    string scaleXName, scaleYName, scaleZName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (scaleXName != "")
            scale.x = animator.GetFloat(scaleXName);
        if (scaleYName != "")
            scale.y = animator.GetFloat(scaleYName);
        if (scaleZName != "")
            scale.z = animator.GetFloat(scaleZName);
        foreach (Transform transform in animator.GetComponentInChildren<Transform>())
        {
            transform.localScale = scale;
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
