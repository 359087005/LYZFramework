/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AttributeMenuModel
* 创建日期：2019-04-12 16:23:03
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
	public class AttributeMenuModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "menu_Tower")]
        public ButtonView menu_Tower;
        [Binding(EntityPropertyName = "menu_Phone")]
        public ButtonView menu_Phone;
        [Binding(EntityPropertyName = "menu_Link")]
        public ButtonView menu_Link;
        [Binding(EntityPropertyName = "menu_Statistics")]
        public ButtonView menu_Statistics;
        [Binding(EntityPropertyName = "menu_Back")]
        public ButtonView menu_Back;

        protected override void Awake()
        {
            DataEntity = new AttributeMenuEntity();
            base.Awake();
        }
    }
}

