/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： WfMainViewModel
* 创建日期：2019-03-20 14:08:47
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_35_Mvvm_Workflow
{
	/// <summary>
    /// 
    /// </summary>
	public class WfMainViewModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "UIA")]
        public ButtonView buttonUIA;
        [Binding(EntityPropertyName = "UIB")]
        public ButtonView buttonUIB;
        [Binding(EntityPropertyName = "UIC")]
        public ButtonView buttonUIC;
        [Binding(EntityPropertyName = "UID")]
        public ButtonView buttonUID;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {

            //实例化DataEntity
            this.DataEntity = new WfMainEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
		
	}
}

