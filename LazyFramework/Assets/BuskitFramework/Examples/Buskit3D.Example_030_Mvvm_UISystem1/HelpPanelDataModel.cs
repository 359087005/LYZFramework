/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： HelpDataModel
* 创建日期：2019-03-15 08:54:35
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
	/// <summary>
    /// 帮助面板数据载体
    /// </summary>
	public class HelpPanelDataModel : ViewModelBehaviour
    {
        /// <summary>
        /// Dropdown
        /// </summary>
        [Binding(EntityPropertyName = "dropdownValue")]
        [HideInInspector]
        public DropdownView _dropdownValue;

        /// <summary>
        /// Text
        /// </summary>
        [Binding(EntityPropertyName = "showText")]
        [HideInInspector]
        public TextView _showText;

        protected override void Awake()
        {
            //Dropdown
            _dropdownValue = GameObject.Find("UIRoot/Panel/MvvMUI/HelpPanel/Dropdown").GetComponent<DropdownView>();

            //Text
            _showText = GameObject.Find("UIRoot/Panel/MvvMUI/HelpPanel/ShowTextUI/Text").GetComponent<TextView>();

            this.DataEntity = new HelpPanellEntity();
            base.Awake();      
            Watch(this);
        }
    }
}

