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
        return Input.GetButtonDown("Clone");
    }
    public override bool SkillIsUsable()
    {
        int cloneCount = GameObject.FindGameObjectsWithTag("Clone").Length;
        return base.SkillIsUsable() && cloneCount == 0 && !FindObjectOfType<GlossaryController>().glossaryOpen;
    }
}
