/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommunicationLogic
* 创建日期：2019-04-09 14:08:31
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

namespace Buskit3D.Example_045_IoCApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class CommunicationLogic : LogicBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        private CommunicationEntity _communicationEntity;
        private void Start()
        {
            _communicationEntity = (CommunicationEntity)GetComponent<CommunicationViewModel>().DataEntity;
        }
        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {
                case "ringUp":
                    if (evt.OldValue == evt.NewValue) return;
                    Debug.Log("ringUp" + ":" + evt.NewValue);
                    _communicationEntity.isCanRingUp = false;
                    _communicationEntity.isCellStandby = true;
                    break;
                case "cellStandby":
                    if (evt.OldValue == evt.NewValue) return;
                    _communicationEntity.isCellStandby = false;
                    Debug.Log("cellStandby" + ":" + evt.NewValue);
                    ClosePhone();
                    break;
                case "isCanRingUp":
                    Debug.Log("cellStandby" + ":" + evt.NewValue);
                    bool active = (bool)evt.NewValue;
                    GameObject.Find("RingUp").GetComponent<Button>().interactable = active;
                    break;
                case "isCellStandby":
                    Debug.Log("cellStandby" + ":" + evt.NewValue);
                    active = (bool)evt.NewValue;
                    GameObject.Find("CellStandby").GetComponent<Button>().interactable = active;
                    break;
                case "phoneNumber":
                    Debug.Log("cellStandby" + ":" + evt.NewValue);
                    if (!string.IsNullOrEmpty(evt.NewValue.ToString()))
                    {
                        //当前是否在服务区
                        IphoneEntity entity = (IphoneEntity)GameObject.Find("ball1").GetComponent<IphoneModel>().DataEntity;
                        if (entity.areaID != -1)
                        {
                            _communicationEntity.isCanRingUp = true;
                        }
                        else
                        {
                            _communicationEntity.isCanRingUp = false;
                        }
                    }
                    else {
                        _communicationEntity.isCanRingUp = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// 手机挂断的操作
        /// </summary>
        public void ClosePhone() {
            var targetIphoneModel = GameObject.Find("ball1").GetComponent<IphoneModel>().targetIphoneModel;
            if (targetIphoneModel != null) {
                var targetIphoneEntity = (IphoneEntity)targetIphoneModel.DataEntity;
                targetIphoneEntity.currentState = 0;
            }
            var iphoneModel = GameObject.Find("ball1").GetComponent<IphoneModel>();
            var iphoneEntity = (IphoneEntity)iphoneModel.DataEntity;
            iphoneEntity.currentState = 0;
            foreach (var key in Trigger.instance.dicTa.Keys)
            {
                var value = ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic;
                if (value.ContainsKey(iphoneModel.iphoneID))
                {
                    ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic.Remove(iphoneModel.iphoneID);
                }
                if (targetIphoneModel != null && value.ContainsKey(targetIphoneModel.iphoneID))
                {
                    ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic.Remove(targetIphoneModel.iphoneID);
                }
            }
        }
    }
}

