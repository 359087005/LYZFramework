/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： DragObjectDataModel
* 创建日期：2019-05-08 13:47:52
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

public class DragObjectEntity : BaseDataModelEntity
{
    public Vector3 pos;
    public float scale;
}
public class DragObjectDataModel : DataModelBehaviour
{
    public DragObjectEntity entity;
    void Awake()
    {
        entity = new DragObjectEntity();
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
        ((DragObjectEntity)this.DataEntity).pos = transform.localPosition;
        ((DragObjectEntity)this.DataEntity).scale = GetComponent<DragObject>().scale;
        base.SaveStorageData();
    }

    /// <summary>
    /// 从RecoverSystem中读取数据
    /// </summary>
    public override void LoadStorageData()
    {
        //还原位置信息
        transform.localPosition = ((DragObjectEntity)this.DataEntity).pos;
        transform.localScale = ((DragObjectEntity)this.DataEntity).scale*Vector3.one;
        GetComponent<DragObject>().scale = ((DragObjectEntity)this.DataEntity).scale;
        base.LoadStorageData();
    }

}


