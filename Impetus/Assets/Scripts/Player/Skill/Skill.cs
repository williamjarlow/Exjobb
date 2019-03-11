using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Tooltip("Duration of the skill in frames.")]
    public int duration;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public bool skillActive;
    int frames;
    Skill[] skills;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        skills = gameObject.GetComponents<Skill>();
    }

    public void PseudoUpdate()
    {
        if (SkillIsUsable() && HandleInput() && !skillActive)
        {
            StartSkill();
            skillActive = true;
        }
        if (skillActive) {
            if (frames < duration)
            {
                UpdateSkill();
                frames++;
            }
            else
            {
                frames = 0;
                StopSkill();
            }
        }
    }
    /// <summary>
    /// Examines whether or not the player is in a state where they can use the skill. Needs to call "SkillIsUsable().base" instead of return true.
    /// </summary
    public virtual bool SkillIsUsable()
    {
        foreach (Skill skill in skills)
            if (skill.skillActive)
                return false;
        return true;
    }
    /// <summary>
    /// Examines whether or not the player has performed the neccessary inputs to use the skill.
    /// </summary>
    public abstract bool HandleInput();
    /// <summary>
    /// Behaviour for starting the skill. Gets called when player is allowed to use the skill and has performed the neccessary inputs.
    /// </summary>
    public abstract void StartSkill();
    /// <summary>
    /// Behaviour for updating the skill. Gets called when skill is active.
    /// </summary>
    public abstract void UpdateSkill();
    /// <summary>
    /// Behaviour for stopping the skill. Gets called when the duration of the skill has run out.
    /// </summary>
    public abstract void StopSkill();
}
