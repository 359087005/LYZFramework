
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
using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_25_Mvvm_SimpleTest
{
    public class SimpleTestEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 输入的答案
        /// </summary>
        [RestoreFireLogic]
        public string answerInputField = "";

        /// <summary>
        /// 分数
        /// </summary>
        [RestoreFireLogic]
        public string grade = "";

        /// <summary>
        /// 是否点击确认按钮
        /// </summary>
        [RestoreFireLogic]
        public int isSure = -1;

        /// <summary>
        /// 题目内容
        /// </summary>
        [RestoreFireLogic]
        public string question = "1+1";

        /// <summary>
        /// 题数
        /// </summary>
        [RestoreFireLogic]
        public string questionNum = "5";

        /// <summary>
        /// Dropdown
        /// </summary>
        [RestoreFireLogic]
        public int dropdownValue = 0;

        /// <summary>
        /// Scrollbar
        /// </summary>
        [RestoreFireLogic]
        public float scrollbarValue = 0;


        public bool isRight = false;
    }
}

