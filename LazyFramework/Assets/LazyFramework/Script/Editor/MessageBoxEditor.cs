using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MessageBoxEditor : Editor
{
    [CustomEditor(typeof(TransformMove))]

    private SerializedProperty cameraTransInfos;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        DrawDefaultInspector();
        MessageBox messageBox = FindObjectOfType<MessageBox>();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("寻找弹框"))
        {

        }
      

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
