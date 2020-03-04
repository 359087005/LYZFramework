
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： WfUIInit
* 创建日期：2019-03-20 14:19:27
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_35_Mvvm_Workflow
{
    /// <summary>
    /// 
    /// </summary>
	public class WfUIInit : MonoBehaviour 
	{

		/// <summary>
        /// Unity Method
        /// </summary>
		void Start () 
		{
            //初始化工作流
            var _wfModel = FindObjectOfType<WorkflowDataModel>();
            var entity = (WorkflowEntity)_wfModel.DataEntity;
            //初始化工作流
            entity.SwitchToTargetWorkflow = "MainMvvmWorkflowLogic";
        }
	
		/// <summary>
        /// Unity Method
        /// </summary>
		void Update ()
		{
	
		}
	}
}

