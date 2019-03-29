using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionLayer : MonoBehaviour
{
    [SerializeField]
    LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        int temp1 = gameObject.layer;
        int temp2 = layer.value;
        while (temp2 >= 2)
        {
            i++;
            temp2 /= 2;
        }
        Debug.Log("temp1: " + temp1);
        Debug.Log("temp2: " + i);
        Physics2D.IgnoreLayerCollision(temp1, i, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
