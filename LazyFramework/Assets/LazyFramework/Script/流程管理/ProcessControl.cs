/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProcessControl
* 创建日期：2019-06-27 15:01:45
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：1.适用于各种流程项目，便于流程管理和后期修改。2.可以基于这套写法可以实现跳转任意步骤、和跳转至下一步功能。
******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Lazy
{
	public class ProcessControl : MonoSingleton<ProcessControl>
    {
        [SerializeField]int curProcess=0;
        [SerializeField] int curStep=0;
        public string CurProcessName
        {
            get
            {
                return processBases[curProcess].processName;
            }
        }
        public string CurStepName
        {
            get
            {
                return processBases[curProcess].processBases[curStep].stepName;
            }
        }
        public string CurStepContent
        {
            get
            {
                return processBases[curProcess].processBases[curStep].stepContent;
            }
        }
        public string CurProcessContent
        {
            get
            {
                return processBases[curProcess].processContent;
            }
        }
        [SerializeField]List<ProcessBase> processBases = new List<ProcessBase>();
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
                processBases.Add(new ProcessBase(processName));
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
                processBases.Add(new ProcessBase(processName));
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
                processBases.Add(new ProcessBase(processName, content));
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
            if (!Checkprocess()) return;
            if (processBases[curProcess].processBases[curStep].step != null)
            {
                processBases[curProcess].processBases[curStep].step.Invoke();
                if (processBases[curProcess].processBases.Count <= ++curStep)
                {
                    curProcess++;
                    curStep = 0;
                    if (onTrigger != null)
                    {
                        onTrigger.Invoke();
                    }
                }
            }
        }
       /// <summary>
       /// 跳转流程
       /// </summary>
       /// <param name="processname">流程名称</param>
       /// <param name="stepname">步骤名称</param>
       /// <param name="init">该流程需要初始化的内容</param>
        public void JumpStep(string processname,string stepname,Action init=null)
        {
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
                                Debug.Log("进入流程：" + processname + " 的：" + stepname + " 步骤");
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
            for (int i = 0; i < processBases.Count; i++)
            {
                if(processBases[i].processName == processname)
                {
                    if(init!=null)
                    {
                        init.Invoke();
                    }
                    curProcess = i;
                    curStep = 0;
                }
            }
            NextStep();
        }
        #endregion
        #region 内部方法
        private bool Checkprocess()
        {
            if (curProcess >= processBases.Count)
            {
                Debug.Log("没有下一个流程了");
                return false;
            }
            if (processBases[curProcess].processBases.Count <= curStep)
            {
                curProcess++;
                curStep = 0;
            }
            if (curProcess >= processBases.Count)
            {
                Debug.Log("没有下一个流程了");
                return false;
            }
            return true;
        }

      
        #endregion
    }
    [Serializable]
    public class ProcessBase
    {
        public string processName;
        public string processContent;
        public List<StepBase> processBases = new List<StepBase>();
        public ProcessBase(string name,string content)
        {
            processName = name;
            processContent = content;
        }
        public ProcessBase(string name)
        {
            processName = name;
        }
        public void AddFunc(string name,Action action)
        {
            processBases.Add(new StepBase(name,action));
        }
        public void AddFunc(string name,string content, Action action)
        {
            processBases.Add(new StepBase(name, content, action));
        }
    }
    [Serializable]
    public class StepBase
    {
        public string stepName;
        public string stepContent;
        public Action step;
        public StepBase(string stepname,Action step)
        {
            stepName = stepname;
            this.step = step;
        }
        public StepBase(string stepname, string content, Action step)
        {
            stepContent = content;
            stepName = stepname;
            this.step = step;
        }
    }
}

