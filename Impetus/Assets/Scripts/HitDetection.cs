using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour {
    [SerializeField]
    HealthHandler healthHandler;
    bool invincible;
    float timeSinceHit = 0;
    Animator thisAnimator;
    Attack previousAttack; //The last attack that hit you.

	void Start () {
        thisAnimator = GetComponent<Animator>();
	}
	
	void Update () {
        if (invincible)
        {
            timeSinceHit += Time.deltaTime;
            //thisAnimator.SetBool("IsInvincible", true);
            if (timeSinceHit >= previousAttack.attackMercyPeriod)
            {
              //thisAnimator.SetBool("IsInvincible", false);
                invincible = false;
                timeSinceHit = 0;
            }
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Attack") && invincible == false)
        {
            previousAttack = other.gameObject.GetComponent<Attack>();
            healthHandler.ChangeHealthValue(previousAttack.attackDamage);
            invincible = true;
        }
    }
}
