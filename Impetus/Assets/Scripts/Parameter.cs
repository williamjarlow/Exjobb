using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    [ReadOnly]
    public string name;
    public bool boolValue;
    public float numberValue;
    public Parameter(string name)
    {
        this.name = name;
    }
}

