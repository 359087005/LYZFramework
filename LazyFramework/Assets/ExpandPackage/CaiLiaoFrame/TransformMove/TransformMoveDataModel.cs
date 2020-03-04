using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;



public class TransformMoveDataEntity:BaseDataModelEntity
{
}

public class TransformMoveDataModel : DataModelBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        TransformMoveDataEntity tmde = new TransformMoveDataEntity();
        this.DataEntity = tmde;
        Watch(this);
    }
    void Start()
    {
        base.Start();
        OnFire(this.DataEntity, "transInfo");
    }
}
