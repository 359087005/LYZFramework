using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

public class TransformMoveLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("transInfo"))
        {
            transform.localPosition = ((TransInfo)evt.NewValue).positon;
            transform.localEulerAngles = ((TransInfo)evt.NewValue).eulerAngles;
            transform.localScale = ((TransInfo)evt.NewValue).localScale;
        }
        
    }
}
