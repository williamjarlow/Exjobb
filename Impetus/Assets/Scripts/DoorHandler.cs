using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open()
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
    }

    public void Close()
    {
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

}
