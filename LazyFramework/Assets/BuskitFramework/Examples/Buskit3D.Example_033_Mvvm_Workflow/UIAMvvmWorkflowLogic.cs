/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIAMvvmWorkflowLogic
* 创建日期：2019-03-20 14:05:04
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_35_Mvvm_Workflow
{   
	/// <summary>
    /// 
    /// </summary>
	public class UIAMvvmWorkflowLogic : WorkflowLogic 
	{
		/// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            /// <summary>
            /// [建议直接使用类名]
            /// </summary>
            this.WorkflowTagName = "UIAMvvmWorkflowLogic";
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        public override void Execute()
        {
            base.Execute();

        }
        /// <summary>
        /// UI进入
        /// </summary>
        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log("OnEnter UIAMvvmWorkflowLogic");
            GameObject.Find("UIRoot").transform.Find("UI_1").transform.localScale = Vector3.one;
        }
        /// <summary>
        /// UI退出
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();
            //Debug.Log("OnExit UIAMvvmWorkflowLogic");
            GameObject.Find("UIRoot").transform.Find("UI_1").transform.localScale = Vector3.zero;
        }		
	}
}

