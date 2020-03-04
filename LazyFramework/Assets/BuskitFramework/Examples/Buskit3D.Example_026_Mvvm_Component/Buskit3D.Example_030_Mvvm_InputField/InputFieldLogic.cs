/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： InputFieldLogic
* 创建日期：2019-03-18 10:59:05
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_30_Mvvm_InputField
{
    /// <summary>
    /// 
    /// </summary>
    public class InputFieldLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("inputContent")) {
                Debug.Log(evt.NewValue);
            }
        }
    }
}

