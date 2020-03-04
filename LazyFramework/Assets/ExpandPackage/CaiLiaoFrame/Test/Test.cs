using Lazy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject btn_Box;
    public string processName;
    public const string PROCESS_NAME = "PROCESS_NAME";
    delegate void CommandEventHandle(object sender, EventArgs e);
   



    public static GameObject go;
    public int a = 0;
    private void Start()
    {
       
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(MyMethod), a);
        }
      
    }

    private void MyMethod(object a)
    {
        print(a);
        //Debug.Log("开了一个线程");
    }
}
