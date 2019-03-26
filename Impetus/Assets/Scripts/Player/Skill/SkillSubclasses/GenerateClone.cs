using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateClone : Skill
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
        if (Input.GetButtonDown("Clone"))
            Debug.Log("reeee");
        return Input.GetButtonDown("Clone");
    }
    public override bool SkillIsUsable()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        return base.SkillIsUsable() && playerCount == 1;
    }
}
