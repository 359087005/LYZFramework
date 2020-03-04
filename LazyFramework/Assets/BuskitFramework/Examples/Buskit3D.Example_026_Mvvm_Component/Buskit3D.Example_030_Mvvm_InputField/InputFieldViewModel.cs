/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： InputFieldViewModel
* 创建日期：2019-03-18 10:58:15
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_30_Mvvm_InputField
{
	/// <summary>
    /// 
    /// </summary>
	public class InputFieldViewModel : ViewModelBehaviour
	{
        /// <summary>
        /// 
        /// </summary>
        [Binding(EntityPropertyName = "inputContent")]
        public InputFieldView InputFieldView;

		/// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {

            //实例化DataEntity
            this.DataEntity = new InputFieldEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
		
	}
}

