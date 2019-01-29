using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAfterDelay : StateMachineBehaviour {
    [SerializeField]
    float delay = 1.0f;
    float timePassed;
    [SerializeField]
    string triggerParamName;
    [SerializeField, Header("Optional parameter")]
    string delayParamName;
    float initialTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timePassed = 0;
        if(delayParamName != "")
            delay = animator.GetFloat(delayParamName);
        initialTime = Time.timeSinceLevelLoad;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(timePassed != Mathf.NegativeInfinity)
            timePassed = Time.timeSinceLevelLoad-initialTime;
        if (timePassed >= delay)
        {
            timePassed = Mathf.NegativeInfinity;
            animator.SetTrigger(triggerParamName);
        }
    }
}
