using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    string agentTag;
    [SerializeField]
    bool permanent;
    [SerializeField]
    DoorHandler[] doors;
    bool flag;
    [SerializeField]
    Sprite activated, deactivated;


    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flag = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == agentTag) {
            flag = false;
            spriteRenderer.sprite = activated;

            foreach(DoorHandler door in doors)
            {
                door.Open();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == agentTag && !permanent && flag) {
            spriteRenderer.sprite = deactivated;
            foreach (DoorHandler door in doors)
            {
                door.Close();
            }
        }
    }
}
