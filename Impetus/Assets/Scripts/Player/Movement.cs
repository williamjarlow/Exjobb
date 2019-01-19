using UnityEngine;
using System.Collections;
using AnimatorStateMachineUtil;

public class Movement : MonoBehaviour
{

    [SerializeField]
    float playerMaxSpeed, playerAcceleration, playerDeceleration, playerJumpSpeed;

    int currentJumpCharges = 0;
    [SerializeField]
    int maximumJumpCharges = 2;

    [SerializeField]
    Vector2 flashJumpStrength;
    
    bool jumpEnabled;
    bool grounded;
    bool flashJumpEnabled;

    Rigidbody2D thisRB2D;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        thisRB2D = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //MoveHorizontal();
        MoveVertical();
        SpecialMovement();
    }

    void MoveVertical()
    {
        if (Input.GetButtonDown("Jump") && jumpEnabled == true)
        {
            thisRB2D.velocity = new Vector2(thisRB2D.velocity.x, playerJumpSpeed);
            ExpendJumpCharge();
        }
    }

    void MoveHorizontal()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = true;
            if (thisRB2D.velocity.x > playerMaxSpeed)
            {
                if (grounded)
                    thisRB2D.velocity -= (new Vector2(playerDeceleration, 0));
            }
            else
                thisRB2D.velocity += new Vector2(playerAcceleration, 0);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = false;
            if (thisRB2D.velocity.x < -playerMaxSpeed)
            {
                if(grounded)
                    thisRB2D.velocity += new Vector2(playerDeceleration, 0);
            }
            else
                thisRB2D.velocity -= new Vector2(playerAcceleration, 0);
        }

        else if (thisRB2D.velocity.x > 0 && grounded)
        {
            thisRB2D.velocity -= new Vector2(playerDeceleration, 0);
            if (thisRB2D.velocity.x < 0)
                thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
        }
        else if (thisRB2D.velocity.x < 0 && grounded)
        {
            thisRB2D.velocity += (new Vector2(playerDeceleration, 0));
            if (thisRB2D.velocity.x > 0)
                thisRB2D.velocity = new Vector2(0, thisRB2D.velocity.y);
        }
    }

    void SpecialMovement()
    {
        if (Input.GetKeyDown(KeyCode.X) && flashJumpEnabled == true)
        {
            if (!spriteRenderer.flipX)
            {
                thisRB2D.velocity = new Vector2(-flashJumpStrength.x, flashJumpStrength.y);
                flashJumpEnabled = false;
            }
            else
            {
                thisRB2D.velocity = new Vector2(flashJumpStrength.x, flashJumpStrength.y);
                flashJumpEnabled = false;
            }
        }
    }

    void RefillJumpCharges()
    {
        currentJumpCharges = maximumJumpCharges;
        jumpEnabled = true;
    }

    void ExpendJumpCharge()
    {
        currentJumpCharges--;
        if (currentJumpCharges <= 0)
            jumpEnabled = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        grounded = true;
        flashJumpEnabled = true;
        RefillJumpCharges();

        GetComponent<Animator>().SetBool("OnGround", true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;

        GetComponent<Animator>().SetBool("OnGround", false);
    }
}
