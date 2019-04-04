using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();
        SetPosition();
    }

    void SetPosition()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(Vector3(1, 0, dist)).x;

        transform.position.x = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
    }

    void SetRotation()
    {
        Vector3 dir = player.transform.position - transform.parent.position;
        dir = player.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
