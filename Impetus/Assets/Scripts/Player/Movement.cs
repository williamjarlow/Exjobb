using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField]
    [Header("Horizontal variables")]
    [Tooltip("Max horizontal speed.")]
    float maxSpeed = 5;

    [SerializeField]
    [Tooltip("Adds to your velocity when moving.")]
    float accelConst = 2.5f;

    [SerializeField]
    [Tooltip("Same as the above, except in the air.")]
    float airAccelConst = 1;

    [SerializeField]
    [Tooltip("Subtracts from your velocity when not moving.")]
    float frictionConst = 0.6f;

    [SerializeField]
    [Tooltip("How strong friction is in the air. 1 = 100% strength.")]
    float airFrictionMult = 0.6f;

    [SerializeField]
    [Header("Vertical variables")]
    [Tooltip("Adds downward velocity constantly.")]
    float gravityConst = 0.8f;

    [SerializeField]
    [Tooltip("Max falling speed.")]
    float terminalVelocity = 10;

    [SerializeField]
    [Tooltip("Added every frame of a jump, kind of.")]
    int jumpHeight = 4;

    [SerializeField]
    [Tooltip("Buffer time when pressing jump before hitting a platform.")][Header ("Time variables in frames")]
    int jumpBufferMax = 3;

    [SerializeField]
    [Tooltip("Grace time when falling off a platform.")]
    int jumpGraceMax = 2;

    [SerializeField]
    [Tooltip("Max time holding space will make you jump for.")]
    int jumpMaxTime = 15;

    [SerializeField]
    [Tooltip("Minimum jump time.")]
    int jumpMinTime = 2;

    [SerializeField]
    [Tooltip("How long it takes for a jump to reach the apex.")]
    int jumpChangeTime = 7;

    [SerializeField]
    [Tooltip("Divides upward velocity by this when letting go of space early in a jump")]
    int jumpCutoffDiv = 7;

    Rigidbody2D thisRB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool onGround, canJump, jumpActive;
    int jumpBuffer, jumpTimer, jumpGraceTimer;
    float jumpTimePercentage;

    // Use this for initialization
    void Start()
    {
        thisRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    void Jump()
    {
        jumpTimer = 0;
        jumpActive = true;
    }

    void MoveHorizontal()
    {
        float direction = Mathf.Sign(thisRB2D.velocity.x);
        float inputDirection = Input.GetAxisRaw("Horizontal");
        if (onGround) //Ground movement
        {
            if (inputDirection != 0)
            {
                //Going too fast in a direction
                if (Mathf.Abs(thisRB2D.velocity.x) > maxSpeed)
                    thisRB2D.velocity -= new Vector2(accelConst * direction, 0);

                //Accelerating in a direction
                else
                {
                    //Accelerate, or set speed to maxSpeed to stop fluctuations above the limit
                    if (Mathf.Abs(thisRB2D.velocity.x) + Mathf.Abs(accelConst * inputDirection) < maxSpeed)
                        thisRB2D.velocity += new Vector2(accelConst * inputDirection, 0);
                    else if (inputDirection == direction)
                        thisRB2D.velocity = new Vector2(maxSpeed * inputDirection, thisRB2D.velocity.y);
                    else
                        thisRB2D.velocity -= new Vector2(frictionConst * direction, 0);
                }
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else //Not holding any key, stopping
            {
                if (Mathf.Abs(thisRB2D.velocity.x) < frictionConst)
                    thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
                else
                    thisRB2D.velocity -= new Vector2(frictionConst * direction, 0);
            }
        }
        else //Air movement
        {
            if (inputDirection != 0)
            {
                //Accelerate when below max speed
                if (Mathf.Abs(thisRB2D.velocity.x) + Mathf.Abs(airAccelConst * inputDirection) < maxSpeed)
                    thisRB2D.velocity += new Vector2(airAccelConst * inputDirection, 0);
                //Decelerate when above max speed
                else if (inputDirection == direction)
                    thisRB2D.velocity -= new Vector2(frictionConst * airFrictionMult * inputDirection, 0);
                //Decelerate when changing direction
                else
                    thisRB2D.velocity -= new Vector2((airAccelConst + frictionConst * airFrictionMult) * direction, 0);

                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else //Not holding any key, stopping
            {
                if (Mathf.Abs(thisRB2D.velocity.x) < frictionConst * airFrictionMult)
                    thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
                else
                    thisRB2D.velocity -= new Vector2(frictionConst * airFrictionMult * direction, 0);
                
            }
        }

    }

    void MoveVertical()
    {
        canJump = (onGround == true || jumpGraceTimer < jumpGraceMax) && jumpActive == false;
        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        if (jumpActive && jumpTimer < jumpMaxTime)
        {
            if (jumpTimer < jumpMinTime || Input.GetButton("Jump"))
            {
                //If at the end of the jump
                if (jumpTimer >= jumpChangeTime)
                {
                    jumpTimePercentage = 1 - ((float)(jumpTimer - jumpChangeTime) / jumpMaxTime);
                    thisRB2D.velocity = new Vector2(thisRB2D.velocity.x, jumpHeight * jumpTimePercentage);
                    print(jumpTimePercentage);
                }

                else
                {
                    thisRB2D.velocity = new Vector2(thisRB2D.velocity.x, jumpHeight);
                }
            }

            //If letting go of jump early
            else if (!Input.GetButton("Jump")){
                jumpActive = false;
                if (thisRB2D.velocity.y > 0){
                    thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y / jumpCutoffDiv);
                }
            }
            jumpTimer++;
        }

        else if (jumpActive)
            jumpActive = false;

        //Add gravity
        if (thisRB2D.velocity.y > -terminalVelocity)
            thisRB2D.velocity -= new Vector2(0, gravityConst);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        onGround = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //Skiten är sen, fixa senare, kanske raycast
        onGround = false;
    }
    
}
