/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：RowTitleViewModel
* 创建日期：2019-04-01 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：标题栏ViewModel
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_032_Mvvm_Table
{
    /// <summary>
    /// 标题栏ViewModel
    /// </summary>
    public class RowTitleViewModel : ViewModelBehaviour
    {
        [Binding(EntityPropertyName = "id")]
        public TextView idText;

        [Binding(EntityPropertyName = "name")]
        public TextView nameText;

        [Binding(EntityPropertyName = "age")]
        public TextView ageText;

        [Binding(EntityPropertyName = "clazz")]
        public TextView classText;

        [Binding(EntityPropertyName = "status")]
        public TextView statusText;

        [Binding(EntityPropertyName = "addClicked")]
        public ButtonView addButton;

        protected override void Awake()
        {
            this.DataEntity = new RowTitleEntity();
            base.Awake();
        }
    }
}
