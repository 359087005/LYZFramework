using Com.Rainier.Buskit.Unity.Architecture.Aop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLogic : Com.Rainier.Buskit3D.LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("name"))
        {
            GameObject.FindObjectOfType<SampleTest>().CubeClicked((string)evt.NewValue);
        }
        if (evt.PropertyName.Equals("btnClicked"))
        {
            GameObject.FindObjectOfType<SampleTest>().BtnClicked((int)evt.NewValue);
        }
        
    }
}
