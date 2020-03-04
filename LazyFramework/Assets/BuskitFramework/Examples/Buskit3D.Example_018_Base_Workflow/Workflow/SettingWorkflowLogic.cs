/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingWorkflowLogic
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 设置界面的工作流
    /// </summary>
    public class SettingWorkflowLogic : WorkflowLogic
    {
        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            /// <summary>
            /// 为工作流赋值[个人建议直接使用类名]
            /// </summary>
            this.WorkflowTagName = "SettingWorkflowLogic";
        }

        /// <summary>
        /// 处理业务逻辑进入
        /// </summary>
        public override void OnEnter()
        {
            //Debug.Log("SettingWorkflowLogic:OnEnter");
            UIController.Instance.showTextEntity.context += "\n设置界面进入的操作";
            UIController.Instance.objSetting.transform.GetChild(0).gameObject.SetActive(true);
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        public override void Execute()
        {
            //Debug.Log("SettingWorkflowLogic:Execute");
            UIController.Instance.showTextEntity.context += "\n设置界面执行关闭的操作";
            UIController.Instance.objSetting.transform.GetChild(0).gameObject.SetActive(false);
        }

        /// <summary>
        /// 处理业务逻辑退出
        /// </summary>
        public override void OnExit()
        {
            //Debug.Log("SettingWorkflowLogic:OnExit");
            UIController.Instance.showTextEntity.context += "\n设置界面离开的操作";
            UIController.Instance.objSetting.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}