/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CubeLogic
* 创建日期：2019-03-12 16:03:25
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_000_PresureDemo
{   
	/// <summary>
    /// 
    /// </summary>
	public class CubeLogic : LogicBehaviour 
	{

        public override void ProcessLogic(PropertyMessage evt)
        {
            base.ProcessLogic(evt);
            if(evt.PropertyName.Equals("number"))
            {

                transform.localPosition += Vector3.one * (int)evt.NewValue;
            }
        }
    }
}

