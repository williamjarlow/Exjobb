using System.Collections;
using System.Collections.Generic;
using AnimatorStateMachineUtil;
using UnityEngine;

public class Uppercut : Skill
{
    Rigidbody2D RB2D;
    [SerializeField]
    float speed;
    bool skillUsable;
    private void Start()
    {
        RB2D = player.GetComponent<Rigidbody2D>();
    }

    #region Skill behaviour
    public override void OnSkillStart()
    {
        RB2D.AddForce(new Vector2(0, speed));
    }
    public override void OnSkillUpdate()
    {
        Debug.Log("ccc");
    }
    public override void OnSkillStop(){}
    #endregion
    #region Conditions
    public override bool SkillInputPerformed()
    {
        float inputDirVertical = Input.GetAxisRaw("Vertical");
        return (Input.GetButtonDown("Attack") && inputDirVertical > 0);
    }
    public override bool SkillIsUsable()
    {
        return skillUsable;
    }
    #endregion
    #region State machine logic
    [StateEnterMethod("Player.FThrust")]
    public void Enter() //Function must be public. For instruction on the statemachineutil, read the readme.
    {
        skillUsable = true;
    }
    [StateExitMethod("Player.FThrust")]
    public void Exit()
    {
        skillUsable = false;
    }
    #endregion
}
