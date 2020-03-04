/*******************************************************************************
* 类 名 称：Command
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：撤销重做
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    public class Command : MonoBehaviour
    {
        /// <summary>
        /// 命令栈     记录当前操作前所有步骤
        /// </summary>
        List<ActionInfo> _lastAction = new List<ActionInfo>();
        /// <summary>
        /// 命令栈     记录当前操作后所有步骤
        /// </summary>
        List<ActionInfo> _nextAction = new List<ActionInfo>();
        /// <summary>
        /// 记录当前步骤   没有实质作用
        /// </summary>
        [SerializeField] private int curStep;

        public void AddOperation(ActionInfo actionInfo)
        {
            curStep++;
            _lastAction.Add(actionInfo);
        }
        public void ClearAll()
        {
            _lastAction.Clear();
            _nextAction.Clear();
        }
        /// <summary>
        /// 重做操作
        /// </summary>
        public void Redo()
        {
            if (_nextAction.Count == 0)
            {
                Debug.Log("没有下一步操作了");
                return;
            }
            else
            {
                curStep++;

                _nextAction[_nextAction.Count - 1].unDo?.Invoke();

                _lastAction.Add(_nextAction[_nextAction.Count - 1]);
                _nextAction.RemoveAt(_nextAction.Count - 1);
            }

        }
        /// <summary>
        /// 撤销操作
        /// </summary>
        public void Undo()
        {
            if (curStep - 1 <= 0)
            {
                Debug.Log("没有上一步操作了");
                return;
            }
            else
            {
                curStep--;

                _lastAction[_lastAction.Count - 1].unDo?.Invoke();

                _nextAction.Add(_lastAction[_lastAction.Count - 1]);
                _lastAction.RemoveAt(_lastAction.Count - 1);
            }
        }
    }
    public class ActionInfo
    {
        public string actionName;
        public Action reDo;
        public Action unDo;
        public ActionInfo(string _actionName, Action _reAction, Action _unDo)
        {
            actionName = _actionName;
            reDo = _reAction;
            unDo = _unDo;
        }
    }
}