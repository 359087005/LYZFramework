/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AttributeMenuLogic
* 创建日期：2019-04-12 16:23:56
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeMenuLogic : LogicBehaviour
    {
        AttributeMenuEntity attributeMenuEntity;

        private void Start()
        {
            attributeMenuEntity = (AttributeMenuEntity)GetComponent<AttributeMenuModel>().DataEntity;
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {
                case "menu_Tower":
                    if (evt.OldValue == evt.NewValue) {
                        return;
                    }
                    OnclickMenu_Tower();
                    break;
                case "menu_Phone":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    OnclickMenu_Phone();
                    break;
                case "menu_Link":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    OnclickMenu_Link();
                    break;
                case "menu_Statistics":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    OnclickMenu_Statistics();
                    break;
                case "menu_Back":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    OnclickMenu_Back();
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    OnclickMenu_UIActive(isShow);
                    break;
            }
        }

        /// <summary>
        /// 打开基站UI
        /// </summary>
        public void OnclickMenu_Tower() {
            var entity = (AttributeToweEntity)FindObjectOfType<AttributeTowerModel>().DataEntity;
            entity.isShow = true;
            attributeMenuEntity.isShow = false;
        }

        /// <summary>
        /// 打开手机UI
        /// </summary>
        public void OnclickMenu_Phone()
        {
            attributeMenuEntity.isShow = false;
            var entity = (AttributePhoneEntity)FindObjectOfType<AttributePhoneModel>().DataEntity;
            entity.isShow = true;
        }

        /// <summary>
        /// 打开链路
        /// </summary>
        public void OnclickMenu_Link()
        {
            attributeMenuEntity.isShow = false;
            var entity = (AttributeListEntity)FindObjectOfType<AttributeListModel>().DataEntity;
            entity.isShow = true;

        }

        /// <summary>
        /// 打开统计UI
        /// </summary>
        public void OnclickMenu_Statistics()
        {
            attributeMenuEntity.isShow = false;
            var entity = (AttributeStatisticsEntity)FindObjectOfType<AttributeStatisticsModel>().DataEntity;
            entity.isShow = true;
        }

        /// <summary>
        /// 返回住UI
        /// </summary>
        public void OnclickMenu_Back()
        {
            var menuEntity = (MenuEntity)FindObjectOfType<MenuViewModel>().DataEntity;
            menuEntity.isShow = true;
            attributeMenuEntity.isShow = false;
        }

        /// <summary>
        /// 控制激活状态
        /// </summary>
        public void OnclickMenu_UIActive(bool isTrue)
        {
            if (isTrue)
            {
                transform.localScale = Vector3.one;
            }
            else {
                transform.localScale = Vector3.zero;
            }
        }

    }
}

