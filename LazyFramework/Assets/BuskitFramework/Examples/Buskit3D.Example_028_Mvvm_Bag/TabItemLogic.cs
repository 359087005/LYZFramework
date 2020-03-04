/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TabItemLogic
* 创建日期：2019-03-21 09:50:50
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_36_Mvvm_Bag
{   
	/// <summary>
    /// 
    /// </summary>
	public class TabItemLogic : LogicBehaviour 
	{
		public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("isOn")) {
                if ((bool)evt.NewValue) {
                    int index = transform.GetSiblingIndex();
                    var dataEntity = FindObjectOfType<BagViewModel>().DataEntity;
                    var _entity =(BagEntity)dataEntity;
                    _entity.tabID = index;
                }
            }
        }		
	}
}

