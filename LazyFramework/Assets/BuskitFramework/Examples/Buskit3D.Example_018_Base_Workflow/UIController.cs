/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： 
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 触发脚本
    /// </summary>
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        //业务逻辑数据模型
        WorkflowDataModel workModel;
        //工作流实体对象
        WorkflowEntity entity;

        /// <summary>
        /// 帮助UI对象
        /// </summary>
        public GameObject objHelp; 
        /// <summary>
        /// 设置UI对象
        /// </summary>
        public GameObject objSetting;
        /// <summary>
        /// 设置UI的实体
        /// </summary>
        public SetUIEntity setEntity;
        /// <summary>
        /// 帮助UI的实体
        /// </summary>
        public HelpUIEntity helpUIEntity;
        /// <summary>
        /// 显示文本的实体
        /// </summary>
        public ShowTextEntity showTextEntity;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            Instance = this;            
        }

        /// <summary>
        /// 初始化工作流
        /// </summary>
        public void Start()
        {
            workModel = FindObjectOfType<WorkflowDataModel>();
            entity = (WorkflowEntity)workModel.DataEntity;
            entity.SwitchToTargetWorkflow = "MaininterfaceWorkflowLogic";
            var model1 = objSetting.GetComponent<DataModelBehaviour>();
            setEntity = (SetUIEntity)model1.DataEntity;
            var model2 = objHelp.GetComponent<DataModelBehaviour>();
            helpUIEntity = (HelpUIEntity)model2.DataEntity;
            var trans = GameObject.Find("Context");
            var model3 = trans.GetComponent<DataModelBehaviour>();
            showTextEntity = (ShowTextEntity)model3.DataEntity;
        }

        /// <summary>
        ///点击设置按钮的监听
        /// </summary>
        public void OnClickOpenSetting()
        {
            entity = (WorkflowEntity)workModel.DataEntity;
            entity.SwitchToTargetWorkflow = "SettingWorkflowLogic";
            //setEntity.isShow = !setEntity.isShow;
        }

        /// <summary>
        ///点帮助按钮的监听
        /// </summary>
        public void OnClickOpenHelp()
        {
            entity = (WorkflowEntity)workModel.DataEntity;
            entity.SwitchToTargetWorkflow = "HelpWorkflowLogic";
            //helpUIEntity.isShow = !helpUIEntity.isShow;
        }

        /// <summary>
        /// 点击关闭设置按钮的监听 
        /// </summary>
        public void OnClickCloseSetting()
        {
            entity = (WorkflowEntity)workModel.DataEntity;
            entity.SwitchToTargetWorkflow = "MaininterfaceWorkflowLogic";
            var logic = workModel.GetComponent<SettingWorkflowLogic>();
            SettingWorkflowLogic settingWorkflowLogic = logic;
            settingWorkflowLogic.Execute();
        }

        /// <summary>
        /// 点击关闭设置按钮的监听
        /// </summary>
        public void OnClickCloseHelp()
        {
            entity = (WorkflowEntity)workModel.DataEntity;
            entity.SwitchToTargetWorkflow = "MaininterfaceWorkflowLogic";
            HelpWorkflowLogic helpWorkflow = workModel.GetComponent<HelpWorkflowLogic>();
            helpWorkflow.Execute();
            //helpUIEntity.isShow = !helpUIEntity.isShow;
        }
    }
}