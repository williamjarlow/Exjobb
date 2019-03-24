using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TypeReferences;

[CustomEditor(typeof(Skill), true)]
[CanEditMultipleObjects]
public class ParameterSelector : Editor
{
    Skill skill;
    int selectedIndex = 0;
    Animator animator;
    List<Parameter> parameters = new List<Parameter>();
    List<AnimatorControllerParameter> animParams = new List<AnimatorControllerParameter>();

    public void OnEnable()
    {
        skill = (Skill)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        skill.Initialize();
        animator = skill.playerAnim;
        animator.Update(0);
        animParams.Clear();
        animParams.AddRange(animator.parameters);

        RemoveOldParameters();
        AddNewParameters();

        GenerateDropdown();
        EditorUtility.SetDirty(skill);
    }

    void RemoveOldParameters()
    {
        List<Parameter> deleteList = new List<Parameter>();
        foreach (Parameter param in parameters)
        {
            bool exists = false;
            foreach (AnimatorControllerParameter animParam in animParams)
            {
                if (animParam.name == param.name)
                    exists = true;
            }
            if (!exists)
                deleteList.Add(param);
        }
        foreach (Parameter param in deleteList)
            parameters.Remove(param);
    }

    void AddNewParameters()
    {
        foreach (AnimatorControllerParameter animParam in animParams)
        {
            bool exists = false;
            foreach (Parameter param in parameters)
                if (param.name == animParam.name)
                {
                    exists = true;
                    break;
                }
            if (exists)
                continue;
            if (animParam.type == AnimatorControllerParameterType.Trigger)
                parameters.Add(new Parameter(animParam.name));
        }
    }

    void GenerateDropdown()
    {
        string[] options = new string[parameters.Count];
        bool exists = false;
        for (int i = 0; i < parameters.Count; i++)
        {
            options[i] = parameters[i].name;
            if (skill.paramName == options[i])
            {
                selectedIndex = i;
                exists = true;
            }
        }
        if (!exists)
            selectedIndex = 0;

        selectedIndex = EditorGUILayout.Popup("Parameter", selectedIndex, options);
        skill.paramName = options[selectedIndex];
    }

}