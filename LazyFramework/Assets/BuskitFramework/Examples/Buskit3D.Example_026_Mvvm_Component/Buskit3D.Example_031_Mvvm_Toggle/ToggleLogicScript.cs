/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ToggleLogicScript
* 创建日期：2019-03-15 11:34:50
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_29_Mvvm_Toggle
{   
	/// <summary>
    /// 
    /// </summary>
	public class ToggleLogicScript : LogicBehaviour 
	{
        public override void ProcessLogic(PropertyMessage evt)
        {
            var _entity = (ToggleEntityScript)GetComponent<ToggleModelBehaviour>().DataEntity;
            if (evt.PropertyName.Equals("isZeroON") && (bool)evt.NewValue)
            {
                _entity.content = "0";
            }
            if (evt.PropertyName.Equals("isOneON") && (bool)evt.NewValue)
            {
                _entity.content = "1";
            }
            if (evt.PropertyName.Equals("isTwoON") && (bool)evt.NewValue)
            {
                _entity.content = "2";
            }
        }
    }
}

