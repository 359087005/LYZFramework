/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MainWorkflowLogic
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using System.Collections.Generic;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

namespace Com.Rainier.Buskit3D.Example_009_Backpack
{
    /// <summary>
    ///主UI工作流
    /// </summary>
    public class MainWorkflowLogic : WorkflowLogic
    {
        /// <summary>
        /// 初始化工作流名字
        /// </summary>
        private void Awake()
        {
            this.WorkflowTagName = "MainWorkflowLogic";
        }

        /// <summary>
        /// 处理业务逻辑进入
        /// </summary>
        public override void OnEnter()
        {

        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        public override void Execute()
        {
        }

        /// <summary>
        /// 处理业务逻辑退出
        /// </summary>
        public override void OnExit()
        {
        }
    }
}