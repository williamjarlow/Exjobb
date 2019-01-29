using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeParameter : StateMachineBehaviour {

    [SerializeField]
    string paramName;
    [Tooltip("Ignore this for boolean.")]
    [SerializeField]
    Vector2 randomRange;
    [SerializeField]
    AnimatorControllerParameter param;
    
    [SerializeField]
    [TextArea(3, 3)]
    string warning = "Ensure that the initial value of the parameter does not trigger any unwanted conditions.";

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (AnimatorControllerParameter p in animator.parameters)
            if (p.name == paramName)
                param = p;

        switch (param.type)
        {
            case AnimatorControllerParameterType.Trigger:
                animator.SetTrigger(paramName);
                break;
            case AnimatorControllerParameterType.Bool:
                bool randomBool;
                randomBool = Random.Range(0, 2) == 0 ? false : true;
                animator.SetBool(paramName, randomBool);
                break;
            case AnimatorControllerParameterType.Int:
                int randomInt;
                randomInt = Random.Range((int)randomRange.x, (int)randomRange.y);
                animator.SetInteger(paramName, randomInt);
                break;
            case AnimatorControllerParameterType.Float:
                float randomFloat;
                randomFloat = Random.Range(randomRange.x, randomRange.y);
                animator.SetFloat(paramName, randomFloat);
                break;
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
