using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIndependantGameObject : StateMachineBehaviour {
    [SerializeField][Header("Instantiates prefab independently of animator object.")]
    GameObject prefab;
    [SerializeField][Tooltip("Check this if instantiated object's position should be equal to animator object's position.")]
    bool inheritPosition;
    [SerializeField]
    Vector2 position;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!inheritPosition)
            Instantiate(prefab, position, new Quaternion());
        else
            Instantiate(prefab, animator.transform.position, animator.transform.rotation);
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
