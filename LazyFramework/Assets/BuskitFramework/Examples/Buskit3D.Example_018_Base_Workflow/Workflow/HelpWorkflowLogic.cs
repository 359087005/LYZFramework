/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：HelpWorkflowLogic
* 创建日期：2018-12-19 16:29:25
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：帮助的工作流
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// HelpWorkflowLogic帮助界面的业务流
    /// </summary>
    public class HelpWorkflowLogic : WorkflowLogic
    {
        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            /// <summary>
            /// 为工作流赋值[个人建议直接使用类名]
            /// </summary>
            this.WorkflowTagName = "HelpWorkflowLogic";
        }

        /// <summary>
        /// 处理业务逻辑进入
        /// </summary>
        public override void OnEnter()
        {
            //Debug.Log("HelpWorkflowLogic:OnEnter");
            UIController.Instance.showTextEntity.context += "\n帮助界面进入的操作";
            UIController.Instance.objHelp.transform.GetChild(0).gameObject.SetActive(true);
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        public override void Execute()
        {
            //Debug.Log("HelpWorkflowLogic:Execute");
            UIController.Instance.showTextEntity.context += "\n帮助界面执行关闭的操作";
            UIController.Instance.objHelp.transform.GetChild(0).gameObject.SetActive(false);
        }

        /// <summary>
        /// 处理业务逻辑退出
        /// </summary>
        public override void OnExit()
        {
            //Debug.Log("HelpWorkflowLogic:OnExit");
            UIController.Instance.showTextEntity.context += "\n帮助界面离开的操作";
            UIController.Instance.objHelp.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
