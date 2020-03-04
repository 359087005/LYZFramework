using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;
public class TiShiDataEntity : BaseDataModelEntity
{
    [RestoreFireLogic]
    public string tiShiStr;
}

public class TiShiDataModel : DataModelBehaviour
{
    void Awake()
    {
        TiShiDataEntity data = new TiShiDataEntity();
        this.DataEntity = data;
        Watch(this);
    }
}
