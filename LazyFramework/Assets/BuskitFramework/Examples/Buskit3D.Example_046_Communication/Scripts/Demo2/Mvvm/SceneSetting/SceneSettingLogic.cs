/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SceneSettingLogic
* 创建日期：2019-04-11 10:11:08
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneSettingLogic : LogicBehaviour
    {
        private void Start()
        {
            InjectService.InjectInto(this);
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "creatPhone":
                    if (!evt.OldValue.Equals(evt.NewValue))
                    {
                        AddPhone();
                    }
                    break;
                case "creatTower":
                    if (!evt.OldValue.Equals(evt.NewValue))
                    {
                        AddTower();
                    }
                    break;
                case "settingLink":
                    if (!evt.OldValue.Equals(evt.NewValue))
                    {
                        SettingLink();
                    }
                    break;
                case "back":
                    if (!evt.OldValue.Equals(evt.NewValue))
                    {
                        Back();
                    }
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    if (isShow)
                    {
                        transform.localScale = Vector3.one;
                    }
                    else
                    {
                        transform.localScale = Vector3.zero;
                    }
                    break;
            }
        }

        /// <summary>
        /// 添加一个塔
        /// </summary>
        public void AddTower()
        {
            CommandDataEntity cmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
            CreateTowerCommandStr str = new CreateTowerCommandStr();
            //随机坐标
            str.Position = new Vector3(Random.Range(-7, 11), Random.Range(-4, 6), Random.Range(0, 50));
            cmdEntity.CreateTowerMessage = str;
        }

        /// <summary>
        /// 添加一个手机
        /// </summary>
        public void AddPhone()
        {
            CommandDataEntity cmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
            CreatePhoneCommandStr str = new CreatePhoneCommandStr();
            //随机坐标
            str.Position = new Vector3(Random.Range(-5f, 5f), 0.125f, Random.Range(-5f, 5f));
            cmdEntity.CreatePhoneMessage = str;
        }
        /// <summary>
        /// 设置连接
        /// </summary>
        public void SettingLink() {
            SettingLinkEntity settingLinkEntity = (SettingLinkEntity)FindObjectOfType<SettingLinkViewModel>().DataEntity;
            settingLinkEntity.isShow = !settingLinkEntity.isShow;
        }

        /// <summary>
        /// 返回
        /// </summary>
        public void Back() {
            var sceneEntity = (SceneSettingEntity)FindObjectOfType<SceneSettingModel>().DataEntity;
            sceneEntity.isShow = false;

            var settingLinkEntity = (SettingLinkEntity)FindObjectOfType<SettingLinkViewModel>().DataEntity;
            settingLinkEntity.isShow = false;

            var menEntity = (MenuEntity)FindObjectOfType<MenuViewModel>().DataEntity;
            menEntity.isShow = true;
        }
    }
}

