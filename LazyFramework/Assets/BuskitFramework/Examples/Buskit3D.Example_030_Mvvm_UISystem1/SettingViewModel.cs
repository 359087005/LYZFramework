/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingViewModel
* 创建日期：2019-03-15 15:01:41
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_29_Mvvm_Toggle
{
	public class SettingViewModel : ViewModelBehaviour
	{
        /// <summary>
        /// 
        /// </summary>
        [Binding(EntityPropertyName = "isChinese")]
        public ToggleView Chinese;

        /// <summary>
        /// 
        /// </summary>
        [Binding(EntityPropertyName = "isEnglish")]
        public ToggleView English;

        /// <summary>
        /// 
        /// </summary>
        [Binding(EntityPropertyName = "voice")]
        public SliderView sliderView;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            //实例化DataEntity
            this.DataEntity = new SettingEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
		
	}
}

