using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamPanelBase : MonoBehaviour
{
    [Header("*空物体(用于题目之间空格)")]
    [SerializeField]public GameObject emptyObject;
  
    public ExamPanelInfo examPanelInfo;
   
}

[System.Serializable]
public class ExamPanelInfo
{
    public int id;
    public GameObject examTemp;
    public Text topic;
    public Text analysis;
   
    public List<ExamOptionInfo> examOptionInfo = new List<ExamOptionInfo>();
    public ExamPanelInfo(GameObject _examTemp,int _id)
    {
        id = _id;
        examTemp = _examTemp;
    }
}
[System.Serializable]
public class ExamOptionInfo
{
    [Header("选项Toggle")]
    public Toggle toggle;
    public string tag;
    public bool isTrue;
    public ExamOptionInfo(Toggle _toggle)
    {
        toggle = _toggle;
        toggle.onValueChanged.AddListener((a) =>
        {
            isTrue = a;
        });
    }
    public ExamOptionInfo(Toggle _toggle, string _tag)
    {
        toggle = _toggle;
        tag = _tag;
        toggle.onValueChanged.AddListener((a) =>
        {
            isTrue = a;
        });
    }
}
