using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Lazy
{
    public abstract class DebugCommandBase : MonoBehaviour
    {
        protected DebugGUI debugGUI;
        protected void Awake()
        {
            debugGUI = DebugGUI.Instance;
            if (debugGUI == null)
                Debug.LogError("未找到debugGUI");
            AddCommand();
        }
        protected virtual string[] CutCommand(string _commandStr)
        {
             return Regex.Split(_commandStr, ";");
        }
        /// <summary>
        /// 增加一条debug命令
        /// </summary>
        /// <param name="debugCommand"></param>
        protected void AddCommandFunc(DebugGUI.DebugCommandEventHandler _debugCommand)
        {
            debugGUI.evtDebugCommand += _debugCommand;
        }
        protected abstract void AddCommand();
    }
  

}
