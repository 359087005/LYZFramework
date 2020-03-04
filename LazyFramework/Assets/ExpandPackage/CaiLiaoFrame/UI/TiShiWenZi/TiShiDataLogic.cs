using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;

public class TiShiDataLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("tiShiStr"))
        {
            this.BuZhouTiShi((string)evt.NewValue);
        }

    }
}
