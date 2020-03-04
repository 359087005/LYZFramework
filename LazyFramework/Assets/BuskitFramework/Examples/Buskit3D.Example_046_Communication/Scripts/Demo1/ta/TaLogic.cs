/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TaLogic
* 创建日期：2019-04-08 16:04:39
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using System.Collections.Generic;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

namespace Buskit3D.Example_045_IoCApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class TaLogic : LogicBehaviour
    {
        /// <summary>
        /// 当前信号塔的数据实体
        /// </summary>
        public TaEntity taEntity;

        private void Start()
        {
            taEntity = (TaEntity)(GetComponent<TaModel>().DataEntity);
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                //添加一个进入信号范围的手机
                case "currentExistList#Add":
                    AddEnterPhone();
                    break;
                //移除一个进入信号范围的手机
                case "currentExistList#Remove":
                    RemoveEnterPhone();
                    break;
                //添加一个使用的记录
                case "currenUseDic#Add":
                    AddUsePhone(evt);
                    break;
                //移除一个使用的记录
                case "currenUseDic#Remove":
                    RemoveUsePhone();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 判断当前信号范围内是否有可以连通的手机
        /// </summary>
        /// <param name="currentGoPhone">去电的号码</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public void IsLink(int callphone, int targetPhone, WatchableList<int> list)
        {
            if (list.Contains(targetPhone)) {
                var obj = FindObjectsOfType<IphoneModel>();
                IphoneModel _callphoneModel = null;
                IphoneModel _targetPhoneModel = null;
                for (int j = 0; j < obj.Length; j++)
                {
                    if (obj[j].iphoneID == callphone)
                    {
                        ((IphoneEntity)obj[j].DataEntity).currentState = 2;
                        _callphoneModel = obj[j];
                    }
                    else if (obj[j].iphoneID == targetPhone)
                    {
                        ((IphoneEntity)obj[j].DataEntity).currentState = 2;
                        _targetPhoneModel = obj[j];
                    }
                }
                _callphoneModel.targetIphoneModel = _targetPhoneModel;
                _targetPhoneModel.targetIphoneModel = _callphoneModel;
            }
        }

        /// <summary>
        /// 添加一个进入的手机
        /// </summary>
        public void AddEnterPhone()
        {
            if(taEntity.currenUseDic.Count <= 0)
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }

        /// <summary>
        /// 添加一个使用的记录
        /// </summary>
        public void AddUsePhone(PropertyMessage evt)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            //获取当前字典中所有的通讯记录，判断当前信号塔是否有可以通信的用户
            int callPhone = ((KeyValuePair<int, int>)evt.NewValue).Key;
            int targetPhone = ((KeyValuePair<int, int>)evt.NewValue).Value;
            var list = taEntity.currentExistList;
            IsLink(callPhone,targetPhone, list);
        }

        /// <summary>
        /// 移除一个进入的手机
        /// </summary>
        public void RemoveEnterPhone()
        {
            if (taEntity.currenUseDic.Count > 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (taEntity.currentExistList.Count > 0) {
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        /// <summary>
        /// 移除一个进入的手机
        /// </summary>
        public void RemoveUsePhone()
        {
            if (taEntity.currenUseDic.Count > 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (taEntity.currentExistList.Count > 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}


