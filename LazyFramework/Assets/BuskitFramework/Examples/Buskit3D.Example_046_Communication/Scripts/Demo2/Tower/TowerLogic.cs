/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TowerLogic
* 创建日期：2019-04-10 14:14:06
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class TowerLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            Debug.Log(evt.PropertyName);
            switch (evt.PropertyName) {
                //动态改变位置
                case "towerPoint":
                    Transform parent = GameObject.Find(evt.NewValue.ToString()).transform;
                    transform.SetParent(parent);
                    transform.localPosition = Vector3.zero;
                    break;
                case "phoneList#Add":
                    PhoneEnter();
                    break;
                case "phoneList#Remove":
                    PhoneLeave();
                    break;
                case "phoneUseList#Add":
                    PhoneCall();
                    break;
                case "phoneUseList#Remove":
                    PhoneLeave();
                    break;
                case "towerList#Add":
                    AddLinkInfo();
                    break;
                case "towerList#Remove":
                    PhoneLeave();
                    break;
                case "reflush":
                    Reflush();
                    break;
            }
        }

        /// <summary>
        /// 电话进入
        /// </summary>
        public void PhoneEnter() {
            var towerEntity = (TowerEntity)GetComponent<DataModelBehaviour>().DataEntity;
            if (towerEntity.phoneUseList.Count > 0) {
                //变成黄色
                for (int j = 0; j < transform.GetChild(1).childCount; j++)
                {
                    transform.GetChild(1).GetChild(j).GetComponent<Renderer>().material.color = Color.yellow;
                }
                return;
            }
            //默认变成绿色
            for (int i = 0; i < transform.GetChild(1).childCount; i++) {
                transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material.color = Color.green;
            }            
        }
        /// <summary>
        /// 电话离开
        /// </summary>
        public void PhoneLeave()
        {
            var towerEntity = (TowerEntity)GetComponent<DataModelBehaviour>().DataEntity;
            
            if (towerEntity.phoneUseList.Count > 0)
            {
                //变成黄色
                for (int j = 0; j < transform.GetChild(1).childCount; j++)
                {
                    transform.GetChild(1).GetChild(j).GetComponent<Renderer>().material.color = Color.yellow;
                }
                return;
            }
            else if (towerEntity.phoneList.Count > 0)
            {
                //默认变成绿色
                for (int i = 0; i < transform.GetChild(1).childCount; i++)
                {
                    transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else {
                //默认色
                for (int i = 0; i < transform.GetChild(1).childCount; i++)
                {
                    transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material.color = new Color32(125, 125, 125, 255);
                }
            }
        }


        /// <summary>
        /// 接入通讯
        /// </summary>
        public void PhoneCall()
        {
            //变成黄色
            for (int j = 0; j < transform.GetChild(1).childCount; j++)
            {
                transform.GetChild(1).GetChild(j).GetComponent<Renderer>().material.color = Color.red;
            }
        }

        /// <summary>
        /// 挂掉电话
        /// </summary>
        public void PhoneOff() {
            var towerEntity = (TowerEntity)GetComponent<DataModelBehaviour>().DataEntity;

            if (towerEntity.phoneUseList.Count > 0)
            {
                //变成黄色
                for (int j = 0; j < transform.GetChild(1).childCount; j++)
                {
                    transform.GetChild(1).GetChild(j).GetComponent<Renderer>().material.color = Color.yellow;
                }
                return;
            }

            //默认变成绿色
            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material.color = Color.green;
            }
        }

        /// <summary>
        /// 添加一个链路信息
        /// </summary>
        public void AddLinkInfo()
        {
            var towerEntity = (TowerEntity)GetComponent<DataModelBehaviour>().DataEntity;
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
            }
            var entity = (SettingLinkEntity)FindObjectOfType<SettingLinkViewModel>().DataEntity;
            entity.linkList = linkInfo;
        }

        /// <summary>
        /// 删除一个链路信息
        /// </summary>
        public void RemoveLinkInfo()
        {
            var towerEntity = (TowerEntity)GetComponent<DataModelBehaviour>().DataEntity;
            if (towerEntity.towerList.Count == 0) {
                Debug.Log("当前没有添加链路信息");
                return;
            }

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
                var entity = (SettingLinkEntity)FindObjectOfType<SettingLinkViewModel>().DataEntity;
                entity.linkList = linkInfo;
            }
        }

        public void Reflush() {
            PhoneLeave();
        }
    }
}

