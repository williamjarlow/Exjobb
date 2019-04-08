using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public class DummyBehaviour : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackHitbox") && !destroyed)
        {
            destroyed = true;
            ClassTypeReference skillUsed = other.GetComponent<AttackBehaviour>().skill;
            GetComponentInParent<ParticleManager>().EmitParticles(skillUsed);
            GameManager.instance.OnDummyDestroy();
            Destroy(gameObject);
        }
    }
}
