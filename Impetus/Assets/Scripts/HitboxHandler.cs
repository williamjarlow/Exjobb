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
    public void DestroyHitboxes()
    {
        foreach (GameObject go in hitboxes)
        {
            hitboxes.Remove(go);
            Destroy(go);
        }
    }
}
