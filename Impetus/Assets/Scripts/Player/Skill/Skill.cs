using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [HideInInspector]
    public string paramName;
    [SerializeField]
    bool overrideMovement, overridable;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Animator playerAnim;
    [HideInInspector]
    public Movement movement;
    [HideInInspector]
    public bool skillActive;
    bool skillStarted;
    Skill[] skills;
    
    private void Awake()
    {
        Initialize();   
    }

    public void Initialize()
    {
        player = GetComponentInParent<Rigidbody2D>().gameObject;
        playerAnim = player.GetComponent<Animator>();
        movement = player.GetComponentInChildren<Movement>();
        skills = gameObject.GetComponents<Skill>();
    }

    public void PseudoUpdate()
    {
        if (SkillInputPerformed() && SkillIsUsable())
            _Start();
        else if (skillActive)
            _Update();
        else if (skillStarted)
            _Stop();
    }

    void _Start()
    {
        OnSkillStart();
        skillActive = true;
        skillStarted = true;
        if(movement != null)
            movement.enabled = !overrideMovement;
        playerAnim.SetTrigger(paramName);
    }

    void _Update()
    {
        OnSkillUpdate();
    }

    void _Stop()
    {
        skillStarted = false;
        OnSkillStop();
        if(overrideMovement)
            movement.enabled = true;
    }

    /// <summary>
    /// Examines whether or not the player is in a state where they can use the skill. Return "SkillIsUsable().base" instead of true if the skill shouldn't animation-cancel.
    /// </summary
    public virtual bool SkillIsUsable()
    {
        foreach (Skill skill in skills)
            if (skill.skillActive && !skill.overridable)
                return false;
        return true;
    }
    /// <summary>
    /// Examines whether or not the player has performed the neccessary inputs to use the skill.
    /// </summary>
    public abstract bool SkillInputPerformed();
    /// <summary>
    /// Behaviour for starting the skill. Gets called when player is allowed to use the skill and has performed the neccessary inputs.
    /// </summary>
    public abstract void OnSkillStart();
    /// <summary>
    /// Behaviour for updating the skill. Gets called when skill is active.
    /// </summary>
    public abstract void OnSkillUpdate();
    /// <summary>
    /// Behaviour for stopping the skill. Gets called when the duration of the skill has run out.
    /// </summary>
    public abstract void OnSkillStop();
}
