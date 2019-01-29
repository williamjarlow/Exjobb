using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectEnabled : StateMachineBehaviour {
    [SerializeField]
    float delay;
    [SerializeField]
    string target;
    [SerializeField, Tooltip("Is target string a Name or a Tag?")]
    bool tag;
    [SerializeField]
    bool enabled;
    GameObject targetObject;

    float initialTime;
    float timePassed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetObject = tag ? GameObject.FindWithTag(target) : GameObject.Find(target);
        timePassed = 0;
        initialTime = Time.timeSinceLevelLoad;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timePassed != Mathf.NegativeInfinity)
            timePassed = Time.timeSinceLevelLoad - initialTime;
        if (timePassed >= delay)
        {
            timePassed = Mathf.NegativeInfinity;
            targetObject.SetActive(enabled);
        }
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
