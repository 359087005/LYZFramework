/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PeojectViewModelBehaviour
* 创建日期：2019-03-15 17:12:56
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_033_MenuList
{

	/// <summary>
    /// 
    /// </summary>
	public class ProjectViewModelBehaviour : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "newButton")]
        public ButtonView newFile;
        [Binding(EntityPropertyName = "saveButton")]
        public ButtonView saveFile;
        /// <summary>
        /// 输入框
        /// </summary>
        [Binding(EntityPropertyName = "inputFileContent")]
        public InputFieldView inputFieldView;
        [Binding(EntityPropertyName = "isOpenMenu")]
        public ButtonView isOpenMenu;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            //实例化DataEntity
            this.DataEntity = new ProjectDataModelEntityScript();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }		
	}
}

