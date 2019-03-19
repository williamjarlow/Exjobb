using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public void SetTrigger(string name)
    {
        GetComponent<Animator>().SetTrigger(name);
    }
}
