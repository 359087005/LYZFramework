/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ComminicationViewModel
* 创建日期：2019-04-11 13:50:51
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    /// 
    /// </summary>
	public class ComminicationViewModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "btnOpen")]
        public ButtonView btnOpen;
        [Binding(EntityPropertyName = "btnOff")]
        public ButtonView btnOff;
        [Binding(EntityPropertyName = "phone1")]
        public DropdownView phone1;
        [Binding(EntityPropertyName = "phone2")]
        public DropdownView phone2;
        [Binding(EntityPropertyName = "back")]
        public ButtonView back;
        [Binding(EntityPropertyName = "recover")]
        public ButtonView recover;

        protected override void Awake()
        {
            this.DataEntity = new ComminicationEntity();
            base.Awake();
        }

    }
}

