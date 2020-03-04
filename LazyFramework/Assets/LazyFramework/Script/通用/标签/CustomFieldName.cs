using UnityEngine;
using System;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field)]
public class CustomLabelAttribute : PropertyAttribute
{
    public string name;
    public CustomLabelAttribute(string _name)
    {
        name = _name;
    }
}
[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
public class FieldLabelEditor : PropertyDrawer
{
    private CustomLabelAttribute cl
    {
        get { return (CustomLabelAttribute)attribute; }
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent(cl.name), true);
    }
}