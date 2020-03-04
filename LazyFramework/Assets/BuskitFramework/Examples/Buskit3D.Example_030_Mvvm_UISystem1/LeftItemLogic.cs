/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LeftItemLogic
* 创建日期：2019-03-14 14:17:35
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using System;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
    /// <summary>
    /// 
    /// </summary>
    public class LeftItemLogic : LogicBehaviour
    {
        /// <summary>
        /// 设置panel
        /// </summary>
        public GameObject setPanel;
        /// <summary>
        /// 帮助panel
        /// </summary>
        public GameObject helpPanel;
        /// <summary>
        /// 游戏panel
        /// </summary>
        public GameObject gamePanel;

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "isOnSetting":
                    if ((bool)evt.NewValue)
                    {
                        setPanel.gameObject.transform.localScale = Vector3.one;
                        helpPanel.gameObject.transform.localScale = Vector3.zero;
                        gamePanel.gameObject.transform.localScale = Vector3.zero;
                    }
                    break;
                case "isOnHelp":
                    if ((bool)evt.NewValue)
                    {
                        setPanel.gameObject.transform.localScale = Vector3.zero;
                        helpPanel.gameObject.transform.localScale = Vector3.one;
                        gamePanel.gameObject.transform.localScale = Vector3.zero;
                    }
                    break;
                case "isOnGame":
                    if ((bool)evt.NewValue)
                    {
                        setPanel.gameObject.transform.localScale = Vector3.zero;
                        helpPanel.gameObject.transform.localScale = Vector3.zero;
                        gamePanel.gameObject.transform.localScale = Vector3.one;
                    }
                    break;
            }
        }
    }
}

