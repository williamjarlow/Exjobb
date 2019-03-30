using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    List<string> agentTags;
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
            bool exists = false;
            foreach(Collider2D actor in actors)
            {
                if (actor != null)
                    exists = true;
            }
            if (!exists)
            {
                Deactivate();
                actors.Clear();
            }  
        }
        else if(triggered)
        {
            Deactivate();
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (agentTags.Contains(collision.gameObject.tag))
        {
            if (!actors.Contains(collision))
                actors.Add(collision);
            Activate();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (agentTags.Contains(collision.gameObject.tag))
        {
            if (!actors.Contains(collision))
                actors.Add(collision);
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (agentTags.Contains(collision.gameObject.tag) && !permanent && flag) {
            actors.Remove(collision);
            Deactivate(collision);
        }
    }

    void Activate()
    {
        flag = false;
        spriteRenderer.sprite = activated;
        triggered = true;
        foreach (DoorHandler door in doors)
        {
            door.Open();
        }
    }

    void Deactivate(Collider2D col = null)
    {
        if (col != null)
            actors.Remove(col);
        spriteRenderer.sprite = deactivated;
        triggered = false;
        foreach (DoorHandler door in doors)
        {
            door.Close();
        }
    }
}
