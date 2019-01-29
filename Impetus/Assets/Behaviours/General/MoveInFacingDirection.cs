using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInFacingDirection : StateMachineBehaviour {
    float speed;

    [SerializeField]
    string speedParameterName;

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        speed = animator.GetFloat(speedParameterName);
        Move(animator.transform);
	}

    void Move(Transform transform)
    {
        Vector2 facingVec = new Vector2();
        facingVec.x = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        facingVec.y = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);

        Vector3 step = facingVec*speed*Time.deltaTime;
        transform.position += step;
    }
}
