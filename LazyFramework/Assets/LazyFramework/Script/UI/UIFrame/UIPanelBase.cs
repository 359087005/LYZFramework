/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIPanelBase
* 创建日期：2019-12-12 17:38:12
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Lazy
{
    /// <summary>
    /// 面板基类
    /// </summary>
    public class UIPanelBase : MonoBehaviour ,IProcessEvent
    {
        public bool IsActive
        {
            get
            {
                if (transform.localScale.x <= 0.1f) return false;
                if (!gameObject.activeSelf) return false;
                return true;
            }
        }
        public ShowType showType;
        public EffectType effectType;
        /// <summary>
        /// 每次进入面板时调用
        /// </summary>
        public virtual void OnEnter() {
            print("进入" + this.name);
            
        }
        /// <summary>
        /// 每次退出面板时调用
        /// </summary>
        public virtual void OnExit() {
            print("退出"+this.name);
        }
        /// <summary>
        /// 每次暂停面板时调用
        /// </summary>
        public virtual void OnPause() {
            print("暂停" + this.name);

        }
        /// <summary>
        /// 每次恢复面板时调用
        /// </summary>
        public virtual void OnResume() {
            print("恢复" + this.name);
        }

        public virtual void ProcessEvent(Message msg)
        {
         
        }

        public enum ShowType
        {
            normal,
            /// <summary>
            /// 退出其他面板
            /// </summary>
            hideOtherPanels
        }
        public enum EffectType
        {
            /// <summary>
            /// 通过canvasgroup组件控制透明度,交互,接收射线
            /// </summary>
            byCanvaGroup,
            /// <summary>
            /// SetActive()
            /// </summary>
            active,
            /// <summary>
            /// 0到1,改变大小
            /// </summary>
            scale,
           
        }
    }
}

