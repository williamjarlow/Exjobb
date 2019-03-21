using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackLeft : Skill
{
    public override void OnSkillStart()
    {

    }
    public override void OnSkillUpdate()
    {
        Debug.Log("Normal attack");
    }
    public override void OnSkillStop()
    {
        
    }
    public override bool SkillInputPerformed()
    {
        float horizontalInputDirection = Input.GetAxisRaw("Horizontal");
        return (Input.GetButtonDown("Attack") && horizontalInputDirection < 0);
    }
    public override bool SkillIsUsable()
    {
        return base.SkillIsUsable();
    }
}
