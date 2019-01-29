using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTarget : StateMachineBehaviour {
    [SerializeField]
    string speedParamName;
    float speed;
    [SerializeField]
    string target;
    [SerializeField, Tooltip("Is target string a Name or a Tag?")]
    bool tag;
    Transform targetTransform;
    
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        targetTransform = tag ? GameObject.FindWithTag(target).transform : GameObject.Find(target).transform;
        speed = animator.GetFloat(speedParamName);
    }
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Vector3 vectorToTarget = targetTransform.position - animator.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, q, speed);
    }

}
