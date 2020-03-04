using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;

public struct HLStruct
{
    public int index;
    public bool isOn;
}
public class HighLightDataEntity : BaseDataModelEntity
{
    [RestoreFireLogic]
    public HLStruct hLStruct = new HLStruct();
}


public class HighLightDataModel : DataModelBehaviour
{
    public static HighLightDataModel instance;
    void Awake()
    {
        instance = this;
        HighLightDataEntity hld = new HighLightDataEntity();
        this.DataEntity = hld;
        Watch(this);
       
    }
   
}

