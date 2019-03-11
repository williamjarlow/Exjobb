using UnityEngine;
using System.Collections;

public enum AttackPhase
{
    INITIALIZE = -1,
    STARTUP = 0,
    ACTIVE = 1,
    ENDLAG = 2,
    FINALIZE = 3
}

[System.Serializable]
public struct ActivePhase
{
    public float damage;
    public GameObject[] hitboxes;
}

public class HitboxHandling : MonoBehaviour{
    [SerializeField]
    bool destroyOnHit;
    [SerializeField]
    ActivePhase[] activePhases;
    int damageWindowIndex = 0;
    AttackPhase attackPhase = AttackPhase.INITIALIZE;
    private void Start()
    {
        AdvancePhase(); //Start at startup
    }

    private void Update()
    {
        if (!GetComponent<Animation>().isPlaying)
            Destroy(gameObject);
    }

    void AdvancePhase()
    {
        attackPhase++;
        switch (attackPhase)
        {
            case AttackPhase.ACTIVE:
                foreach(GameObject hitbox in activePhases[damageWindowIndex].hitboxes)
                    Instantiate(hitbox, transform);
                break;
            case AttackPhase.ENDLAG:
                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    if(child.gameObject != gameObject)
                        Destroy(child.gameObject);
                }
                break;
            case AttackPhase.FINALIZE:
                Destroy(gameObject);
                break;
            default:
                break;
        }
            
    }
    void AdvanceActive()
    {
        damageWindowIndex++;
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.gameObject != gameObject)
                Destroy(child.gameObject);
        }
        foreach (GameObject hitbox in activePhases[damageWindowIndex].hitboxes)
            Instantiate(hitbox, transform);
    }
}
