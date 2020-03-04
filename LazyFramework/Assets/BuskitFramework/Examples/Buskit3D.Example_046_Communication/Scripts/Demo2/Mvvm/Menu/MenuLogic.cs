/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuLogic
* 创建日期：2019-04-11 14:18:34
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
    public class MenuLogic : LogicBehaviour
    {
        private MenuEntity menuEntity;

        private void Start()
        {
            menuEntity = (MenuEntity)GetComponent<MenuViewModel>().DataEntity;
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {

                case "sceneSetting":
                    if (evt.OldValue != evt.NewValue) {
                        var sceneSettingModel = (SceneSettingEntity)FindObjectOfType<SceneSettingModel>().DataEntity;
                        sceneSettingModel.isShow = true;
                        menuEntity.isShow = false;
                    }
                    break;
                case "communication":
                    if (evt.OldValue != evt.NewValue)
                    {
                        var comminicationEntity = (ComminicationEntity)FindObjectOfType<ComminicationViewModel>().DataEntity;
                        comminicationEntity.isShow = true;
                        menuEntity.isShow = false;
                    }
                    break;
                case "attribute":
                    if (evt.OldValue != evt.NewValue)
                    {
                        var attributeMenuEntity = (AttributeMenuEntity)FindObjectOfType<AttributeMenuModel>().DataEntity;
                        attributeMenuEntity.isShow = true;
                        menuEntity.isShow = false;
                    }
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    if (isShow)
                    {
                        transform.localScale = Vector3.one;
                    }
                    else {
                        transform.localScale = Vector3.zero;
                    }
                    break;
            }
        }
    }
}

