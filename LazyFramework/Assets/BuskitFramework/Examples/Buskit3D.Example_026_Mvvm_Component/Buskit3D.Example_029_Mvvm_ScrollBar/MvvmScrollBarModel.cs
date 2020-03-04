
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_28_Mvvm_ScrollBar
{
    public class MvvmScrollBarModel : ViewModelBehaviour
    {
        #region 绑定组件
        [Binding(EntityPropertyName = "scrollbarValue")]
        public ScrollbarView scrollbarValue;
        [Binding(EntityPropertyName = "context")]
        public TextView textView;
        #endregion

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            //实例化DataEntity
            this.DataEntity = new MvvmScrollBarEntity();
            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
    }
}


