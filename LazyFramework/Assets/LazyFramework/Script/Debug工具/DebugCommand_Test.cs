using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommand_Test : DebugCommandBase
{
    protected override void AddCommand()
    {
        AddCommandFunc((command)=>
        {
            if (command == "test")
                Debug.Log("测试指令");
        });
    }
}
