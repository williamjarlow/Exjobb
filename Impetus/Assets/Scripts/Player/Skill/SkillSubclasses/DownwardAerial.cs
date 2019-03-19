using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownwardAerial : Skill
{
    public override void OnSkillStart()
    {

    }
    public override void OnSkillUpdate()
    {
        Debug.Log("aaa");
    }
    public override void OnSkillStop()
    {

    }
    public override bool SkillInputPerformed()
    {
        float inputDirHorizontal = Input.GetAxisRaw("Horizontal");
        float inputDirVertical = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && inputDirVertical < 0 && inputDirHorizontal == 0);
    }
    public override bool SkillIsUsable()
    {
        return (base.SkillIsUsable() && !movement.onGround);
    }
}
