/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： WfMainLogic
* 创建日期：2019-03-20 14:09:38
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
	public class WfMainLogic : LogicBehaviour 
	{
		public override void ProcessLogic(PropertyMessage evt)
        {            

            switch (evt.PropertyName) {

                case "UIA":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //Debug.Log("UIA");
                    //执行工作流A
                    var _wfModel = FindObjectOfType<WorkflowDataModel>();
                    var entity = (WorkflowEntity)_wfModel.DataEntity;
                    //初始化工作流
                    entity.SwitchToTargetWorkflow = "UIAMvvmWorkflowLogic";
                    break;

                case "UIB":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //执行工作流B
                    _wfModel = FindObjectOfType<WorkflowDataModel>();
                    entity = (WorkflowEntity)_wfModel.DataEntity;
                    //初始化工作流
                    entity.SwitchToTargetWorkflow = "UIBMvvmWorkflowLogic";
                    break;

                case "UIC":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //执行工作流C
                    _wfModel = FindObjectOfType<WorkflowDataModel>();
                    entity = (WorkflowEntity)_wfModel.DataEntity;
                    //初始化工作流
                    entity.SwitchToTargetWorkflow = "UICMvvmWorkflowLogic";
                    break;

                case "UID":
                    if (evt.NewValue.ToString().Equals("0")) return;
                    //执行工作流D
                    _wfModel = FindObjectOfType<WorkflowDataModel>();
                    entity = (WorkflowEntity)_wfModel.DataEntity;
                    //初始化工作流
                    entity.SwitchToTargetWorkflow = "UIDMvvmWorkflowLogic";
                    break;

            }
        }		
	}
}

