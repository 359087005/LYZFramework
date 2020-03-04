/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuDataModel
* 创建日期：2019-03-19 09:29:40
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_35_Mvvm_Menu
{
    /// <summary>
    /// 菜单UI数据载体类
    /// </summary>
    public class MenuDataModel : ViewModelBehaviour
    {
        #region 设置实体数据和ViewModel的映射关系
        /// <summary>
        /// 设置实体数据和ViewModel的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "openLight")]
        public ToggleView _openLight;

        [Binding(EntityPropertyName = "lightAnalysisBtn")]
        public ButtonView _lightAnalysisBtn;

        [Binding(EntityPropertyName = "lightMessageBtn")]
        public ButtonView _lightMessageBtn;

        [Binding(EntityPropertyName = "openSchemeDesign")]
        public ToggleView _openSchemeDesign;

        [Binding(EntityPropertyName = "allDesignBtn")]
        public ButtonView _allDesignBtn;

        [Binding(EntityPropertyName = "basicBtn")]
        public ButtonView _basicBtn;

        [Binding(EntityPropertyName = "spaceDesignBtn")]
        public ButtonView _spaceDesignBtn;

        [Binding(EntityPropertyName = "openLightDesign")]
        public ToggleView _openLightDesign;

        [Binding(EntityPropertyName = "faceDesignBtn")]
        public ButtonView _faceDesignBtn;

        [Binding(EntityPropertyName = "earDesignBtn")]
        public ButtonView _earDesignBtn;

        [Binding(EntityPropertyName = "showText")]
        public TextView _showText;
        #endregion

        protected override void Awake()
        {
            this.DataEntity = new MenuEntity();
            base.Awake();
        }
    }
}

