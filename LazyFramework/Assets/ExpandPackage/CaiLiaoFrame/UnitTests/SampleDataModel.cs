using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

public class SampleDataEntity :BaseDataModelEntity
{
  public  string name;
  public int btnClicked;
    /// <summary>
    /// 场景恢复用
    /// </summary>
  public WatchableList<TransInfo> gameObjectList = new WatchableList<TransInfo>();
}

public class SampleDataModel : DataModelBehaviour
{
    public static SampleDataModel instance;
    void Awake()
    {
        instance = this;
        SampleDataEntity sde = new SampleDataEntity();
        this.DataEntity = sde;
        Watch(this);
    }

    public override void SaveStorageData()
    {
        base.SaveStorageData();
        SampleTest.instance.SaveStorageData();
    }
    public override void LoadStorageData()
    {
        base.LoadStorageData();
        SampleTest.instance.LoadStorageData();

    }
}
