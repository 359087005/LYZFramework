/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommunicationViewModel
* 创建日期：2019-04-09 13:56:21
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_045_IoCApplication
{
	/// <summary>
    /// 
    /// </summary>
	public class CommunicationViewModel : ViewModelBehaviour
	{
        /// <summary>
        /// 进行呼叫
        /// </summary>
        [Binding(EntityPropertyName = "ringUp")]
        public ButtonView ringUp;
        /// <summary>
        /// 待机状态
        /// </summary>
        [Binding(EntityPropertyName = "cellStandby")]
        public ButtonView cellStandby;
        /// <summary>
        /// 提示标题
        /// </summary>
        [Binding(EntityPropertyName = "tip")]
        public TextView tip;
        /// <summary>
        /// 被呼叫的手机号码
        /// </summary>
        [Binding(EntityPropertyName = "phoneNumber")]
        public InputFieldView phoneNumber;
        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void Awake()
        {
            this.DataEntity = new CommunicationEntity();
            base.Awake();
        }
    }
}

