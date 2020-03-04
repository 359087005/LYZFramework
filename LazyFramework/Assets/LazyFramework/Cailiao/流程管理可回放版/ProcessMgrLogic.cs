using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;
using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProcessMgrEntity : BaseDataModelEntity
{
    public int processNext;
}
public class ProcessMgrLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("processNext"))
        {
            ProcessMgr.Instance.NextStepFunc();
        }
    }
}
