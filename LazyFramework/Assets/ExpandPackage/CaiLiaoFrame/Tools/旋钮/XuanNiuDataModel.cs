/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： XuanNiuDataMode
* 创建日期：2019-05-08 11:57:52
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

public class XuanNiuEntity : BaseDataModelEntity
{
    public Vector3 rot;
    public bool isRot;
    public float hasrotAngles;
}
	public class XuanNiuDataModel : DataModelBehaviour
	{
        public XuanNiuEntity entity;
        void Awake()
        {
            entity = new XuanNiuEntity();
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
            ((XuanNiuEntity)this.DataEntity).rot =  GetComponent<XuanNiu>().target.transform.localEulerAngles;
            ((XuanNiuEntity)this.DataEntity).isRot = GetComponent<XuanNiu>().isRot;
            ((XuanNiuEntity)this.DataEntity).hasrotAngles = GetComponent<XuanNiu>().hasrotAngles;
            base.SaveStorageData();
        }

        /// <summary>
        /// 从RecoverSystem中读取数据
        /// </summary>
        public override void LoadStorageData()
        {
            //还原位置信息
            GetComponent<XuanNiu>().target.transform.localEulerAngles = ((XuanNiuEntity)this.DataEntity).rot;
            GetComponent<XuanNiu>().isRot = ((XuanNiuEntity)this.DataEntity).isRot;
            GetComponent<XuanNiu>().hasrotAngles = ((XuanNiuEntity)this.DataEntity).hasrotAngles;
            base.LoadStorageData();
        }
	}


