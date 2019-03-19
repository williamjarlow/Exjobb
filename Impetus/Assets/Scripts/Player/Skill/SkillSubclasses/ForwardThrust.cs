using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardThrust : Skill
{
    Rigidbody2D RB2D;
    [SerializeField]
    float speed;
    float inputDirection = 0;
    float direction;
    private void Start()
    {
        RB2D = player.GetComponent<Rigidbody2D>();
    }
    public override void OnSkillStart()
    {
        direction = inputDirection;
    }
    public override void OnSkillUpdate()
    {
        RB2D.velocity = new Vector2(direction * speed, 0);
        Debug.Log("bbb");
    }
    public override void OnSkillStop()
    {
        
    }
    public override bool SkillInputPerformed()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
        return (Input.GetButtonDown("Attack") && inputDirection != 0);
    }
}
