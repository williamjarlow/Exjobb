using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public class DummyBehaviour : MonoBehaviour
{
    GameObject player;
    LineRenderer lineRenderer;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer.SetPosition(0, gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, gameObject.transform.position + (Vector3.Normalize(gameObject.transform.position - player.transform.position) * (spriteRenderer.sprite.rect.width / 28)));
        lineRenderer.SetPosition(1, player.transform.position);
        if (spriteRenderer.isVisible)
            lineRenderer.SetPosition(1, lineRenderer.GetPosition(0));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackHitbox"))
        {
            ClassTypeReference skillUsed = other.GetComponent<AttackBehaviour>().skill;
            GetComponentInParent<ParticleManager>().EmitParticles(skillUsed);
            GameManager.instance.OnDummyDestroy();
            Destroy(gameObject);
        }
    }
}
