using UnityEngine;
using System.Collections;

public class CloneMovement : MonoBehaviour
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
    int jumpBuffer, jumpTimer, jumpGraceTimer, jumpChargeTimer;
    float jumpTimePercentage, _jumpChargeMult = 1;
    Vector2 velocity;

    [SerializeField]
    [Header("Collision Detection")]
    [Tooltip("The layer mask used in floor detection.")]
    LayerMask environmentLayerMask;

    // Use this for initialization
    void Start()
    {
        RB2D = GetComponentInParent<Rigidbody2D>();
        parentTransform = GetComponentInParent<Transform>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    void MoveHorizontal()
    {
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
    }

    void MoveVertical()
    {
        //Add gravity
        if (RB2D.velocity.y > -terminalVelocity)
            RB2D.velocity -= new Vector2(0, gravityConst);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value)
        {
            onGround = true;
            _jumpChargeMult = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value)
            onGround = false;
    }
}
