/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-14 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：虚拟时间(业务逻辑)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_015
{
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class VirtualTimeDataLogic : LogicBehaviour
    {
        /// <summary>
        /// 时间日期文本
        /// </summary>
        Text DateText;
        /// <summary>
        /// 时间日期数据
        /// </summary>
        DateTime datetime;
        /// <summary>
        /// 时间流速
        /// </summary>
        double  SecondVelocity = 1;
        double  MinuteVelocity = 0;
        double  HourVelocity = 0;
        double  DayVelocity = 0;
        int MonthVelocity = 0;
        int YearVelocity = 0;
        /// <summary>
        /// 以秒做单位的计时器_
        /// </summary>
        float Timer;
        private VirtualTimeDataEntity _dataEntity;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            _dataEntity = (VirtualTimeDataEntity)GameObject.FindObjectOfType<VirtualTimeDataModel>().DataEntity;
           datetime = DateTime.Now;
            DateText = GetComponentInChildren<Text>();
            DateText.text = datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("DayTime"))
            {
                Debug.Log("DayTime");
                datetime = (DateTime)evt.NewValue;
            }
            if (evt.PropertyName.Equals("IsTimeVelocity"))
            {
                YearVelocity = _dataEntity.AddYears;
                MonthVelocity = _dataEntity.AddMonths;
                DayVelocity = _dataEntity.AddDays;
                HourVelocity = _dataEntity.AddHours;
                MinuteVelocity = _dataEntity.AddMinutes;
                SecondVelocity = _dataEntity.AddSeconds;
            }
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > (1))
            {
                datetime = datetime.AddSeconds(1 * SecondVelocity);
                datetime = datetime.AddMinutes(1 * MinuteVelocity);
                datetime = datetime.AddHours(1 * HourVelocity);
                datetime = datetime.AddDays(1 * DayVelocity);
                datetime = datetime.AddMonths(1 * MonthVelocity);
                datetime = datetime.AddYears(1 * YearVelocity);
                DateText.text = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                Timer = 0;
            }
        }
    }
}

