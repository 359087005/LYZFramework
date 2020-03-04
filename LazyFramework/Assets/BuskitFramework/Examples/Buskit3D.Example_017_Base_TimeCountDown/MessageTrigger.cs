/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-09 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：倒计时(消息触发器)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Com.Rainier.Buskit3D.Example_017
{
    /// <summary>
    /// 触发器类
    /// </summary>
    public class MessageTrigger : MonoBehaviour
    {
        //输入文本框对象
        public InputField inputField;
        //实体对象
        private TimeCountDownDataEntity entity;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            entity = (TimeCountDownDataEntity)GameObject.FindObjectOfType<TimeCountDownDataModel>().DataEntity;
            inputField.onEndEdit.AddListener(value =>InputFieldCallBack(value));
        }

        /// <summary>
        /// InputField回调函数
        /// </summary>
        /// <param name="text"></param>
        private void InputFieldCallBack(string text)
        {
            entity.Second = Convert.ToSingle(text);
        }
        /// <summary>
        /// 点击开始
        /// </summary>
        public void OnClickStart()
        {
            //给时间实体赋值
            if (inputField.text != "")
            {
                entity.isStart = !entity.isStart;
            }
        }
    }
}

