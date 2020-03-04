/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： BagLogic
* 创建日期：2019-03-21 09:22:45
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

namespace Buskit3D.Example_36_Mvvm_Bag
{
    /// <summary>
    /// 
    /// </summary>
    public class BagLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "buttonView":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    var _entity = (BagEntity)GetComponent<DataModelBehaviour>().DataEntity;
                    if (string.IsNullOrEmpty(_entity.inputFieldContent)) return;
                    SearchObj(_entity.inputFieldContent);
                    break;
                case "inputFieldContent":
                    break;
                case "itemID":                    
                    break;
                case "tabID":
                    if ((int)evt.NewValue == -1) return;
                    ChangItemShow((int)evt.NewValue);
                    break;
                case "initBag":
                    if (evt.NewValue.ToString().Equals("1"))
                        InitBag();
                    break;
            }
        }

        public void SearchObj(string name) {
            //得到当前需要的所有的ID
            ArrayList idLisr = new ArrayList();
            foreach (var value in BagTrigger.Instance.bagDic.Values) {
                if (name.Equals(value.name.ToString())) {
                    idLisr.Add(value.id);
                }
            }

            for (int i = 0; i < BagTrigger.Instance.itemParent.childCount; i++) {
                int id = System.Convert.ToInt32(BagTrigger.Instance.itemParent.GetChild(i).name);
                if (idLisr.Contains(id))
                {
                    BagTrigger.Instance.itemParent.GetChild(i).gameObject.SetActive(true);
                }
                else {
                    BagTrigger.Instance.itemParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public void InitBag()
        {
            ArrayList tabArrayList = new ArrayList();
            //加载Item/Tab
            GetFirstTab();
            foreach (var value in BagTrigger.Instance.bagDic.Values) {

                GetItem(value);
                if (tabArrayList.Contains(value.type)) {
                    continue;
                }
                GetTab(value,tabArrayList);
            }
        }

        public void ChangItemShow(int tabID) {
            //if (!isCanInit) return;            
            for (int i = 0; i < BagTrigger.Instance.itemParent.childCount; i++) {

                if (tabID == 0) {
                    BagTrigger.Instance.itemParent.GetChild(i).gameObject.SetActive(true);
                    continue;
                }
                
                int id = System.Convert.ToInt32(BagTrigger.Instance.itemParent.GetChild(i).name);
                if (BagTrigger.Instance.bagDic[id].type == tabID)
                {
                    BagTrigger.Instance.itemParent.GetChild(i).gameObject.SetActive(true);
                }
                else {
                    BagTrigger.Instance.itemParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        private void GetItem(BagItem value) {
            //加载Item
            var objItem = ReplayManager.GetInstance("BagItem", Vector3.zero);
            objItem.SetActive(true);
            objItem.name = value.id.ToString();
            objItem.transform.localScale = Vector3.one;
            objItem.transform.SetParent(BagTrigger.Instance.itemParent);
            ReplayManager.RegisterPrefab(objItem);
        }

        private void GetTab(BagItem value ,ArrayList tabArrayList) {
            //加载Tab
            var objTab = ReplayManager.GetInstance("BagTab", Vector3.zero);
            objTab.SetActive(true);
            objTab.name = value.type.ToString();
            objTab.transform.localScale = Vector3.one;
            objTab.transform.SetParent(BagTrigger.Instance.tabParent);
            objTab.transform.Find("Background/Label").GetComponent<Text>().text = value.type.ToString();
            objTab.GetComponent<Toggle>().group = BagTrigger.Instance.tabGroup;
            ReplayManager.RegisterPrefab(objTab);
            tabArrayList.Add(value.type);            
        }

        private void GetFirstTab()
        {
            //加载Tab
            var objTab = ReplayManager.GetInstance("BagTab", Vector3.zero);
            objTab.SetActive(true);
            objTab.name = "全部";
            objTab.transform.localScale = Vector3.one;
            objTab.transform.SetParent(BagTrigger.Instance.tabParent);
            objTab.transform.Find("Background/Label").GetComponent<Text>().text = "全部";
            objTab.GetComponent<Toggle>().group = BagTrigger.Instance.tabGroup;
            ReplayManager.RegisterPrefab(objTab);
            objTab.GetComponent<Toggle>().isOn = true;
        }
    }
}

