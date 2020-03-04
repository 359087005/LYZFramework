/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TaModel
* 创建日期：2019-04-08 16:04:25
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using System.Collections.Generic;

namespace Buskit3D.Example_045_IoCApplication
{
	/// <summary>
    /// 
    /// </summary>
	public class TaModel : DataModelBehaviour
	{
        public List<GameObject> taList = new List<GameObject>();
        /// <summary>
        /// 塔的区域ID
        /// </summary>
        public int areaId;

        private void Awake()
        {
            this.DataEntity = new TaEntity();
            Watch(this);
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log("进入触发器");
            //进入触发器执行的代码，添加一个进入的手机
            var entity = (TaEntity)this.DataEntity;
            var _iphoneModel = collider.gameObject.GetComponent<IphoneModel>();
            var iphoneEntity = (IphoneEntity)_iphoneModel.DataEntity;

            _iphoneModel.taModel = this;

            iphoneEntity.areaID = areaId;
            entity.currentExistList.Add(_iphoneModel.iphoneID);
        }

        private void OnTriggerExit(Collider collider)
        {
            //离开触发器执行的代码，删除当前进入的手机
            var taEntity = (TaEntity)this.DataEntity;
            var _iphoneModel = collider.gameObject.GetComponent<IphoneModel>();
            var iphoneEntity = (IphoneEntity)_iphoneModel.DataEntity;

            if (_iphoneModel.targetIphoneModel != null) {
                var targetIphoneEntity = (IphoneEntity)_iphoneModel.targetIphoneModel.DataEntity;
                targetIphoneEntity.currentState = 0;
            }

            taEntity.currentExistList.Remove(_iphoneModel.iphoneID);
            iphoneEntity.areaID = -1;
            iphoneEntity.currentState = 0;

            foreach (var key in Trigger.instance.dicTa.Keys) {
                var value = ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic;
                if (value.ContainsKey(_iphoneModel.iphoneID)) {
                    ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic.Remove(_iphoneModel.iphoneID);
                }
                if (_iphoneModel.targetIphoneModel!=null && value.ContainsKey(_iphoneModel.targetIphoneModel.iphoneID)) {
                    ((TaEntity)Trigger.instance.dicTa[key].DataEntity).currenUseDic.Remove(_iphoneModel.targetIphoneModel.iphoneID);
                }
            }
        }
    }
}

