using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;


public class HighLightLogic : LogicBehaviour
{
    public static HighLightLogic instance;
    public List<GameObject> allHLList;
    void Awake()
    {
        instance = this;
        //foreach(Collider e in GameObject.FindObjectsOfType<Collider>())
        //{
        //    allCollidersList.Add(e);
        //}
    }
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("hLStruct"))
        {
            HLStruct hLS = (HLStruct)evt.NewValue;
            if (hLS.isOn)
            {
                allHLList[hLS.index].OnHightligher();
            }
            else
            {
                allHLList[hLS.index].OffHightligher();
            }
        }
    }
}
