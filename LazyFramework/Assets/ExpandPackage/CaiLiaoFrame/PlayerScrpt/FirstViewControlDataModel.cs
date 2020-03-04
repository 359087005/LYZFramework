using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;


public class FirstViewDataEntity :BaseDataModelEntity
{
    [RestoreFireLogic]
    public int playerState = (int)PlayerControlState.aI;
    [RestoreFireLogic]
    public bool isCanSimpleMove = true, isCanRotate = true, isXuanNiu = false, isCanUpDown = true, isCanScrollView = true;
    [RestoreFireLogic]
    public bool isTextInput;
    [RestoreFireLogic]
    public Vector3 playerPos, playerRot;
    [RestoreFireLogic]
    public Vector3 cameraPos, cameraRot;
    [RestoreFireLogic]
    public float fieldOfView;
    [RestoreFireLogic]
    public int iniQuaternionInt;
    [RestoreFireLogic]
    public float fov;
}
public class FirstViewControlDataModel : DataModelBehaviour
{
    public static FirstViewControlDataModel instance;
    void Awake()
    {
        instance = this;
        FirstViewDataEntity fvde = new FirstViewDataEntity();
        this.DataEntity = fvde;
        Watch(this);

       
    }
    void Start()
    {
        base.Start();
    }
    public override void LoadStorageData()
    {
        base.LoadStorageData();
        
    }
    public override void SaveStorageData()
    {
         base.SaveStorageData();

    }
}
