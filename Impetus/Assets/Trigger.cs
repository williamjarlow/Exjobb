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
    bool flag, triggered;
    List<Collider2D> actors = new List<Collider2D>();
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
        if(actors.Count > 0)
        {
            bool exists = true;
            foreach(Collider2D actor in actors)
            {
                if (actor == null)
                    exists = false;
            }
            if (!exists)
                Deactivate();
        }

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == agentTag)
        {
            actors.Add(collision);
            Activate();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == agentTag)
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == agentTag && !permanent && flag) {
            Deactivate();
        }
    }

    void Activate()
    {
        flag = false;
        spriteRenderer.sprite = activated;

        foreach (DoorHandler door in doors)
        {
            door.Open();
        }
    }

    void Deactivate()
    {
        spriteRenderer.sprite = deactivated;
        foreach (DoorHandler door in doors)
        {
            door.Close();
        }
    }
}
