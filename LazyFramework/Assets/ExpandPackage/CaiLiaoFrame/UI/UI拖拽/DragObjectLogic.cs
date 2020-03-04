/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： DragObjectLogic
* 创建日期：2019-05-08 13:48:07
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

public class DragObjectLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("pos"))
        {
            transform.localPosition = ((Vector3)evt.NewValue);
        }
        if (evt.PropertyName.Equals("scale"))
        {
           GetComponent<DragObject>().scale = (float)evt.NewValue;
           transform.localScale = (float)evt.NewValue * Vector3.one;
        }
       
    }
}


