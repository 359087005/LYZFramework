
/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: TimeControl.cs
  Author:汪海波       Version :1.0          Date: 2017-3-1
  Description:时间控制脚本
************************************************************/

using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour
{
    void Start()
    {
       
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 4;
        }
        if (Input.GetKey(KeyCode.Alpha5) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 5;
        }
        if (Input.GetKey(KeyCode.Alpha6) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 6;
        }
        if (Input.GetKey(KeyCode.Alpha7) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 7;
        }
        if (Input.GetKey(KeyCode.Alpha8) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 8;
        }
        if (Input.GetKey(KeyCode.Alpha9) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            Time.timeScale = 9;
        }
    }
}
