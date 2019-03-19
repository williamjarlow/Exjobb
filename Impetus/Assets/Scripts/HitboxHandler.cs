using UnityEngine;
using System.Collections.Generic;

public class HitboxHandler : MonoBehaviour
{
    [SerializeField]
    bool destroyOnHit;
    List<GameObject> hitboxes = new List<GameObject>();

    public void GenerateHitbox(GameObject hitboxPrefab)
    {
        hitboxes.Add(Instantiate(hitboxPrefab, transform));
    }
    public void DestroyHitbox(GameObject hitboxInstance)
    {
        foreach (GameObject go in hitboxes)
            if (go == hitboxInstance)
            {
                hitboxes.Remove(hitboxInstance);
                Destroy(hitboxInstance);
            }
    }
}
