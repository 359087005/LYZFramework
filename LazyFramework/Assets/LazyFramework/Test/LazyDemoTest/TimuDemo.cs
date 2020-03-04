using System.Collections;
using System.Collections.Generic;
using Lazy;
using UnityEngine;

public class TimuDemo : MonoBehaviour
{
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //显示1-3题
            ExamPanelManager.Instance.ShowExam(1, 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            //显示3-6题
            ExamPanelManager.Instance.ShowExam(3, 6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //显示第一组所有题目
            ExamPanelManager.Instance.ShowExamByGroup(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //显示第二组所有题目
            ExamPanelManager.Instance.ShowExamByGroup(2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //清空面板
            ExamPanelManager.Instance.ClearAll();
        }
    }
}
