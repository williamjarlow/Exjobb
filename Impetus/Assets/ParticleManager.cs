using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public enum Direction
{
    LEFT = 0,
    UP = 90,
    RIGHT = 180,
    DOWN = 270
}

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmitParticles(ClassTypeReference skillUsed)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particleSystem.shape;
        ParticleSystem.VelocityOverLifetimeModule velocity = particleSystem.velocityOverLifetime;
        Vector3 curRot = shape.rotation;

        if (velocity.enabled == false)
            velocity.enabled = true;

        if (skillUsed.Type == typeof(AttackLeft))
        {
            shape.rotation = new Vector3((int)Direction.LEFT, curRot.y, curRot.z);
        }
            
        else if (skillUsed.Type == typeof(AttackRight))
        {
            shape.rotation = new Vector3((int)Direction.RIGHT, curRot.y, curRot.z);
        }
            
        else if (skillUsed.Type == typeof(AttackUp))
        {
            shape.rotation = new Vector3((int)Direction.UP, curRot.y, curRot.z);
        }
            
        else if (skillUsed.Type == typeof(AttackDown))
        {
            shape.rotation = new Vector3((int)Direction.DOWN, curRot.y, curRot.z);
            velocity.enabled = false;
        }
            

        particleSystem.Play();
    }
}
