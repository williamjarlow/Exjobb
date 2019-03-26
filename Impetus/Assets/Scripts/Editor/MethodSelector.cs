using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MethodSelector : Editor
{
   /* public override void OnInspectorGUI()
    { 
        base.OnInspectorGUI();
        List<MethodInfo> methods = new List<MethodInfo>();
        System.Type[] mbs = ((GameObject)target).GetComponents<MonoBehaviour>();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Default; // BindingFlags is located in System.Reflection - modify these to your liking to get the methods you're interested in
        foreach (MonoBehaviour mb in mbs)
        {
            methods.AddRange((System.Type)mb..(flags));
        }

        foreach (var m in methods)
            Debug.Log(m.Name);
    }*/
}
