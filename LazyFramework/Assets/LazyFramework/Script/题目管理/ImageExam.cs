using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ImageExam : ExamPanelBase
{
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            switch (gameObject.transform.GetChild(i).name)
            {
                case "题目":
                    if (gameObject.transform.GetChild(i).GetComponent<Text>() != null)
                    {
                        examPanelInfo.topic = gameObject.transform.GetChild(i).GetComponent<Text>();
                        examPanelInfo.topic.gameObject.SetActive(true);
                    }
                    break;
                case "选项":
                    for (int j = 0; j < gameObject.transform.GetChild(i).childCount; j++)
                    {
                        if (gameObject.transform.GetChild(i).GetChild(j).GetComponent<Toggle>() != null)
                        {
                            examPanelInfo.examOptionInfo.Add(new ExamOptionInfo(gameObject.transform.GetChild(i).GetChild(j).GetComponent<Toggle>(), new System.Text.ASCIIEncoding().GetString(new byte[] { (byte)(j + 65) })));
                        }
                    }
                    break;
                case "解析":
                    if (gameObject.transform.GetChild(i).GetComponent<Text>() != null)
                    {
                        examPanelInfo.analysis = gameObject.transform.GetChild(i).GetComponent<Text>();
                        examPanelInfo.analysis.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
[CustomEditor(typeof(ImageExam))]
public class ImageExamEditor : Editor
{
    public string examID;
   
    public override void OnInspectorGUI()
    {
        ImageExam myScript = (ImageExam)target;
        EditorGUILayout.BeginHorizontal();
        
        GUILayout.Label("请输入在Josn中对应的题号（阿拉伯数字）");
        GUI.backgroundColor = Color.green;
      
        examID = GUILayout.TextField(examID);
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();
        if (GUI.changed)
        {
            myScript.examPanelInfo.id = int.Parse(examID);
            EditorUtility.SetDirty(target);
        }
        DrawDefaultInspector();
    }
}