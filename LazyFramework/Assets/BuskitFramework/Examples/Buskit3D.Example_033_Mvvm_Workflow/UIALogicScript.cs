/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIALogicScript
* 创建日期：2019-03-20 14:46:37
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
	public class UIALogicScript : LogicBehaviour 
	{
		public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("close"))
            {
                if (evt.NewValue.ToString().Equals("0")) return;
                //返回默认工作流
                var _wfModel = FindObjectOfType<WorkflowDataModel>();
                var entity = (WorkflowEntity)_wfModel.DataEntity;
                //初始化工作流
                entity.SwitchToTargetWorkflow = "MainMvvmWorkflowLogic";
            }
        }		
	}
}

