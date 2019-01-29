using UnityEngine;
using System.Collections;
using AnimatorStateMachineUtil;

/*public enum ParamType
{
    TRIGGER,
    BOOL,
    INT,
    FLOAT,
    Count
}

[System.Serializable]
public class Parameter
{
    public ParamType paramType;
    public string name;
}
*/
public class Movement : MonoBehaviour
{
    [SerializeField]
    string movingName, jumpName, jumpChargesName, onGroundName, maxJumpChargesName;
    int maxJumpCharges, jumpCharges;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        maxJumpCharges = animator.GetInteger(maxJumpChargesName);
    }

    void Update()
    {
        animator.SetBool(movingName, Input.GetAxis("Horizontal") != 0);

        if (animator.GetBool(onGroundName))
            animator.SetInteger(jumpChargesName, maxJumpCharges);

        if (Input.GetButtonDown("Jump") && animator.GetInteger(jumpChargesName) > 0)
        {
            animator.SetTrigger(jumpName);
            animator.SetBool(onGroundName, false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<Animator>().SetBool(onGroundName, true);
        GetComponent<Animator>().SetTrigger("Change");
    }

    void OnCollisionExit2D(Collision2D other)
    {
        GetComponent<Animator>().SetBool(onGroundName, false);
        GetComponent<Animator>().SetTrigger("Change");
    }
}
