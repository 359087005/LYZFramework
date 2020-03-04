/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MainMvvmWorkflowLogic
* 创建日期：2019-03-20 14:03:09
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
	public class MainMvvmWorkflowLogic : WorkflowLogic 
	{
		/// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            /// <summary>
            /// [建议直接使用类名]
            /// </summary>
            this.WorkflowTagName = "MainMvvmWorkflowLogic";
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            Debug.Log("123123");
        }
        /// <summary>
        /// UI进入
        /// </summary>
        public override void OnEnter()
        {
            //Debug.Log("如果有数据初始化，建议在这里操作");
            GameObject.Find("UIRoot").transform.Find("MainPanel").gameObject.SetActive(true);
        }
        /// <summary>
        /// UI退出
        /// </summary>
        public override void OnExit()
        {
            //Debug.Log("根据当前的需求，对UI切换做相应的操作，案例中，无操作");

        }		
	}
}

