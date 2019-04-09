using UnityEngine;
using System.Collections;

public class CloneMovement : MonoBehaviour
{

    [SerializeField]
    [Header("Vertical variables")]
    [Tooltip("Adds downward RB2D.velocity constantly.")]
    float gravityConst = 0.8f;

    [SerializeField]
    [Tooltip("Max falling speed.")]
    float terminalVelocity = 10;

    Rigidbody2D RB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Transform parentTransform;

    [HideInInspector]
    public bool onGround = false;

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
        MoveVertical();
    }

    void MoveVertical()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int temp = 1 << collision.gameObject.layer; //bitshift to make layermask work.
        if (temp == environmentLayerMask.value)
            onGround = false;
    }
}
