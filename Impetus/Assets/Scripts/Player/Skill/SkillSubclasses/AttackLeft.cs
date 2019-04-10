using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeft : Skill
{
    public override void OnSkillStart()
    {

    }
    public override void OnSkillUpdate()
    {
    }
    public override void OnSkillStop()
    {
        
    }
    public override bool SkillInputPerformed()
    {
        float horizontalInputDirection = Input.GetAxisRaw("Horizontal");
        return (Input.GetButtonDown("Attack") && (horizontalInputDirection < 0 || !player.GetComponent<SpriteRenderer>().flipX));
    }
    public override bool SkillIsUsable()
    {
        return base.SkillIsUsable();
    }
}
