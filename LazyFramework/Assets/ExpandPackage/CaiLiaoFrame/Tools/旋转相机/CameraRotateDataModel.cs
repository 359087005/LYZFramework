/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： NewDataModelScript
* 创建日期：2019-05-05 16:33:12
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

	public class CameraRotateEntity : BaseDataModelEntity
    {
        public Vector3 cameraPos;
        public Vector3 cameraRot;
        public Vector3 targetPos;
        public Vector3 targetRot;
        public float distance;
        public bool isCanMove;
        public bool isCanRot;
    }
    public class CameraRotateDataModel : DataModelBehaviour
	{
        public CameraRotateEntity entity;
        void Awake()
        {
            entity = new CameraRotateEntity();
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
            ((CameraRotateEntity)this.DataEntity).cameraPos = transform.localPosition;
            ((CameraRotateEntity)this.DataEntity).cameraRot = transform.localEulerAngles;
            ((CameraRotateEntity)this.DataEntity).targetPos = CameraRotate.instance.target.transform.localPosition;
            ((CameraRotateEntity)this.DataEntity).targetRot = CameraRotate.instance.target.transform.localEulerAngles;
            ((CameraRotateEntity)this.DataEntity).isCanMove = CameraRotate.instance.isCanMove;
            ((CameraRotateEntity)this.DataEntity).isCanRot = CameraRotate.instance.isCanRot;
            base.SaveStorageData();
        }

        /// <summary>
        /// 从RecoverSystem中读取数据
        /// </summary>
        public override void LoadStorageData()
        {
            //还原位置信息
            transform.localPosition = ((CameraRotateEntity )this.DataEntity).cameraPos;
            transform.localEulerAngles = ((CameraRotateEntity)this.DataEntity).cameraRot;
            CameraRotate.instance.target.transform.localPosition = ((CameraRotateEntity)this.DataEntity).targetPos;
            CameraRotate.instance.target.transform.localEulerAngles = ((CameraRotateEntity)this.DataEntity).targetRot;
            CameraRotate.instance.isCanMove = ((CameraRotateEntity)this.DataEntity).isCanMove;
            CameraRotate.instance.isCanRot = ((CameraRotateEntity)this.DataEntity).isCanRot;

            base.LoadStorageData();
        }

	}
