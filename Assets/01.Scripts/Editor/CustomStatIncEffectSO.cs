using System;
using SkillSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatIncUpEffectSO))]
public class CustomStatIncEffectSO : CustomPowerEffectSO
{
    private SerializedProperty targetStatProp;
    private SerializedProperty increaseValueProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        targetStatProp = serializedObject.FindProperty("targetStat");
        increaseValueProp = serializedObject.FindProperty("increaseValue");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        try
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(targetStatProp);
            EditorGUILayout.PropertyField(increaseValueProp);

            serializedObject.ApplyModifiedProperties();
        }
        catch (Exception e)
        {
            Debug.Log($"exception occur when draw{e.Message}");
        }
    }
}
