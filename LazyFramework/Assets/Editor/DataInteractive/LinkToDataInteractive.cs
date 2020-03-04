using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ExpInteractive))]
public class LinkToDataInteractive:Editor
{
   
    private SerializedObject dataInteractive;
    private SerializedProperty platformType, Host, id, token, sequenceCode, hostUrl;//定义类型  
    void OnEnable()
    {

        dataInteractive = new SerializedObject(target);
        platformType = dataInteractive.FindProperty("platformType");        
#if UNITY_STANDALONE_WIN       
        Host = dataInteractive.FindProperty("Host");
        id = dataInteractive.FindProperty("id");
        token = dataInteractive.FindProperty("token");
        sequenceCode = dataInteractive.FindProperty("sequenceCode");
        hostUrl = dataInteractive.FindProperty("hostUrl");
#else
        hostUrl = dataInteractive.FindProperty("hostUrl");
#endif
    }
      public override void OnInspectorGUI()
    {
        dataInteractive.Update();//更新test
        EditorGUILayout.PropertyField(platformType);  
        if (platformType.enumValueIndex == 0)//购买平台
        {
             EditorGUILayout.PropertyField(hostUrl);
#if UNITY_STANDALONE_WIN
           
            EditorGUILayout.PropertyField(token);
#endif
        }
        else if (platformType.enumValueIndex == 1)//简易平台
        {
#if UNITY_STANDALONE_WIN
            EditorGUILayout.PropertyField(Host);
            EditorGUILayout.PropertyField(id);
            EditorGUILayout.PropertyField(sequenceCode);
#endif 
        }  
        dataInteractive.ApplyModifiedProperties();//应用
    }

}
