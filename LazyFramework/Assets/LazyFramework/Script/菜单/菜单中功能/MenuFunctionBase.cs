/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：MenuFunctionBase
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：物体的菜单功能基类，  通过菜单的物体功能需要继承这个基类，然后写对应的功能逻辑。
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lazy
{
    [RequireComponent(typeof(Menu_Interactive))]
    public abstract class MenuFunctionBase : MonoBehaviour
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string funcName;
        /// <summary>
        /// 该功能是否激活
        /// </summary>
        public bool isActive = true;
        /// <summary>
        /// 该功能是否可用
        /// </summary>
        public bool canUse = true;
        public delegate void OnFunctionEndEventHandler(GameObject sender, string functionName);
        public event OnFunctionEndEventHandler OnFuncEnd;
        public virtual void Start()
        {
            Init();
        }
        /// <summary>
        /// 初始化功能
        /// </summary>
        protected virtual void Init()
        {
            EventManager.AddEvent(EventTopic.FUNC_MENU, OnFuncDown);
        }
        void OnDestroy()
        {
            EventManager.RemoveEvent(EventTopic.FUNC_MENU, OnFuncDown);
        }
        /// <summary>
        /// 当菜单中的功能按钮被按下时调用
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        private void OnFuncDown(object obj, params object[] message)
        {
            if (obj is GameObject)
            {
                if ((GameObject)obj == gameObject && (string)message[0] == "OnFunctionStart" && (string)message[1] == funcName)
                {
                    StartFunc();
                }
            }
        }
        /// <summary>
        /// 当功能生命周期结束时，手动调用这个功能。
        /// </summary>
        protected virtual void OnFunctionEnd()
        {
            EventManager.SendMessage(EventTopic.FUNC_MENU, gameObject, "OnFunctionEnd", funcName);
        }
        /// <summary>
        /// 当功能被唤醒时    （功能逻辑写在这个方法里）
        /// </summary>
        protected abstract void StartFunc();
    }
}