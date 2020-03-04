/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ConfigViewModel
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_036_Mvvm_BestPractices
{
    /// <summary>
    /// 配置界面的ViewModel
    /// </summary>
    public class BagViewModel : CommonViewModel
    {
        [Binding(EntityPropertyName = "selectedHua")]
        public ButtonView selectedHua;

        [Binding(EntityPropertyName = "selectedNiao")]
        public ButtonView selectedNiao;

        [Binding(EntityPropertyName = "selectedYu")]
        public ButtonView selectedYu;

        [Binding(EntityPropertyName = "selectedChong")]
        public ButtonView selectedChong;

        [Binding(EntityPropertyName = "selectedShou")]
        public ButtonView selectedShou;

        [Binding(EntityPropertyName = "selectedChong2")]
        public ButtonView selectedChong2;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            this.DataEntity = new BagEntity();
            base.Awake();
        }
    }
}
