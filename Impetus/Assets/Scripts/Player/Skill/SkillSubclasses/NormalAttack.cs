using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Skill
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
        float verticalInputDirection = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && (horizontalInputDirection != 0 || verticalInputDirection > 0));
    }
    public override bool SkillIsUsable()
    {
        return base.SkillIsUsable();
    }
}
