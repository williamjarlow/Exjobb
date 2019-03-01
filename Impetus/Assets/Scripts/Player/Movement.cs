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
    [Tooltip("Divides upward velocity by this when letting go of space early in a jump.")]
    int jumpCutoffDiv = 7;

    Rigidbody2D thisRB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Transform parentTransform;

    bool onGround, canJump, jumpActive;
    int jumpBuffer, jumpTimer, jumpGraceTimer;
    float jumpTimePercentage;
    Vector2 speed;

    [SerializeField]
    [Header("Collision Detection")]
    [Tooltip("The layer mask used in floor detection.")]
    LayerMask environmentLayerMask;

    [SerializeField]
    [Tooltip("How close a floor has to be for the player to be counted as on the ground.")]
    float leeway;

    // Use this for initialization
    void Start()
    {
        thisRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        parentTransform = GetComponentInParent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveHorizontal();
        MoveVertical();
        DetectCollisions();
        ApplySpeed();
    }

    void Jump()
    {
        jumpTimer = 0;
        jumpActive = true;
    }

    void DetectCollisions()
    {
        float playerWidth = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit;
        float playerHeight = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit / 2;

        RaycastHit2D ray = Physics2D.BoxCast(parentTransform.position, new Vector2(playerWidth, playerHeight), 0, speed, speed.magnitude, environmentLayerMask.value);
        if (ray.collider != null)
        {
            onGround = (ray.distance < playerHeight + leeway);
            //William kommer ihåg
            Vector2 collisionDir = new Vector2(parentTransform.position.x, parentTransform.position.y) - ray.point;
        }
        jumpGraceTimer = onGround ? jumpGraceMax : jumpGraceTimer - 1;
    }

    void MoveHorizontal()
    {
        float direction = Mathf.Sign(speed.x);
        float inputDirection = Input.GetAxisRaw("Horizontal");
        if (onGround) //Ground movement
        {
            if (inputDirection != 0)
            {
                //Going too fast in a direction
                if (Mathf.Abs(speed.x) > maxSpeed)
                    speed -= new Vector2(accelConst * direction, 0);

                //Accelerating in a direction
                else
                {
                    //Accelerate, or set speed to maxSpeed to stop fluctuations above the limit
                    if (Mathf.Abs(speed.x) + Mathf.Abs(accelConst * inputDirection) < maxSpeed)
                        speed += new Vector2(accelConst * inputDirection, 0);
                    else if (inputDirection == direction)
                        speed = new Vector2(maxSpeed * inputDirection, speed.y);
                    else
                        speed -= new Vector2(frictionConst * direction, 0);
                }
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else //Not holding any key, stopping
            {
                if (Mathf.Abs(speed.x) < frictionConst)
                    speed = new Vector2(0, speed.y);
                else
                    speed -= new Vector2(frictionConst * direction, 0);
            }
        }
        else //Air movement
        {
            if (inputDirection != 0)
            {
                //Accelerate when below max speed
                if (Mathf.Abs(speed.x) + Mathf.Abs(airAccelConst * inputDirection) < maxSpeed)
                    speed += new Vector2(airAccelConst * inputDirection, 0);
                //Decelerate when above max speed
                else if (inputDirection == direction)
                    speed -= new Vector2(frictionConst * airFrictionMult * inputDirection, 0);
                //Decelerate when changing direction
                else
                    speed -= new Vector2((airAccelConst + frictionConst * airFrictionMult) * direction, 0);

                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else //Not holding any key, stopping
            {
                if (Mathf.Abs(speed.x) < frictionConst * airFrictionMult)
                    speed = new Vector2(0, speed.y);
                else
                    speed -= new Vector2(frictionConst * airFrictionMult * direction, 0);
            }
        }

    }

    void MoveVertical()
    {
        canJump = (onGround == true || jumpGraceTimer > 0) && jumpActive == false;
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
                    speed = new Vector2(speed.x, jumpHeight * jumpTimePercentage);
                }

                else
                {
                    speed = new Vector2(speed.x, jumpHeight);
                }
            }

            //If letting go of jump early
            else if (!Input.GetButton("Jump")){
                jumpActive = false;
                if (speed.y > 0){
                    speed = new Vector2(speed.x, speed.y / jumpCutoffDiv);
                }
            }
            jumpTimer++;
        }

        else if (jumpActive)
            jumpActive = false;

        //Add gravity
        if (speed.y > -terminalVelocity)
            speed -= new Vector2(0, gravityConst);
    }

    void ApplySpeed()
    {
        thisRB2D.position += speed * Time.deltaTime;
    }
    
}
