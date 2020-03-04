/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 当需要应用自动寻路时的四视角控制具体调用方法
************************************************************/
using UnityEngine;
using System.Collections;

public class AIViewBtn : MonoBehaviour
{

    public Transform target0;
    public Transform taget0Player, taget0Cam;
    public Transform target1;
    public Transform taget1Player, taget1Cam;


    public void ViewBtn0Clicked(GameObject go)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<FirstAndThirdViewSwitch>())
            if (FirstAndThirdViewSwitch.instance.curState == PlayerState.ThirdView)
                return;
      
            AIViewControl.instance.MoveToDestination(AIViewControl.Room.bRoom, target0.position, taget0Player.position, taget0Player.eulerAngles, taget0Cam.localPosition, taget0Cam.localEulerAngles, 2, 60,() =>
           Debug.Log("View0Btn Clicked"));
     
    }
    public void ViewBtn1Clicked(GameObject go)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<FirstAndThirdViewSwitch>())
            if (FirstAndThirdViewSwitch.instance.curState == PlayerState.ThirdView)
                return;

        AIViewControl.instance.MoveToDestination(AIViewControl.Room.cRoom, target1.position, taget1Player.position, taget1Player.eulerAngles, taget1Cam.localPosition, taget1Cam.localEulerAngles, 2,60, () =>
                    Debug.Log("View1Btn Clicked"));
      
       
    }

}
