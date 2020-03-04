/*******************************************************************************
* 版本声明：v1.0.0
* 类 名 称： ProcessControl
* 创建日期：2019-06-27 15:01:45
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：1.适用于各种流程项目，便于流程管理和后期修改。2.可以基于这套写法可以实现跳转任意步骤、和下一步功能。
******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Lazy
{
    [RequireComponent(typeof(ProcessMgrModel))]
    [RequireComponent(typeof(ProcessMgrLogic))]
    public abstract class ProcessMgr : MonoSingleton<ProcessMgr>
    {
        int curProcess=0;
        int curStep = -1;
        private Action OnClickDown=null;
        /// <summary>
        /// 当前模块名
        /// </summary>
        public string CurProcessName
        {
            get
            {
                return processBases[curProcess].processName;
            }
        }
        /// <summary>
        /// 当前步骤名
        /// </summary>
        public string CurStepName
        {
            get
            {
                return processBases[curProcess].processBases[curStep].stepName;
            }
        }
        /// <summary>
        /// 当前步骤说明
        /// </summary>
        public string CurStepContent
        {
            get
            {
                return processBases[curProcess].processBases[curStep].stepContent;
            }
        }
        /// <summary>
        /// 当前模块说明
        /// </summary>
        public string CurProcessContent
        {
            get
            {
                return processBases[curProcess].processContent;
            }
        }
        /// <summary>
        /// 流程管理数据
        /// </summary>
        List<ProcessBasePlayBack> processBases = new List<ProcessBasePlayBack>();
        #region 对外函数
        /// <summary>
        /// 增加一步
        /// </summary>
        /// <param name="processName">流程名</param>
        /// <param name="stepname">步骤名</param>
        /// <param name="step">步骤代码</param>
        public void AddStep(string processName,string stepname, Action step)
        {
            bool isExistProcess = false;
            for (int i = 0; i < processBases.Count; i++)
            {
                if(processBases[i].processName == processName)
                {
                    isExistProcess = true;
                    processBases[i].AddFunc(stepname,step);
                }
            }
            if(!isExistProcess)
            {
                processBases.Add(new ProcessBasePlayBack(processName));
                for (int i = 0; i < processBases.Count; i++)
                {
                    if (processBases[i].processName == processName)
                    {
                        processBases[i].AddFunc(stepname, step);
                    }
                }
            }
        }
        /// <summary>
        /// 增加一步
        /// </summary>
        /// <param name="processName">流程名称</param>
        /// <param name="stepname">步骤名称</param>
        /// <param name="stepContent">步骤内容</param>
        /// <param name="step">步骤代码</param>
        public void AddStep(string processName, string stepname, string stepContent, Action step)
        {
            bool isExistProcess = false;
            for (int i = 0; i < processBases.Count; i++)
            {
                if (processBases[i].processName == processName)
                {
                    isExistProcess = true;
                    processBases[i].AddFunc(stepname, step);
                }
            }
            if (!isExistProcess)
            {
                processBases.Add(new ProcessBasePlayBack(processName));
                for (int i = 0; i < processBases.Count; i++)
                {
                    if (processBases[i].processName == processName)
                    {
                        processBases[i].AddFunc(stepname, stepContent, step);
                    }
                }
            }
        }
        /// <summary>
        /// 增加一步
        /// </summary>
        /// <param name="processName">流程名</param>
        /// <param name="content">流程内容描述</param>
        /// <param name="stepname">步骤名</param>
        /// <param name="stepContent">步骤内容描述</param>
        /// <param name="step">步骤代码</param>
        public void AddStep(string processName, string content, string stepname,string stepContent, Action step)
        {
            bool isExistProcess = false;
            for (int i = 0; i < processBases.Count; i++)
            {
                if (processBases[i].processName == processName)
                {
                    isExistProcess = true;
                    processBases[i].AddFunc(stepname, step);
                }
            }
            if (!isExistProcess)
            {
                processBases.Add(new ProcessBasePlayBack(processName, content));
                for (int i = 0; i < processBases.Count; i++)
                {
                    if (processBases[i].processName == processName)
                    {
                        processBases[i].AddFunc(stepname,stepContent, step);
                    }
                }
            }
        }
        /// <summary>
        /// 进行下一步
        /// </summary>
        public void NextStep(Action onTrigger = null)
        {
            OnClickDown = onTrigger;
            ((ProcessMgrEntity)ProcessMgrModel.ins.DataEntity).processNext++;
        }
        /// <summary>
        /// 执行下一步
        /// </summary>
        /// <param name="onTrigger"></param>
        public void NextStepFunc(Action onTrigger = null)
        {
            if (OnClickDown != null)
            {
                OnClickDown.Invoke();
                OnClickDown = null;
            }
         
            if (curProcess >= processBases.Count)
            {
                Debug.LogError("没有下一个步骤");
                return;
            }
            if (processBases[curProcess].processBases.Count <= ++curStep)
            {
                curProcess++;
                curStep = 0;
                if (onTrigger != null)
                {
                    onTrigger.Invoke();
                    onTrigger = null;
                }
            }
            if (curProcess >= processBases.Count)
            {
                Debug.LogError("没有下一个步骤");
                return;
            }
            processBases[curProcess].processBases[curStep].step.Invoke();
            Debug.Log("当前模块：" + CurProcessName + "当前步骤：" + CurStepName);
        }
        /// <summary>
        /// 跳转流程
        /// </summary>
        /// <param name="processname">流程名称</param>
        /// <param name="stepname">步骤名称</param>
        /// <param name="init">该流程需要初始化的内容</param>
        public void JumpStep(string processname,string stepname,Action init=null)
        {
            InitFunc();
            for (int i = 0; i < processBases.Count; i++)
            {
                if(processBases[i].processName == processname)
                {
                    for (int j = 0; j < processBases[i].processBases.Count; j++)
                    {
                        if (processBases[i].processBases[j].stepName == stepname)
                        {
                            if (processBases[i].processBases[j].step != null)
                            {
                                Debug.Log("当前模块：" + CurProcessName + "当前步骤：" + CurStepName);
                                if (init != null)
                                {
                                    init.Invoke();
                                }
                                curProcess = i;
                                curStep = j+1;
                                processBases[i].processBases[j].step.Invoke();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 跳转到该流程的第一步
        /// </summary>
        /// <param name="processname">流程名称</param>
        /// <param name="init">该流程需要初始化的内容</param>
        public void JumpStep(string processname,Action init=null)
        {
            InitFunc();
            for (int i = 0; i < processBases.Count; i++)
            {
                if(processBases[i].processName == processname)
                {
                    if(init!=null)
                    {
                        init.Invoke();
                    }
                    curProcess = i;
                    curStep = -1;
                }
            }
            NextStep();
        }
        /// <summary>
        /// 根据流程、步骤索引跳步
        /// </summary>
        /// <param name="processIndex"></param>
        /// <param name="stepIndex"></param>
        /// <param name="init"></param>
        public void JumpStep(int processIndex,int stepIndex,Action init =null)
        {
            curProcess = processIndex;
            curStep = stepIndex;
            if (init != null)
            {
                init.Invoke();
            }
        }

        protected virtual void Start()
        {
            Init();
        }
        /// <summary>
        /// 流程初始化
        /// </summary>
        protected abstract void Init();
        /// <summary>
        /// 
        /// </summary>
        public virtual void ReDo()
        {

        }
        public virtual void UnDo()
        {

        }
        #endregion
        #region 内部方法
        private void InitFunc()
        {
            OnClickDown = null;
        }
        #endregion
    }
    #region 数据结构
    [Serializable]
    public class ProcessBasePlayBack
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string processName;
        /// <summary>
        /// 模块说明
        /// </summary>
        public string processContent;
        /// <summary>
        /// 步骤包含得流程
        /// </summary>
        public List<StepBasePlayBack> processBases = new List<StepBasePlayBack>();
        public ProcessBasePlayBack(string name,string content)
        {
            processName = name;
            processContent = content;
        }
        public ProcessBasePlayBack(string name)
        {
            processName = name;
        }
        public void AddFunc(string name,Action action)
        {
            processBases.Add(new StepBasePlayBack(name,action));
        }
        public void AddFunc(string name,string content, Action action)
        {
            processBases.Add(new StepBasePlayBack(name, content, action));
        }
    }
    [Serializable]
    public class StepBasePlayBack
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string stepName;
        /// <summary>
        /// 步骤说明
        /// </summary>
        public string stepContent;
        /// <summary>
        /// 当前执行
        /// </summary>
        public Action step;
        /// <summary>
        /// 撤销
        /// </summary>
        public Action undo;
        /// <summary>
        /// 重做
        /// </summary>
        public Action redo;
        public StepBasePlayBack(string stepname,Action step)
        {
            stepName = stepname;
            this.step = step;
        }
        public StepBasePlayBack(string stepname, string content, Action step)
        {
            stepContent = content;
            stepName = stepname;
            this.step = step;
        }
    }
    #endregion
}

