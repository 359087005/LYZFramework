/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ShowValueLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_036_Mvvm_BestPractices
{
    /// <summary>
    /// 显示属性变化事件，测试属性绑定是否正确
    /// </summary>
    public class LogicOperation : CommonLogic
    {
        /// <summary>
        /// 显示属性变化事件，测试属性绑定是否正确
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //处理背景音乐调整
            if (evt.PropertyName.Equals("BackgroundVolume"))
            {
                float newValue = (float)evt.NewValue;
                newValue = newValue * 100;
                string strNewValue = String.Format("{0:F}", newValue);

                ConfigViewModel viewModel = GetComponent<ConfigViewModel>();
                if (viewModel != null)
                {
                    ConfigEntity entity = utilsEntity.GetEntity<ConfigEntity>(gameObject);
                    entity.BackgroundText = "当前音量:[" + strNewValue + "%]";
                }
                return;
            }

            //处理前景音乐调整
            if (evt.PropertyName.Equals("ForegroundVolume"))
            {
                float newValue = (float)evt.NewValue;
                newValue = newValue * 100;
                string strNewValue = String.Format("{0:F}", newValue);

                ConfigViewModel viewModel = GetComponent<ConfigViewModel>();
                if (viewModel != null)
                {
                    ConfigEntity entity = (ConfigEntity)viewModel.DataEntity;
                    entity.ForegroundText = "当前音量:[" + strNewValue + "%]";
                }
                return;
            }

            //处理确定操作
            if (evt.PropertyName.Equals("OkClicked"))
            {
                //忽略初始化事件
                if(evt.NewValue == evt.OldValue)
                {
                    return;
                }

                int clicked = (int)evt.NewValue;
                if (clicked == -1)
                {
                    return;
                }

                ConfigViewModel viewModel = GetComponent<ConfigViewModel>();
                if (viewModel != null)
                {
                    ConfigEntity entity = (ConfigEntity)viewModel.DataEntity;
                    entity.OpenClose = false;
                }
                return;
            }

            //处理对话框打开或关闭操作
            if (evt.PropertyName.Equals("OpenClose"))
            {
                bool openClose = (bool)evt.NewValue;
                if (!openClose)
                {
                    gameObject.transform.localScale = Vector3.zero;
                }
                else
                {
                    gameObject.transform.localScale = Vector3.one;
                }
                return;
            }

            //处理开启前景音乐
            if (evt.PropertyName.Equals("EnableFgMusic"))
            {
                bool enable = (bool)evt.NewValue;
                Slider slier = GameObject.Find("Canvas/ImageConfigPanel/SliderFgVolume").GetComponent<Slider>();
                slier.interactable = enable;
                return;
            }

            //处理开启背景音乐
            if (evt.PropertyName.Equals("EnableBgMusic"))
            {
                bool enable = (bool)evt.NewValue;
                Slider slier = GameObject.Find("Canvas/ImageConfigPanel/SliderBgVolume").GetComponent<Slider>();
                slier.interactable = enable;
                return;
            }
        }
    }
}
