/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：RowDataViewModel
* 创建日期：2019-04-01 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：数据行ViewModel
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_032_Mvvm_Table
{
    /// <summary>
    /// 数据行ViewModel
    /// </summary>
    public class RowDataViewModel : ViewModelBehaviour
    {
        [Binding(EntityPropertyName = "id")]
        public TextView idText;

        [Binding(EntityPropertyName = "name")]
        public InputFieldView nameText;

        [Binding(EntityPropertyName = "age")]
        public InputFieldView ageText;

        [Binding(EntityPropertyName = "clazz")]
        public InputFieldView classText;

        [Binding(EntityPropertyName = "status")]
        public InputFieldView statusText;

        [Binding(EntityPropertyName = "removeClicked")]
        public ButtonView removeButton;
    }
}
