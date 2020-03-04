/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：Binding
* 创建日期：2019-0-07 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：数据实体类
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_25_Mvvm_SimpleTest
{
    public class SimpleTestViewModel : ViewModelBehaviour
    {
        /// <summary>
        /// 设置resultInputField与Entity中answerInputField的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "answerInputField")]
        public ViewBehaviour<string> resultInputField;

        /// <summary>
        /// 设置gradeText与Entity中grade的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "grade")]
        public ViewBehaviour<string> gradeText;

        /// <summary>
        /// 设置sureButton与Entity中isSure的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "isSure")]
        public ButtonView sureButton;

        /// <summary>
        /// 设置questionText与Entity中question的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "question")]
        public TextView questionText;

        /// <summary>
        /// 设置numText与Entity中questionNum的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "questionNum")]
        public ViewBehaviour<string> numText;

        /// <summary>
        /// 设置dropdown与Entity中dropdownValue的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "dropdownValue")]
        public ViewBehaviour<int> dropdown;

        /// <summary>
        /// 设置dropdown与Entity中dropdownValue的映射关系
        /// </summary>
        [Binding(EntityPropertyName = "scrollbarValue")]
        public ViewBehaviour<float> scrollbar;

        /// <summary>
        /// 执行绑定过程
        /// </summary>
        protected override void Awake()
        {
            //搜索View组件

            //结果输入
            resultInputField = GameObject.Find("Canvas/Panel/ResultInputField").GetComponent<InputFieldView>();
            //分数
            gradeText        = GameObject.Find("Canvas/Panel/GradeText").GetComponent<TextView>();
           
            //确认按钮
            sureButton       = GameObject.Find("Canvas/Panel/SureButton").GetComponent<ButtonView>();
        
            //题目内容
            questionText     = GameObject.Find("Canvas/Panel/ItemText").GetComponent<TextView>();

            //题目数量
            numText = GameObject.Find("Canvas/Panel/NumText").GetComponent<TextView>();

            //下拉列表
            dropdown = GameObject.Find("Canvas/Panel/Dropdown").GetComponent<DropdownView>();

            //滑动列表
            scrollbar = GameObject.Find("Canvas/Panel/Scrollbar").GetComponent<ScrollbarView>();

            //实例化DataEntity
            this.DataEntity = new SimpleTestEntity();

            //在父类的Awake函数中执行绑定过程
            base.Awake();
        }
    }
}
