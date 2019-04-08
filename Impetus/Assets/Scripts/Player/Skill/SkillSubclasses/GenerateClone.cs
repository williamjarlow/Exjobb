using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateClone : Skill
{
    public override void OnSkillStart()
    {
        GameObject clone = GameObject.FindWithTag("Clone");
        if (clone != null)
            clone.GetComponent<SpriteRenderer>().flipX = player.GetComponent<SpriteRenderer>().flipX;

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
        bool glossaryOpen = false;
        if (FindObjectOfType<GlossaryController>() != null && FindObjectOfType<GlossaryController>().glossaryOpen)
            glossaryOpen = true;
        return base.SkillIsUsable() && !glossaryOpen;
    }
}
