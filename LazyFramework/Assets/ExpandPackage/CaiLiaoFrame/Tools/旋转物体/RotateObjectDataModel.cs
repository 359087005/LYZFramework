/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： NewDataModelScript
* 创建日期：2019-05-08 09:54:33
* 作者名称：汪海波
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

public class RotateObjectEntity : BaseDataModelEntity
{
    public Vector3 pos;
    public Vector3 rot;
    public float distance;
}

public class RotateObjectDataModel : DataModelBehaviour
{
    public RotateObjectEntity entity;
    void Awake()
    {
        entity = new RotateObjectEntity();
    }
    void Start()
    {
        this.DataEntity = entity;
        this.Watch(entity);
        base.Start();
    }

    /// <summary>
    /// 将数据写入到RecoverSystem中
    /// </summary>
    public override void SaveStorageData()
    {
        ((RotateObjectEntity)this.DataEntity).pos = GetComponent<RotateObject>().model.transform.localPosition;
        ((RotateObjectEntity)this.DataEntity).rot = GetComponent<RotateObject>().model.transform.localEulerAngles;
        ((RotateObjectEntity)this.DataEntity).distance = GetComponent<RotateObject>().distance;
        base.SaveStorageData();
    }

    /// <summary>
    /// 从RecoverSystem中读取数据
    /// </summary>
    public override void LoadStorageData()
    {
        //还原位置信息
        GetComponent<RotateObject>().model.transform.localPosition = ((RotateObjectEntity)this.DataEntity).pos;
        GetComponent<RotateObject>().model.transform.localEulerAngles = ((RotateObjectEntity)this.DataEntity).rot;
        GetComponent<RotateObject>().distance = ((RotateObjectEntity)this.DataEntity).distance;
        base.LoadStorageData();
    }
}


