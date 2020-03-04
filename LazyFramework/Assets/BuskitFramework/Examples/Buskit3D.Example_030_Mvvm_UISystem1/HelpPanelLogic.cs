/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： HelpPanelLogic
* 创建日期：2019-03-15 08:57:59
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
    /// <summary>
    /// 帮助面板业务逻辑类
    /// </summary>
    public class HelpPanelLogic : LogicBehaviour
    {
        private HelpPanellEntity entity;
        private void Start()
        {
            entity = (HelpPanellEntity)GameObject.FindObjectOfType<HelpPanelDataModel>().DataEntity;
        }
        public override void ProcessLogic(PropertyMessage evt)
        {
            if(evt.PropertyName.Equals("dropdownValue"))
            {
               if(evt.NewValue.Equals(0))
                {
                    entity.showText = "欢迎来到单机副本";
                }
                if (evt.NewValue.Equals(1))
                {
                    entity.showText = "欢迎来到野外挂机";
                }
                if (evt.NewValue.Equals(2))
                {
                    entity.showText = "欢迎来到一剑妖仙塔";
                }
            }
        }
    }
}

