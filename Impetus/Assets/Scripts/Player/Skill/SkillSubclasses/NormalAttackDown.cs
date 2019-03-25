using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackDown : Skill
{
    [SerializeField]
    float force;
    Rigidbody2D RB2D;
    void Start()
    {
        RB2D = player.GetComponent<Rigidbody2D>();
    }

    public override void OnSkillStart()
    {
        RB2D.velocity = (new Vector2(RB2D.velocity.x, force));
    }
    public override void OnSkillUpdate()
    {
        
    }
    public override void OnSkillStop()
    {

    }
    public override bool SkillInputPerformed()
    {
        float inputDirVertical = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && inputDirVertical < 0);
    }
    public override bool SkillIsUsable()
    {
        return (base.SkillIsUsable() && !movement.onGround);
    }
}
