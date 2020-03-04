using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 发布者 : MonoBehaviour
{    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            EventManager.SendMessage(EventTopic.TestTopic_1, gameObject, "打滚儿",transform);
        }

    }

}
