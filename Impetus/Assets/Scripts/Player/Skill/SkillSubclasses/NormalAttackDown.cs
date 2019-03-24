using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackDown : Skill
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
        float inputDirVertical = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && inputDirVertical < 0);
    }
    public override bool SkillIsUsable()
    {
        return (base.SkillIsUsable() && !movement.onGround);
    }
}
