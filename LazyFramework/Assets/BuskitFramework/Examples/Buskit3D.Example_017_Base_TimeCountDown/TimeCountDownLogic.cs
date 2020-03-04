/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：倒计时(业务逻辑)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using System;

namespace Com.Rainier.Buskit3D.Example_017
{
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class TimeCountDownLogic : LogicBehaviour
    {
        //保存倒计时总秒数
        private float TimeSecond;
        //计时显示框
        private Text timeText;
        //数据实体对象
        private TimeCountDownDataEntity entity;
        //
        private bool isStart = false;
        
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            entity = (TimeCountDownDataEntity)GameObject.FindObjectOfType<TimeCountDownDataModel>().DataEntity;
            timeText = this.transform.Find("CountDownShowPanel/NumType").GetComponent<Text>();
            timeText.text = "0:00";
        }

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //显示类型
            if (evt.PropertyName.Equals("isStart"))
            {
                if ((bool)evt.NewValue)
                {
                    TimeSecond = entity.Second;
                    isStart = entity.isStart;
                    this.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "暂停计时";
                }
                else
                {
                    this.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "开始计时";
                    isStart = false;
                    entity.Second = Convert.ToSingle(timeText.text); 
                }
            }
        }

           /// <summary>
            /// Unity Method
            /// </summary>
        private void Update()
        {
            if(isStart)
            {
                if (TimeSecond <= 0)
                {
                    timeText.text = "0:00";
                    isStart = false;
                }
                else
                {
                    //保留两位小数
                    timeText.text = TimeSecond.ToString("#0.00");
                    TimeSecond -= Time.deltaTime;
                    if(TimeSecond <= 0)
                    {
                        timeText.text = "0:00";
                        isStart = false;
                    }
                }
            }           
        }
    }
}

