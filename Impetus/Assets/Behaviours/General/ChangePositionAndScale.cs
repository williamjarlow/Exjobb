using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionAndScale : StateMachineBehaviour {

    float time;
    [SerializeField]
    Vector3 targetPosition, targetScale;
    [SerializeField]
    string timeParameterName;

    Transform transform;
    float timePassed;
    float initialTime;
    float stepSizePos;
    float stepSizeScale;
    float distance;
    float scaleDifference;
     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = animator.GetFloat(timeParameterName);
        transform = animator.transform;
        if (time == 0)
            time = 0.0001f;
        initialTime = Time.timeSinceLevelLoad;
        timePassed = 0;

        distance = Vector3.Distance(transform.position, targetPosition);
        scaleDifference = Vector3.Distance(transform.localScale, targetScale);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        timePassed = Time.timeSinceLevelLoad-initialTime;
        initialTime = Time.timeSinceLevelLoad;
        stepSizePos = (distance * timePassed)/time;
        stepSizeScale = (scaleDifference * timePassed) / time;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, stepSizePos);
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, stepSizeScale);
    }

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
