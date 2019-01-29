using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineInFacingDirection : StateMachineBehaviour {

    Transform transform;
    LineRenderer lineRenderer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        lineRenderer = animator.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            Vector2 facingVec = new Vector2();
            facingVec.x = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            facingVec.y = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);

            SetLine(transform.position, facingVec * 1000);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (lineRenderer != null)
            SetLine(Vector3.zero, Vector3.zero);
    }

    void SetLine(Vector3 position, Vector3 target)
    {
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, target);
    }
}
