﻿/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ShowValueLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_28_Mvvm_DropDown
{
    public class MvvmDropDownLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("dropdowmValue"))
            {
                var _entity = ((MvvmDropDownEntity)GetComponent<MvvmDropDownModel>().DataEntity);
                _entity.tabName = evt.NewValue.ToString();
            }

            if (evt.PropertyName.Equals("tabName"))
            {
                if(evt.NewValue == evt.OldValue)
                {
                    return;
                }

                var _entity = ((MvvmDropDownEntity)GetComponent<MvvmDropDownModel>().DataEntity);
                _entity.tabName = evt.NewValue.ToString();
            }
        }
    }
}