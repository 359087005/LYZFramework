/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingLinkLogic
* 创建日期：2019-04-11 09:52:45
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
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using System.Collections.Generic;

namespace Buskit3D.Example_046_Communication
{
    public class SettingLinkLogic : LogicBehaviour
    {
        /// <summary>
        /// 给下拉框赋值
        /// </summary>
        private List<string> showNames;

        /// <summary>
        /// 链接的文本
        /// </summary>
        public Text linkList;
        /// <summary>
        /// 当前UIroot对象
        /// </summary>
        public Transform settingLinkObj;
        /// <summary>
        /// 下拉框
        /// </summary>
        public Dropdown dropdown;

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {
                case "dropdown":
                    SettingLinkShow(((int )evt.NewValue));
                    break;
                case "linkList":
                    linkList.text = evt.NewValue.ToString();
                    break;
                case "btnSure":
                    if (evt.OldValue != evt.NewValue)
                    {
                        UIActveFalse();
                    }
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    if (isShow)
                    {
                        UIActveTrue();
                    }
                    else
                    {
                        UIActveFalse();
                    }
                    break;
            }
        }

        /// <summary>
        /// 设置连接的显示
        /// </summary>
        public void SettingLinkShow(int index) {
            //得到当前基站的信息
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            foreach (var key in pool.objectsDic.Keys) {
                pool.objectsDic[key].transform.GetChild(2).GetComponent<Renderer>().material.color = Color.yellow;
                if (key.ToString().Equals(dropdown.captionText.text)){
                    pool.objectsDic[key].transform.GetChild(2).GetComponent<Renderer>().material.color = Color.red;
                    var towerEntity = (TowerEntity)pool.objectsDic[key].DataEntity;
                    string linkInfo = "";
                    for (int i = 0; i < towerEntity.towerList.Count; i++)
                    {
                        if (i == 0)
                        {
                            linkInfo = towerEntity.towerList[0].ToString();
                        }
                        else
                        {
                            linkInfo = linkInfo + "_" + towerEntity.towerList[i];
                        }
                        var entity = (SettingLinkEntity)GetComponent<SettingLinkViewModel>().DataEntity;
                        entity.linkList = linkInfo;
                    }
                }
            }
        }

        /// <summary>
        /// 修改链接状态
        /// </summary>
        public void ChangLinkState(TowerModel clickModel) {
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            if (string.IsNullOrEmpty(dropdown.captionText.text)) return;
            int targetObjectID = System.Convert.ToInt32(dropdown.captionText.text);
            var tarrgetTowerModel = pool.objectsDic[targetObjectID];
            var tarrgetTowerEntity = (TowerEntity)tarrgetTowerModel.DataEntity;

            //添加和删除LinkList
            if (tarrgetTowerEntity.towerList.Contains(clickModel.DataEntity.objectID))
            {
                tarrgetTowerEntity.towerList.Remove(clickModel.DataEntity.objectID);
            }
            else {
                tarrgetTowerEntity.towerList.Add(clickModel.DataEntity.objectID);
            }
        }

        /// <summary>
        /// UI激活
        /// </summary>
        public void UIActveTrue() {
            transform.localScale = Vector3.one;
            Transform settingLink = GameObject.Find("SettingLink").transform;
            var settinglinkModel = settingLink.GetComponent<SettingLinkViewModel>();
            var settinglinkEntity = (SettingLinkEntity)settinglinkModel.DataEntity;
            UpdateDropdownView(settinglinkModel, settinglinkEntity);
        }
        /// <summary>
        /// UI关闭
        /// </summary>
        public void UIActveFalse()
        {
            transform.localScale = Vector3.zero;
            SettingLinkEntity settingLinkEntity = (SettingLinkEntity)GetComponent<SettingLinkViewModel>().DataEntity;
            settingLinkEntity.isShow = false;
            //得到当前基站的信息
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            foreach (var key in pool.objectsDic.Keys)
            {
                pool.objectsDic[key].transform.GetChild(2).GetComponent<Renderer>().material.color = Color.yellow;
            }
        }

        /// <summary>
        /// 刷数据
        /// </summary>
        /// <param name="showNames"></param>
        private void UpdateDropdownView(SettingLinkViewModel settinglinkModel, SettingLinkEntity settinglinkEntity)
        {
            showNames = new List<string>();
            Dropdown dropdownItem = settinglinkModel.transform.Find("Tower/Dropdown").GetComponent<Dropdown>();
            dropdownItem.options.Clear();
            Dropdown.OptionData tempData;

            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            pool.Foreach(ForeachPool);

            for (int i = 0; i < showNames.Count; i++)
            {
                tempData = new Dropdown.OptionData();
                tempData.text = showNames[i];
                dropdownItem.options.Add(tempData);
            }
            if (showNames.Count > 0)
            {
                dropdownItem.captionText.text = showNames[0];
            }
            settinglinkEntity.dropdown = 0;

        }
        private void ForeachPool(int index, TowerModel towerModel)
        {
            showNames.Add(towerModel.DataEntity.objectID.ToString());
        }



        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && transform.localScale.x ==1)
            {
                //鼠标发射射线，打印接收到射线的物体
                //射线检测，点击UI和空白处无反应
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.transform.name.Contains("Terrain"))
                    {
                        var model = hit.collider.transform.GetComponent<TowerModel>();
                        if (dropdown.captionText.text.Equals(model.DataEntity.objectID.ToString())) {
                            Debug.Log("不能添加自己");
                            return;
                        }
                        ChangLinkState(model);
                    }
                }
            }
        }
    }
}

