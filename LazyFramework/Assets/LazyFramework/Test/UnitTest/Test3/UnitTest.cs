/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UnitTest
* 创建日期：2019-12-12 17:40:32
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：MDUIFrame的单元测试案例
******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class UnitTest : MonoBehaviour, IProcessEvent
    {


        public enum UTEvents
        {
            openSplashPanel,
            startShiYan,
            enterMainMenu,
            openYuXiPanel,
            UpdateYuXiContent,
            YuXiTi,
            TiShi,
            PutInBag,
            TakeOutFromBag,
            moveItemFailed,

        }
        public void ProcessEvent(Message msg)
        {
            if (msg.id == (ushort)UTEvents.openSplashPanel)
            {
                UIFrame.Instance.Open("首页");
            }
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            UIFrame.Instance.panelDic = new Dictionary<string, string>() {
            {"主菜单","主菜单"},
            {"首页", "首页" },
            {"背包","背包" },
            {"滑动列表","滑动列表" },
            { "特效展示","特效展示"}
        };
            this.Register((ushort)UTEvents.openSplashPanel);
            MessageCenter.SendEvent(new Message((ushort)UTEvents.openSplashPanel));
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
        }
    }
}

