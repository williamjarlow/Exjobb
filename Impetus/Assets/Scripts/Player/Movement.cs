using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField]
    [Header("Horizontal variables")]
    [Tooltip("Max horizontal speed.")]
    float maxSpeed = 5;

    [SerializeField]
    [Tooltip("Max horizontal speed while sprinting.")]
    float sprintMaxSpeed = 5;

    [SerializeField]
    [Tooltip("Adds to your RB2D.velocity when moving.")]
    float accelConst = 2.5f;

    [SerializeField]
    [Tooltip("Adds to your RB2D.velocity while sprinting.")]
    float sprintAccelConst = 2.5f;

    [SerializeField]
    [Tooltip("Subtracts from your RB2D.velocity when not moving.")]
    float frictionConst = 0.6f;

    [SerializeField]
    [Tooltip("How strong friction is in the air. 1 = 100% strength.")]
    float airFrictionMult = 0.6f;

    [SerializeField]
    [Tooltip("Sets the size of the deadzone.")]
    float deadzone = 0.2f;

    [SerializeField]
    [Header("Vertical variables")]
    [Tooltip("Adds downward RB2D.velocity constantly.")]
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
    [Tooltip("Divides upward RB2D.velocity by this when letting go of space early in a jump.")]
    int jumpCutoffDiv = 7;

    [SerializeField]
    [Header("Jump charge variables")]
    [Tooltip("How long it takes for the jump to charge.")]
    int jumpChargeTime = 15;

    [SerializeField]
    [Tooltip("Height multiplier for charged jumps.")]
    float jumpChargeMult = 1.2f;

    Rigidbody2D RB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Transform parentTransform;

    [HideInInspector]
    public bool onGround;

    bool canJump, jumpActive, jumpCharged, sprinting;
    int jumpBuffer, jumpTimer, jumpGraceTimer, jumpChargeTimer, deadzoneMult;
    float jumpTimePercentage, _jumpChargeMult = 1;
    Vector2 velocity;
    ParticleSystem particleSystem;

    Vector3 spawnPosition;

    [SerializeField]
    [Header("Collision Detection")]
    [Tooltip("The layer mask used in floor detection.")]
    LayerMask environmentLayerMask;

    [SerializeField]
    LayerMask barrierLayerMask, teleportLayerMask;

    bool jump;

    // Use this for initialization
    void Start()
    {
        spawnPosition = GetComponentInParent<Transform>().position;
        RB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        parentTransform = GetComponentInParent<Transform>();
        animator = GetComponentInParent<Animator>();
        particleSystem = GetComponentInParent<ParticleSystem>();
    }

    void Update()
    {
        if (!jump && Input.GetButtonDown("Jump"))
            jump = true;
    }

    void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
    }

    void Jump()
    {
        jumpTimer = 0;
        jumpActive = true;
        animator.SetTrigger("Jumping");
        jumpChargeTimer = 0;
        jump = false;
    }

    void MoveHorizontal()
    {
        float direction = Mathf.Sign(RB2D.velocity.x);

        if (Input.GetAxis("Horizontal") > deadzone || Input.GetAxis("Horizontal") < -deadzone)
            deadzoneMult = 1;
        else
            deadzoneMult = 0;

        float inputDirection = Input.GetAxisRaw("Horizontal") * deadzoneMult;
        if (onGround) //Ground movement
        {
            if (inputDirection != 0)
            {
                if (Input.GetButton("Sprint")) //If sprinting
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        animator.SetTrigger("Running");

                    if (particleSystem.isStopped && RB2D.velocity.x != 0)
                        particleSystem.Play();
                    else if (RB2D.velocity.x == 0)
                        particleSystem.Stop();

                    //Going too fast in a direction
                    if (Mathf.Abs(RB2D.velocity.x) > sprintMaxSpeed)
                        RB2D.velocity -= new Vector2(sprintAccelConst * direction, 0);

                    //Accelerating in a direction
                    else
                    {
                        //Accelerate, or set speed to maxSpeed to stop fluctuations above the limit
                        if (Mathf.Abs(RB2D.velocity.x) + Mathf.Abs(sprintAccelConst * inputDirection) < sprintMaxSpeed)
                            RB2D.velocity += new Vector2(sprintAccelConst * inputDirection, 0);
                        else if (inputDirection == direction)
                            RB2D.velocity = new Vector2(sprintMaxSpeed * inputDirection, RB2D.velocity.y);
                        else
                            RB2D.velocity -= new Vector2(frictionConst * direction, 0);
                    }
                }
                else //If not sprinting
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        animator.SetTrigger("Running");

                    //Going too fast in a direction
                    if (Mathf.Abs(RB2D.velocity.x) > maxSpeed)
                        RB2D.velocity -= new Vector2(accelConst * direction, 0);

                    //Accelerating in a direction
                    else
                    {
                        //Accelerate, or set speed to maxSpeed to stop fluctuations above the limit
                        if (Mathf.Abs(RB2D.velocity.x) + Mathf.Abs(accelConst * inputDirection) < maxSpeed)
                            RB2D.velocity += new Vector2(accelConst * inputDirection, 0);
                        else if (inputDirection == direction)
                            RB2D.velocity = new Vector2(maxSpeed * inputDirection, RB2D.velocity.y);
                        else
                            RB2D.velocity -= new Vector2(frictionConst * direction, 0);
                    }
                }
                
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }

            else //Not holding any key, stopping
            {
                if (particleSystem.isEmitting)
                    particleSystem.Stop();

                if (Mathf.Abs(RB2D.velocity.x) < frictionConst)
                    RB2D.velocity = new Vector2(0, RB2D.velocity.y);
                else
                    RB2D.velocity -= new Vector2(frictionConst * direction, 0);
            }

            
        }
        else //Air movement
        {
            if (particleSystem.isEmitting)
                particleSystem.Stop();

            if (inputDirection != 0)
            {
                if (Input.GetButton("Sprint")) //If sprinting
                {
                    //Accelerate when below max speed
                    if (Mathf.Abs(RB2D.velocity.x) + Mathf.Abs(sprintAccelConst * inputDirection) < sprintMaxSpeed)
                        RB2D.velocity += new Vector2(sprintAccelConst * inputDirection, 0);
                    //Decelerate when above max speed
                    else if (inputDirection == direction)
                        RB2D.velocity -= new Vector2(frictionConst * airFrictionMult * inputDirection, 0);
                    //Decelerate when changing direction
                    else
                        RB2D.velocity -= new Vector2((sprintAccelConst + frictionConst * airFrictionMult) * direction, 0);
                }
                else
                {
                    //Accelerate when below max speed
                    if (Mathf.Abs(RB2D.velocity.x) + Mathf.Abs(accelConst * inputDirection) < maxSpeed)
                        RB2D.velocity += new Vector2(accelConst * inputDirection, 0);
                    //Decelerate when above max speed
                    else if (inputDirection == direction)
                        RB2D.velocity -= new Vector2(frictionConst * airFrictionMult * inputDirection, 0);
                    //Decelerate when changing direction
                    else
                        RB2D.velocity -= new Vector2((accelConst + frictionConst * airFrictionMult) * direction, 0);
                }

                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else //Not holding any key, stopping
            {
                if (Mathf.Abs(RB2D.velocity.x) < frictionConst * airFrictionMult)
                    RB2D.velocity = new Vector2(0, RB2D.velocity.y);
                else
                    RB2D.velocity -= new Vector2(frictionConst * airFrictionMult * direction, 0);
            }
        }

    }

    void HandleDuckInput()
    {

        bool duckingInput = Input.GetAxisRaw("Vertical") < 0;

        bool idleState = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        bool duckingState = animator.GetCurrentAnimatorStateInfo(0).IsName("Ducking");
        bool chargingState = animator.GetCurrentAnimatorStateInfo(0).IsName("ChargedJump");

        if (onGround)
        {
            
            //Ducking
            if (idleState && duckingInput)
                animator.SetTrigger("Ducking");

            //Charging jump
            if (duckingState && duckingInput)
            {
                jumpChargeTimer++;
                if (!chargingState && jumpChargeTimer >= jumpChargeTime)
                {
                    animator.SetTrigger("ChargedJump");
                    _jumpChargeMult = jumpChargeMult;
                }
            }
        }
        //Cancelling charge
        if ((duckingState || chargingState) && (!duckingInput || (Input.GetAxis("Horizontal") > deadzone || Input.GetAxis("Horizontal") < -deadzone) || !onGround))
        {
            animator.SetTrigger("Idle");
            _jumpChargeMult = 1;
            jumpChargeTimer = 0;
        }
    }

    void MoveVertical()
    {
        canJump = (onGround == true || jumpGraceTimer > 0) && jumpActive == false;
        jumpGraceTimer = onGround ? jumpGraceMax : jumpGraceTimer - 1;

        if (jump && canJump)
        {
            Jump();
        }
        //For ducking and charge jumping
        HandleDuckInput();

        if (jumpActive && jumpTimer < jumpMaxTime)
        {
            if (jumpTimer < jumpMinTime || Input.GetButton("Jump"))
            {
                //If at the end of the jump
                if (jumpTimer >= jumpChangeTime)
                {
                    jumpTimePercentage = 1 - ((float)(jumpTimer - jumpChangeTime) / jumpMaxTime);
                    RB2D.velocity = new Vector2(RB2D.velocity.x, jumpHeight * _jumpChargeMult * jumpTimePercentage);
                }

                else
                {
                    RB2D.velocity = new Vector2(RB2D.velocity.x, jumpHeight * _jumpChargeMult);
                }
            }
            //If letting go of jump early
            else if (!Input.GetButton("Jump"))
            {
                jumpActive = false;
                if (RB2D.velocity.y > 0)
                {
                    RB2D.velocity = new Vector2(RB2D.velocity.x, RB2D.velocity.y / jumpCutoffDiv);
                }
            }
            jumpTimer++;
        }

        else if (jumpActive)
            jumpActive = false;

        //Add gravity
        if (RB2D.velocity.y > -terminalVelocity)
            RB2D.velocity -= new Vector2(0, gravityConst);
        if(!Input.GetButtonDown("Jump"))
            jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value || temp == barrierLayerMask.value)
        {
            onGround = true;
            _jumpChargeMult = 1;
            animator.SetTrigger("Landing");
        }
        if (temp == teleportLayerMask.value)
            RB2D.transform.position = spawnPosition;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value || temp == barrierLayerMask.value)
        {
            onGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value || temp == barrierLayerMask.value)
            onGround = false;
    }
}
