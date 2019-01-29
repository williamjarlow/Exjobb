using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInSetDirection : StateMachineBehaviour {

    [SerializeField]
    string speedParamName, directionXParamName, directionYParamName;

    Vector2 movementDirection = Vector2.zero;
    float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = animator.GetFloat(speedParamName);
        movementDirection.x = animator.GetFloat(directionXParamName);
        movementDirection.y = animator.GetFloat(directionYParamName);
        movementDirection = movementDirection.normalized;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Move(animator.transform);
	}

    void Move(Transform transform)
    {
        Vector3 step = movementDirection*speed*Time.deltaTime;
        transform.position += step;
    }
}
