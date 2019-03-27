using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionLayer : MonoBehaviour
{
    [SerializeField]
    LayerMask layer1, layer2;
    // Start is called before the first frame update
    void Start()
    {
        int temp1 = gameObject.layer;
        int temp2 = gameObject.layer;
        //Debug.Log("layer1: " + layer1.value + ", temp1: " + temp1);
        //Debug.Log("layer2: " + layer2.value + ", temp2: " + temp2);
        Physics2D.IgnoreLayerCollision(temp1, temp2, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
