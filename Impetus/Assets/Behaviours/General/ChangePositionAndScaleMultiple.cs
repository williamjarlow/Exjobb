using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Target
{
    public Vector3 targetPosition, targetScale;
    [HideInInspector]
    public Transform transform;
    public string name;
    [HideInInspector]
    public float stepSizePos, stepSizeScale, distance, scaleDifference;
}

public class ChangePositionAndScaleMultiple : StateMachineBehaviour {
    float time;
    [SerializeField]
    List<Target> targets;
    float timePassed;
    float initialTime;
    [SerializeField]
    string timeParameterName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = animator.GetFloat(timeParameterName);
        if (time == 0)
            time = 0.0001f;
        initialTime = Time.timeSinceLevelLoad;
        timePassed = 0;

        foreach (Target target in targets)
        {
            target.transform = GameObject.Find(target.name).transform;
            target.distance = Vector3.Distance(target.transform.localPosition, target.targetPosition);
            target.scaleDifference = Vector3.Distance(target.transform.localScale, target.targetScale);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timePassed = Time.timeSinceLevelLoad - initialTime;
        initialTime = Time.timeSinceLevelLoad;
        foreach (Target target in targets)
        {
            target.stepSizePos = (target.distance * timePassed) / time;
            target.stepSizeScale = (target.scaleDifference * timePassed) / time;
            target.transform.localPosition = Vector3.MoveTowards(target.transform.localPosition, target.targetPosition, target.stepSizePos);
            target.transform.localScale = Vector3.MoveTowards(target.transform.localScale, target.targetScale, target.stepSizeScale);
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
