using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : Skill
{
    public override void OnSkillStart()
    {

    }
    public override void OnSkillUpdate()
    {
        Debug.Log("Up attack");
    }
    public override void OnSkillStop()
    {
        
    }
    public override bool SkillInputPerformed()
    {
        float verticalInputDirection = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && verticalInputDirection > 0);
    }
    public override bool SkillIsUsable()
    {
        return base.SkillIsUsable();
    }
}
