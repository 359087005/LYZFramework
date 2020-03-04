/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommandUIViewModel
* 创建日期：2019-04-16 17:08:16
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace IoCAndTwoCommunication
{
	/// <summary>
    /// 
    /// </summary>
	public class CommandUIViewModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "btnCreate")]
        public ButtonView btnCreate;
        [Binding(EntityPropertyName = "btnColor")]
        public ButtonView btnColor;
        [Binding(EntityPropertyName = "btnDelete")]
        public ButtonView btnDelete;
        [Binding(EntityPropertyName = "btnUndo")]
        public ButtonView btnUndo;
        [Binding(EntityPropertyName = "btnRedo")]
        public ButtonView btnRedo;
        [Binding(EntityPropertyName = "rotationSlider")]
        public SliderView rotationSlider;

        protected override void Awake()
        {
            this.DataEntity = new CommandUIViewEntity();
            base.Awake();
        }

    }
}

