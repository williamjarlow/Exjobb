using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    GameObject target;
    SpriteRenderer sprite;

    [SerializeField]
    float minSize, maxSize, maxDist;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();
        SetPosition();
    }

    void SetPosition()
    {
        float yExtent = 1.95f * Camera.main.orthographicSize; // Get vertical world-space extent.
        float xExtent = yExtent * Camera.main.aspect; // Get horizontal world-space extent.
        Vector3 center = Camera.main.transform.position; // Get world-space center.
        Bounds bounds = new Bounds(center, new Vector2(xExtent, yExtent));
           
        Vector2 targetPos = target.transform.position;
        Vector2 closestPoint = new Vector2();
        if (!bounds.Contains(targetPos))
        {
            closestPoint = bounds.ClosestPoint(targetPos);
            closestPoint -= (Vector2)Vector3.Normalize((Vector3)targetPos - (Vector3)closestPoint)*2;
        }

        float percent = 1 - Mathf.Clamp(Vector3.Distance(targetPos, closestPoint) / maxDist, 0, 1);
        float scale = (1 - percent) * minSize + (percent * maxSize);
        transform.localScale = new Vector3(scale, scale);

        transform.position = closestPoint;
        sprite.enabled = !target.GetComponent<SpriteRenderer>().isVisible;
    }

    void SetRotation()
    {
        Vector3 dir = player.transform.position - target.transform.position;
        dir = player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
