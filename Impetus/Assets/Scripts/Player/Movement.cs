using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float acceleration, deceleration, maxSpeed, jumpSpeed;
    [SerializeField]
    int maxJumpCharges, jumpCharges;
    Rigidbody2D thisRB2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool onGround;

    // Use this for initialization
    void Start()
    {
        thisRB2D = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (onGround)
            jumpCharges = maxJumpCharges;

        MoveHorizontal();
        if (Input.GetButtonDown("Jump") && jumpCharges > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        jumpCharges--;
        thisRB2D.velocity = new Vector2(thisRB2D.velocity.x, jumpSpeed);
    }

    void MoveHorizontal()
    {
        float direction = Mathf.Sign(thisRB2D.velocity.x);
        float inputDirection = Input.GetAxisRaw("Horizontal");
        if (onGround) //Ground movement
        {
            if (inputDirection != 0)
            {
                if (Mathf.Abs(thisRB2D.velocity.x) > maxSpeed)
                    thisRB2D.velocity -= new Vector2(deceleration * direction, 0);
                else
                    thisRB2D.velocity += new Vector2(acceleration * inputDirection, 0);
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") > 0;
            }
            else
            {
                thisRB2D.velocity -= new Vector2(deceleration * direction, 0);
                if (thisRB2D.velocity.x < 0 && direction > 0 ||
                    thisRB2D.velocity.x > 0 && direction < 0)
                    thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
            }
        }
        else //Air movement
        {
            if (inputDirection != 0)
            {
                spriteRenderer.flipX = inputDirection > 0;
                if (inputDirection < 0)
                {
                    if (thisRB2D.velocity.x > -maxSpeed)
                        thisRB2D.velocity -= new Vector2(acceleration, 0);
                }
                else if (inputDirection > 0)
                {
                    if (thisRB2D.velocity.x < maxSpeed)
                        thisRB2D.velocity += new Vector2(acceleration, 0);
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        onGround = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
        jumpCharges--;
    }
}
