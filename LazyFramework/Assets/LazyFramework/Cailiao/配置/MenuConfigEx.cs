using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lazy;

public class MenuConfigEx : MonoBehaviour
{
    void Start()
    {
        EventManager.AddEvent(EventTopic.FUNC_MENU, (a,b) =>
         {
             if(b[0] is string)
             {
                 if((string)b[0]== "OnFunctionStart")
                 {
                     switch((string)b[1])
                     {
                         case "装配":
                             FirstViewControl.instance.SetIsCanRotate(false);
                             break;
                         case "移动":
                             OnFuncStart();
                             break;
                         case "X轴旋转":
                             OnFuncStart();
                             break;
                         case "Y轴旋转":
                             OnFuncStart();
                             break;
                         case "缩放":
                             OnFuncStart();
                             break;
                     }
                 }
                 if ((string)b[0] == "OnFunctionEnd")
                 {
                     switch ((string)b[1])
                     {
                         case "装配":
                             FirstViewControl.instance.SetIsCanRotate(true);
                             break;
                     }
                     OnFuncEnd();
                 }
             }
         });
    }
    private void OnFuncEnd()
    {
        FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
    }
    private void OnFuncStart()
    {
        FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
    }
}
