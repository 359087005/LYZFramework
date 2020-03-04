/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuItemViewModel
* 创建日期：2019-03-18 15:28:40
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buskit3D.Example_033_MenuList
{
	/// <summary>
    /// 
    /// </summary>
	public class MenuItemViewModel : ViewModelBehaviour
	{       

        /// <summary>
        /// 读取按钮
        /// </summary>
        [Binding(EntityPropertyName ="readButton")]
        public ButtonView readButton;

        /// <summary>
        /// 删除按钮
        /// </summary>
        [Binding(EntityPropertyName = "delButton")]
        public ButtonView delButton;
        [Binding(EntityPropertyName = "itemName")]
        public TextView textView;
        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            readButton = transform.Find("Read").GetComponent<ButtonView>();
            delButton = transform.Find("Del").GetComponent<ButtonView>();
            textView = transform.Find("Label").GetComponent<TextView>();
            //实例化DataEntity
            this.DataEntity = new MenuItemEntity();
            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }

        public override void LoadStorageData()
        {
            base.LoadStorageData();
            var menuItemEntity = (MenuItemEntity)DataEntity;
            transform.SetParent(ProjectTrigger.Instance.contentParent);
            transform.GetComponent<Toggle>().group = ProjectTrigger.Instance.toggleGroup;
            transform.Find("Label").GetComponent<Text>().text = menuItemEntity.itemName;
            transform.localScale = Vector3.one;
            gameObject.SetActive(true);
        }
    }
}

