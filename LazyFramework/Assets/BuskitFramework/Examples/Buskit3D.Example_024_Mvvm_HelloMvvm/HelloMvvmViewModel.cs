/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：HelloMvvmViewModel
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Training.Mvvm.A
{
    public class HelloMvvmViewModel : ViewModelBehaviour
    {
        /// <summary>
        /// 设置Slider1与Entity中configValue1的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "textSize")]
        public SliderView textSize;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            //实例化DataEntity
            this.DataEntity = new HelloMvvmEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
    }
}
