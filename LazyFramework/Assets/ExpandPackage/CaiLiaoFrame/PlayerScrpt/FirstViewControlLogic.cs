using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

public class FirstViewControlLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("playerPos"))
        {
            FirstViewControl.instance.characterTrans.localPosition = (Vector3)evt.NewValue;
        }
        if (evt.PropertyName.Equals("playerRot"))
        {
            FirstViewControl.instance.characterTrans.localEulerAngles = (Vector3)evt.NewValue;
        }
        if (evt.PropertyName.Equals("cameraPos"))
        {
            FirstViewControl.instance.mainCamTrans.localPosition = (Vector3)evt.NewValue;
        }
        if (evt.PropertyName.Equals("cameraRot"))
        {
            FirstViewControl.instance.mainCamTrans.localEulerAngles = (Vector3)evt.NewValue;
        }

        if (evt.PropertyName.Equals("isCanRotate"))
        {
            FirstViewControl.instance.SetIsCanRotate((bool)evt.NewValue, false);
        }
        if (evt.PropertyName.Equals("isCanScrollView"))
        {
            FirstViewControl.instance.SetIsCanScrollView((bool)evt.NewValue,false);
        }

        if (evt.PropertyName.Equals("iniQuaternionInt"))
        {
            FirstViewControl.instance.IniQuaternion(false);
        }

        if (evt.PropertyName.Equals("fov"))
        {
            FirstViewControl.instance.mainCamTrans.GetComponent<Camera>().fieldOfView = (float)evt.NewValue;
        }
        if (evt.PropertyName.Equals("playerState"))
        {
            FirstViewControl.instance.SetPlayerState((PlayerControlState)evt.NewValue, false);
        }
    }
}
