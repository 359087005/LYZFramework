/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProjectLogicScript
* 创建日期：2019-03-15 17:13:41
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Buskit3D.Example_033_MenuList
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectLogicScript : LogicBehaviour
    {       

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {

                case "newButton":
                    if ((int)evt.NewValue == 0) return;
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    //场景跳转
                    ProjectTrigger.Instance.OnClickLoadSence_A();
                    break;

                case "saveButton":
                    if ((int)evt.NewValue == 0) return;                    
                    AddItem();
                    break;

                case "isOpenMenu":
                    if ((int)evt.NewValue == 0) return;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    break;
            }
        }

        private void AddItem() {
            int id = Random.Range(0, 1000000);
            var _dataEntity = GetComponent<ProjectViewModelBehaviour>().DataEntity;
            var _projectDataModelEntityScript = (ProjectDataModelEntityScript)_dataEntity;
            string name = _projectDataModelEntityScript.inputFileContent;
            if (ProjectTrigger.Instance.menuItemDic.ContainsKey(id)) {
                Debug.Log("中奖了");
                return;
            }
            //执行覆盖
            if (ProjectTrigger.Instance.menuItemDic.ContainsValue(name)) {
                OverrrideItem(id, name);
            }
            //执行创建
            else {
                CreateItem(id, name);
            }
        }

        /// <summary>
        /// 创建一个Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="prefabName"></param>
        private void CreateItem(int id,string name,string prefabName = "MenuItem") {
            ProjectTrigger.Instance.menuItemDic.Add(id, name);
            var obj = ReplayManager.GetInstance(prefabName, Vector3.zero);
            obj.transform.SetParent(ProjectTrigger.Instance.contentParent);
            obj.GetComponent<Toggle>().group = ProjectTrigger.Instance.toggleGroup;
            ReplayManager.RegisterPrefab(obj);
            var _dataEntity = obj.GetComponent<MenuItemViewModel>().DataEntity;
            var _menuEntiyt = (MenuItemEntity)_dataEntity;
            _menuEntiyt.menuID = id;
            _menuEntiyt.itemName = name;
        }

        /// <summary>
        /// 执行覆盖
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        private void OverrrideItem(int id,string name) {
            //执行覆盖
            Debug.Log("覆盖操作");
        }
    }
}