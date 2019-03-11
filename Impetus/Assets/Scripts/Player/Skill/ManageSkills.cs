using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSkills : MonoBehaviour
{
    [SerializeField]
    [TextArea(5,5)]
    string explanation = "The skill order determines in which order the skill inputs will be checked. For example, a neutral attack that can be performed at any time should be low on the list, while a directional aerial attack should be higher.";
    [SerializeField]
    Skill[] skillOrder;
    // Update is called once per frame
    void Update()
    {
        foreach (Skill skill in skillOrder)
            skill.PseudoUpdate();
    }
}
