
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CalenderTrigger
* 创建日期：2018-12-27 14:04:03
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    虚拟时间(消息触发器)
******************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example_015
{
    /// <summary>
    /// 触发器类
    /// </summary>
	public class CalenderTrigger : MonoBehaviour 
	{
        //每天时间周期
        public InputField inputDayHour;
        public InputField inputDayMinute;
        //倍速
        public InputField inputYear;
        public InputField inputMonth;
        public InputField inputDay;
        public InputField inputHour;
        public InputField inputMinute;
        public InputField inputSecond;
        //每天时间周期和倍速确定按钮
        public Button DayTimeBtn;
        public Button TimeSpeedBtn;
        public DateTime dateTime;
        private  VirtualTimeDataEntity entity;  
        
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            entity = (VirtualTimeDataEntity)GameObject.FindObjectOfType<VirtualTimeDataModel>().DataEntity;
            DayTimeBtn.onClick.AddListener(OnClick);
            TimeSpeedBtn.onClick.AddListener(OnValueChange);
        }

        /// <summary>
        /// 时间周期
        /// </summary>
        private void OnClick()
        {
            if (!string.IsNullOrEmpty(inputDayHour.text) && !string.IsNullOrEmpty(inputDayMinute.text))
            {
                dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,int.Parse(inputDayHour.text), int.Parse(inputDayMinute.text), 0);   
                entity.DayTime = dateTime;
            }
        }

        /// <summary>
        /// 倍速改变
        /// </summary>
        private void OnValueChange()
        {
           if(!string.IsNullOrEmpty(inputYear.text))
            {
                entity.AddYears = int.Parse(inputYear.text);
            }

            if (!string.IsNullOrEmpty(inputMonth.text))
            {
                entity.AddMonths = int.Parse(inputMonth.text);
            }

            if (!string.IsNullOrEmpty(inputDay.text))
            {
                entity.AddDays = int.Parse(inputDay.text);
            }

            if (!string.IsNullOrEmpty(inputHour.text))
            {
                entity.AddHours = double.Parse(inputHour.text);
            }

            if (!string.IsNullOrEmpty(inputMinute.text))
            {
                entity.AddMinutes = double.Parse(inputMinute.text);
            }

            if (!string.IsNullOrEmpty(inputSecond.text))
            {
                entity.AddSeconds = double.Parse(inputSecond.text);
            }             
            entity.IsTimeVelocity = !entity.IsTimeVelocity;                  
        }  
    }
}

