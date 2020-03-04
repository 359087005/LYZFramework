using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Lazy
{
    public class ExamImportTools : ExamImportBase
    {
        [SerializeField]private ReadMode readMode;
        public override void AddExam()
        {
            switch (readMode)
            {
                case ReadMode.json:
                    StartCoroutine(StreamingAssetPathConfigReader.TextReader(path+".json", (a) => 
                    {
                        ExamMgr.ExamDatas = new ExamReadByJson().LoadExam(a);
                    }));
                    break;
                case ReadMode.csv:
                    break;
                case ReadMode.txt:
                    break;
            }
        }
       
        public enum ReadMode
        {
            json,
            csv,
            txt
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(ExamImportTools))]
    public class ExamImportToolsEditor : Editor
    {
        private SerializedProperty cameraTransInfos;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            DrawDefaultInspector();

            ExamImportTools myScript = (ExamImportTools)target;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("手动加载题目到内存"))
            {
                myScript.AddExam();
            }
            if (GUILayout.Button("删除内存中所有题目"))
            {
                myScript.ClearExam();
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
#endif

}
