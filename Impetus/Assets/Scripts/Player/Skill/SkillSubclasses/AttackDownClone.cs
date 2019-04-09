using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDownClone : Skill
{
    [SerializeField]
    float force;
    Rigidbody2D RB2D;
    bool chargeAvailable = true;
    new CloneMovement movement;
    void Start()
    {
        RB2D = player.GetComponent<Rigidbody2D>();
        movement = player.GetComponentInChildren<CloneMovement>();
    }
    private void Update()
    {
        if (movement.onGround)
            chargeAvailable = true;
    }
    public override void OnSkillStart()
    {
        chargeAvailable = false;
        RB2D.velocity = new Vector2(RB2D.velocity.x, force);
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
        return (base.SkillIsUsable() && !movement.onGround && chargeAvailable);
    }
}
