/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： RotateObjectLogic
* 创建日期：2019-05-08 09:54:59
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

  
	/// <summary>
    /// 
    /// </summary>
	public class RotateObjectLogic : LogicBehaviour 
	{
	   public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("pos"))
            {
                GetComponent<RotateObject>().model.transform.localPosition = ((Vector3)evt.NewValue);
            }
            if (evt.PropertyName.Equals("rot"))
            {
                GetComponent<RotateObject>().model.transform.localEulerAngles = ((Vector3)evt.NewValue);
            }
            if (evt.PropertyName.Equals("distance"))
            {
                GetComponent<RotateObject>().distance = ((float)evt.NewValue);
            }
	   }
	}


