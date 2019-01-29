using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionForward : StateMachineBehaviour {
    [SerializeField, Tooltip("The name of the boolean parameter which serves as output.")]
    string boolParamName;
    [SerializeField]
    LayerMask target;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolParamName, false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(boolParamName, FacingTarget(animator.transform));
    }

    bool FacingTarget(Transform transform)
    {
        Vector2 facingVec = new Vector2();
        facingVec.x = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        facingVec.y = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        // Cast a ray toward the direction the object is facing.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingVec, Mathf.Infinity, target.value);

        // If it hits something...
        if (hit.collider != null)
            return true;
        return false;
    }

}
