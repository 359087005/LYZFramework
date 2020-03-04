/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：MaininterfaceWorkflowLogic
* 创建日期：2018-12-19 16:29:25
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：MaininterfaceWorkflowLogic
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 主UI的业务流
    /// </summary>
    public class MaininterfaceWorkflowLogic : WorkflowLogic
    {
        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            /// <summary>
            /// 为工作流赋值[个人建议直接使用类名]
            /// </summary>
            this.WorkflowTagName = "MaininterfaceWorkflowLogic";
        }

        /// <summary>
        /// 处理业务逻辑进入
        /// </summary>
        public override void OnEnter()
        {
           // UnityEngine.Debug.Log("主UI进入：MainWorkflowLogic:OnEnter");
           UIController.Instance.showTextEntity.context += "\n主UI进入的操作";
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        public override void Execute()
        {
            //Debug.Log("主UI执行：MainWorkflowLogic:Execute");
            UIController.Instance.showTextEntity.context += "\n主UI执行关闭的操作";
        }

        /// <summary>
        /// 处理业务逻辑退出
        /// </summary>
        public override void OnExit()
        {
            //Debug.Log("主UI离开：MainWorkflowLogic:OnExit");
            UIController.Instance.showTextEntity.context += "\n主UI离开的操作";
        }
    }
}