/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ControlRotDataModel
* 创建日期：2019-05-08 10:41:34
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

public class  ControlRotEntity : BaseDataModelEntity
{
    public Vector3 cameraPos;
    public Vector3 cameraRot;
    public Vector3 targetPos;
    public Vector3 targetRot;
    public float distance;
    public bool isCanMove;
    public bool isCanRot;
}
	public class ControlRotDataModel : DataModelBehaviour
	{
        public  ControlRotEntity entity;
        void Awake()
        {
            entity = new  ControlRotEntity();
        }
        void Start()
        {
            this.DataEntity = entity;
            Watch(this);
            base.Start();
        }

        /// <summary>
        /// 将数据写入到RecoverSystem中
        /// </summary>
        public override void SaveStorageData()
        {
            (( ControlRotEntity)this.DataEntity).cameraPos = transform.localPosition;
            (( ControlRotEntity)this.DataEntity).cameraRot = transform.localEulerAngles;
            (( ControlRotEntity)this.DataEntity).targetPos =  ControlRot.instance.target.transform.localPosition;
            (( ControlRotEntity)this.DataEntity).targetRot =  ControlRot.instance.target.transform.localEulerAngles;
            (( ControlRotEntity)this.DataEntity).isCanMove =  ControlRot.instance.isCanMove;
            (( ControlRotEntity)this.DataEntity).isCanRot =  ControlRot.instance.isCanRot;
            base.SaveStorageData();
        }

        /// <summary>
        /// 从RecoverSystem中读取数据
        /// </summary>
        public override void LoadStorageData()
        {
            //还原位置信息
            transform.localPosition = (( ControlRotEntity)this.DataEntity).cameraPos;
            transform.localEulerAngles = (( ControlRotEntity)this.DataEntity).cameraRot;
             ControlRot.instance.target.transform.localPosition = (( ControlRotEntity)this.DataEntity).targetPos;
             ControlRot.instance.target.transform.localEulerAngles = (( ControlRotEntity)this.DataEntity).targetRot;
             ControlRot.instance.isCanMove = (( ControlRotEntity)this.DataEntity).isCanMove;
             ControlRot.instance.isCanRot = (( ControlRotEntity)this.DataEntity).isCanRot;

            base.LoadStorageData();
        }
	}


