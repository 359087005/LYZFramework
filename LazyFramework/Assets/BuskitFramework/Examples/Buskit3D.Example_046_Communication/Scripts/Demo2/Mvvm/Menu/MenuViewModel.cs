/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuViewModel
* 创建日期：2019-04-11 14:18:00
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
	public class MenuViewModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "sceneSetting")]
        public ButtonView sceneSetting;
        [Binding(EntityPropertyName = "communication")]
        public ButtonView communication;
        [Binding(EntityPropertyName = "attribute")]
        public ButtonView attribute;
        protected override void Awake()
        {
            DataEntity = new MenuEntity();
            base.Awake();
        }

    }
}

