/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIEquipmentDataModel
* 创建日期：2019-04-1 09:36:53
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

//includes for Unity

//includes for System
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine.UI;

/// <summary>
/// 名称空间定义：SHHY.Otoliths
/// </summary>
namespace Buskit3D.Example_039_Mvvm_ObjectIntroduce
{
	/// <summary>
    /// 类 名 称：SHHY.Otoliths.UIEquipmentDataModel
	/// 类 功 能：
	/// 主要接口：
    /// </summary>
	public class UIEquipmentViewDataModel : ViewModelBehaviour
	{
        [Binding(EntityPropertyName = "IsChangeToLast")]
        public ButtonView IsChangeToLast;
        [Binding(EntityPropertyName = "IsChangeToNext")]
        public ButtonView IsChangeToNext;
        [Binding(EntityPropertyName = "title")]
        public TextView title;
        [Binding(EntityPropertyName = "content")]
        public TextView content;
        /// <summary>
        /// 生命周期
        /// </summary>
        protected override void Awake()
        {
            IsChangeToLast = transform.Find("Interface/Last").GetComponent<ButtonView>();
            IsChangeToNext = transform.Find("Interface/Next").GetComponent<ButtonView>();
            title = transform.Find("Interface/BackGround/Title").GetComponent<TextView>();
            content = transform.Find("Interface/BackGround/Scroll View/Viewport/Content/Text").GetComponent<TextView>();

            //实例化DataEntity
            this.DataEntity = new UIEquipmentDataModelEntity();
            //监听自身Entity
            this.Watch(this);
            //在父类的Awake函数中执行绑定过程
            base.Awake();
        } 
    }
}

