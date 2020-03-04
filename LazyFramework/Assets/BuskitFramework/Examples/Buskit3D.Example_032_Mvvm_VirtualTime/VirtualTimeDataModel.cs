/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： VirtualTimeDataModel
* 创建日期：2019-03-15 17:12:28
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;
using System;

namespace Buskit3D.Example_34_Mvvm_VirtualTime
{
	/// <summary>
    /// 虚拟时间数据载体
    /// </summary>
	public class VirtualTimeDataModel : ViewModelBehaviour
    {
# region 设置实体数据和ViewModel的映射关系
        [Binding(EntityPropertyName = "dataTimeText")]
        public TextView _dataTimeText;

        [Binding(EntityPropertyName = "inputFildHour")]
        public InputFieldView _inputFildHour;
        [Binding(EntityPropertyName = "inputFildMinute")]
        public InputFieldView _inputFildMinute;
        [Binding(EntityPropertyName = "sureBtnTop")]
        public ButtonView _sureBtnTop;

        [Binding(EntityPropertyName = "inputFildY")]
        public InputFieldView _inputFildY;
        [Binding(EntityPropertyName = "inputFildM")]
        public InputFieldView _inputFildM;
        [Binding(EntityPropertyName = "inputFildD")]
        public InputFieldView _inputFildD;
        [Binding(EntityPropertyName = "inputFildh")]
        public InputFieldView _inputFildh;
        [Binding(EntityPropertyName = "inputFildm")]
        public InputFieldView _inputFildm;
        [Binding(EntityPropertyName = "inputFilds")]
        public InputFieldView _inputFilds;
        [Binding(EntityPropertyName = "sureBtnDown")]
        public ButtonView _sureBtnDown;

        #endregion

        /// <summary>
        /// 初始化DataEntity ,并监视自己
        /// </summary>
        protected override void Awake()
        {
            this.DataEntity = new VirtualTimeEntity();           
            base.Awake();
        }

        public override void OnModelValueChanged(PropertyMessage msg)
        {
            base.OnModelValueChanged(msg);
            if (msg.NewValue is DateTime)
            {
                OnModelValueChangedStuff<DateTime>(msg);
            }
        }
    }
}

