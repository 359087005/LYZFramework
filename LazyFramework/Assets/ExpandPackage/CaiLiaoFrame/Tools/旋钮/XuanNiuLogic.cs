/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： XuanNiuLogic
* 创建日期：2019-05-08 11:58:06
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;


public class XuanNiuLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("rot"))
        {
            GetComponent<XuanNiu>().target.transform.localEulerAngles = ((Vector3)evt.NewValue);
        }
        if (evt.PropertyName.Equals("isRot"))
        {
            GetComponent<XuanNiu>().isRot = ((bool)evt.NewValue);
        }
        if (evt.PropertyName.Equals("hasrotAngles"))
        {
            GetComponent<XuanNiu>().hasrotAngles = ((float)evt.NewValue);
        }
    }
}


