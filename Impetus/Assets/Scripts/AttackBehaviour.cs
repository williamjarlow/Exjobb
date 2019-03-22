using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] List<Sprite> spriteList;
    [SerializeField] [ClassExtends(typeof(Skill))] ClassTypeReference skill;
    SpriteRenderer spriteRenderer;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextSprite()
    {
        spriteRenderer.sprite = spriteList[index++];
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void EndAttack()
    {
        GetComponentInParent<Animator>().SetTrigger("Idle");
        Skill _skill = (Skill)FindObjectOfType(skill.Type);
        _skill.skillActive = false;
    }

}
