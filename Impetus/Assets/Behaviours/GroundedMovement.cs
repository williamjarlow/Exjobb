using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedMovement : StateMachineBehaviour {

    [SerializeField]
    string groundAccelerationName, groundDecelerationName, groundMaxSpeedName;

    float acceleration;
    float deceleration;
    float maxSpeed;

    SpriteRenderer spriteRenderer;
    Rigidbody2D thisRB2D;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        acceleration = animator.GetFloat(groundAccelerationName);
        deceleration = animator.GetFloat(groundDecelerationName);
        maxSpeed = animator.GetFloat(groundMaxSpeedName);
        spriteRenderer = animator.GetComponentInParent<SpriteRenderer>();
        thisRB2D = animator.GetComponentInParent<Rigidbody2D>();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        MoveHorizontal();
    }
    void MoveHorizontal()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = true;
            if (thisRB2D.velocity.x > maxSpeed)
            {//if not grounded, don't slow down
                thisRB2D.velocity -= new Vector2(deceleration, 0);
            }
            else
                thisRB2D.velocity += new Vector2(acceleration, 0);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = false;
            if (thisRB2D.velocity.x < -maxSpeed)
            {
                    thisRB2D.velocity += new Vector2(deceleration, 0);
            }
            else
                thisRB2D.velocity -= new Vector2(acceleration, 0);
        }

        else if (thisRB2D.velocity.x > 0)//only if grounded
        {
            thisRB2D.velocity -= new Vector2(deceleration, 0);
            if (thisRB2D.velocity.x < 0)
                thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
        }
        else if (thisRB2D.velocity.x < 0)//only if grounded
        {
            thisRB2D.velocity += (new Vector2(deceleration, 0));
            if (thisRB2D.velocity.x > 0)
                thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
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
