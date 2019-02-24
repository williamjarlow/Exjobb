using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField]
    [Header("Horizontal variables")]
    [Tooltip("Max horizontal speed")]
    float maxSpeed = 5;

    [SerializeField]
    [Tooltip("Deceleration multiplier when changing direction")]
    float decelMult = 0.7f;

    [SerializeField]
    [Tooltip("How fast you accelerate")]
    float accelConst = 2.5f;

    [SerializeField]
    [Tooltip("It's friction")]
    float frictionConst = 0.6f;

    [SerializeField]
    [Tooltip("It's friction in the air")]
    float airFrictionConst = 0.6f;

    [SerializeField]
    [Header("Vertical variables")]
    [Tooltip("Added downward velocity every frame")]
    float gravityConst = 0.8f;

    [SerializeField]
    [Tooltip("Max falling speed")]
    float terminalVelocity = 10;

    [SerializeField]
    [Tooltip("How high the player will go every frame of a jump")]
    int jumpHeight = 12;

    [SerializeField]
    [Tooltip("Jump buffer length")][Header ("Time variables in frames")]
    int jumpBufferMax = 3;

    [SerializeField]
    [Tooltip("Grace time when falling off a platform")]
    int jumpGraceMax = 2;

    [SerializeField]
    [Tooltip("How long the player can jump for")]
    int jumpMaxTime = 15;

    [SerializeField]
    [Tooltip("Short hop length")]
    int jumpMinTime = 2;

    [SerializeField]
    [Tooltip("How long it will take for the jump to turn into a curve")]
    int jumpChangeTime = 7;

    Rigidbody2D thisRB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool onGround, canJump, jumpActive;
    int jumpBuffer, jumpTimer, jumpGraceTimer;

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
        canJump = false;
       // thisRB2D.velocity = new Vector2(thisRB2D.velocity.x, jumpSpeed);
    }

    void MoveHorizontal()
    {
        float direction = Mathf.Sign(thisRB2D.velocity.x);
        float inputDirection = Input.GetAxisRaw("Horizontal");
        if (onGround) //Ground movement
        {
            Debug.Log(thisRB2D.velocity.x);
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
                    else
                    {
                        if (inputDirection == direction)
                            thisRB2D.velocity = new Vector2(maxSpeed * inputDirection, thisRB2D.velocity.y);
                        else
                            thisRB2D.velocity -= new Vector2(frictionConst * direction, 0);
                    }
                }

                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else
            {
                //Not holding any key, stopping
                thisRB2D.velocity -= new Vector2(frictionConst * direction, 0);

                //Stops fluctuations while standing still
                if (thisRB2D.velocity.x < 0 && direction > 0 || thisRB2D.velocity.x > 0 && direction < 0)
                    thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
            }
        }
        else //Air movement
        {
            if (inputDirection != 0)
            {
                spriteRenderer.flipX = inputDirection > 0;
                //If not going max speed
                if (!(Mathf.Abs(thisRB2D.velocity.x) > maxSpeed))
                {
                    //Accelerate
                    //thisRB2D.velocity += new Vector2(acceleration * inputDirection, 0);
                }
                else
                {

                }
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
        }

    }

    void MoveVertical()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        onGround = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
    }
    
}
