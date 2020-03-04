/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IphoneLogic
* 创建日期：2019-04-08 16:03:29
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_045_IoCApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class IphoneLogic : LogicBehaviour
    {
        /// <summary>
        /// 当前手机的数据模型
        /// </summary>
        private IphoneModel iphoneModel;
        /// <summary>
        /// 当前手机的数据实体
        /// </summary>
        private IphoneEntity iphoneEntity;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            iphoneModel = GetComponent<IphoneModel>();
            iphoneEntity = (IphoneEntity)iphoneModel.DataEntity;
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                //当前状态改变
                case "currentState":
                    int _state = (int)evt.NewValue;
                    switch (_state)
                    {
                        //待机状态
                        case 0:
                            CellStandby();
                            break;

                        //去电状态
                        case 1:
                            RingUp();
                            break;

                        //拨通状态
                        case 2:
                            LinkSuccess();
                            break;

                        default:
                            break;
                    }
                    break;
                //不同区域触发不同的监听反馈
                case "areaID":
                    int _areaID = (int)evt.NewValue;
                    ChangeArea(_areaID);
                    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// 拨打电话
        /// </summary>
        public void RingUp()
        {
            //保存当前的打电话与接电话的链接
            CommunicationEntity entity = (CommunicationEntity)GameObject.Find("RootUI").GetComponent<CommunicationViewModel>().DataEntity;
            int number = System.Convert.ToInt32(entity.phoneNumber);
            if (number == iphoneModel.iphoneID) {
                Debug.Log("占线");
                iphoneEntity.currentState = 0;
                return;
            }

            //获取所有的通信塔传递信号
            var list = iphoneModel.taModel.taList;
            var currentEntity = (TaEntity)(iphoneModel.taModel.DataEntity);
            if (!currentEntity.currenUseDic.ContainsKey(iphoneModel.iphoneID))
            {
                currentEntity.currenUseDic.Add(iphoneModel.iphoneID, number);
            }
            for (int i = 0; i < list.Count; i++)
            {
                var model = list[i].GetComponent<TaModel>();
                var taEntity = (TaEntity)(model.DataEntity);
                if (!taEntity.currenUseDic.ContainsKey(iphoneModel.iphoneID))
                    taEntity.currenUseDic.Add(iphoneModel.iphoneID, number);
            }
        }

        /// <summary>
        /// 手机待机
        /// </summary>
        public void CellStandby()
        {
            //颜色变为固定颜色白色
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        /// <summary>
        /// 拨通状态
        /// </summary>
        public void LinkSuccess()
        {
            //拨通后改变颜色
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log(iphoneModel.iphoneID+"-----------------------");
        }

        /// <summary>
        /// 手机转移区域
        /// </summary>
        public void ChangeArea(int currentAreaID)
        {
            //无信号状态
            if (currentAreaID == -1)
            {
                var iphoneModel = GetComponent<IphoneModel>();
                if (iphoneModel != null)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                iphoneModel.taModel = null;

                if (iphoneModel.iphoneID == 1)
                {
                    CommunicationEntity entity = (CommunicationEntity)GameObject.Find("RootUI").GetComponent<CommunicationViewModel>().DataEntity;
                    entity.isCanRingUp = false;
                    entity.isCellStandby = false;
                }
            }
            else
            {
                if (iphoneModel.iphoneID == 1)
                {
                    CommunicationEntity entity = (CommunicationEntity)GameObject.Find("RootUI").GetComponent<CommunicationViewModel>().DataEntity;
                    if (string.IsNullOrEmpty(entity.phoneNumber))
                    {
                        entity.isCanRingUp = false;
                    }
                    else
                    {
                        entity.isCanRingUp = true;
                    }
                }
            }
        }
    }
}


