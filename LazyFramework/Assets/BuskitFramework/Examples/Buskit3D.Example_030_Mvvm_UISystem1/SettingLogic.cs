/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingLogic
* 创建日期：2019-03-15 15:02:43
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using System;

namespace Buskit3D.Example_29_Mvvm_Toggle
{
    /// <summary>
    /// 设置面板业务逻辑处理类
    /// </summary>
    public class SettingLogic : LogicBehaviour
    {
        //显示信息Text
        public Text logicText1;
        public Text logicText2;

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case ("isChinese"):
                    if((bool)evt.NewValue)                   
                        logicText1.text = "当前语言：简体中文";                                      
                    break;
                case ("isEnglish"):
                    if ((bool)evt.NewValue)
                        logicText1.text = "language：English";
                    break;
                case ("voice"):
                    logicText2.text = "当前音量："+Convert.ToInt32(evt.NewValue).ToString();
                    break;
            }              
        }
    }
}

