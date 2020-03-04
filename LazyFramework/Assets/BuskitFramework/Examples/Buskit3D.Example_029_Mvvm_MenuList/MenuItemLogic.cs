/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MenuItemLogic
* 创建日期：2019-03-18 15:29:49
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;
using UnityEngine.UI;

namespace Buskit3D.Example_033_MenuList
{   
	/// <summary>
    /// 
    /// </summary>
	public class MenuItemLogic : LogicBehaviour 
	{

		public override void ProcessLogic(PropertyMessage evt)
        {
            Debug.Log(evt.PropertyName);
            switch (evt.PropertyName)
            {
                case "readButton":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //场景跳转
                    ProjectTrigger.Instance.OnClickLoadSence_A();
                    break;

                case "delButton":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //删除当前的信息
                    DelGameObject();
                    break;
                case "itemName":
                    Debug.Log(evt.NewValue);
                    transform.Find("Label").GetComponent<Text>().text = evt.NewValue.ToString();
                    break;
            }
        }

        private void DelGameObject() {
            var _dataEntity = GetComponent<MenuItemViewModel>().DataEntity;
            var _menuEntity = (MenuItemEntity)_dataEntity;
            int id = _menuEntity.menuID;
            ProjectTrigger.Instance.menuItemDic.Remove(id);
            gameObject.SetActive(false);
        }
	}
}

