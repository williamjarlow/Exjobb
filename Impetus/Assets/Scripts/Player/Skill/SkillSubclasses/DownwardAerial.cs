using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownwardAerial : Skill
{
    Rigidbody2D RB2D;
    [SerializeField]
    float speed;
    float inputDirection = 0;
    private void Start()
    {
        RB2D = player.GetComponent<Rigidbody2D>();
    }
    public override void StartSkill()
    {

    }
    public override void UpdateSkill()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
        RB2D.velocity += inputDirection * new Vector2(speed, 0);
    }
    public override void StopSkill()
    {

    }
    public override bool HandleInput()
    {
        return (Input.GetButtonDown("Attack") && inputDirection != 0);
    }
    public override bool SkillIsUsable()
    {
        base.SkillIsUsable();
        return false;
    }
}
