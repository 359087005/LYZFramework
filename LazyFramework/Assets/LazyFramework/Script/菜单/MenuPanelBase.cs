using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    /// <summary>
    /// 菜单面板基类
    /// </summary>
    public abstract class MenuPanelBase : MonoBehaviour
    {
        protected bool isShow = false;
        /// <summary>
        /// 加载时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="func"></param>
        public abstract void LoadFuncUI(object sender, List<MenuFunctionBase> func);
    }
}
