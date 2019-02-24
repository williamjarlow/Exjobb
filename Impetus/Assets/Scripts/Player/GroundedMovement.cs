using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedMovement : Movement {
    Movement movement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (onGround)
            jumpCharges = maxJumpCharges;
    }
}
