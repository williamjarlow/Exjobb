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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == agentTag) {
            foreach(DoorHandler door in doors)
            {
                door.Open();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == agentTag && !permanent)
        {
            foreach (DoorHandler door in doors)
            {
                door.Close();
            }
        }
    }
}
