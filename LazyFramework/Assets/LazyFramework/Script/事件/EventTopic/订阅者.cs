using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class 订阅者 : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddEvent(EventTopic.TestTopic_1, Func);


    }
    public void Func(object topic,params object[] message)
    {
        Debug.Log("Func");
    }
   

}
