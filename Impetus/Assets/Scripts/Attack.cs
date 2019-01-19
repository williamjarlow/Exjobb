using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour{
    public float attackDamage;
    public float attackMercyPeriod;
    public Vector3 targetLocation;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public virtual void AttackMovement()
    {
        targetLocation = player.transform.position;
    }

    class HomingAttackBehaviour : Attack
    {
        public override void AttackMovement()
        {

        }
    }

    class BouncingAttackBehaviour : Attack
    {

    }
}
