/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： VirtualTimeLogic
* 创建日期：2019-03-15 17:13:16
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_34_Mvvm_VirtualTime
{   
	/// <summary>
    /// 虚拟时间业务逻辑处理类
    /// </summary>
	public class VirtualTimeLogic : LogicBehaviour 
	{
        /// <summary>
        /// 时间
        /// </summary>
        private DateTime datetime;

        /// <summary>
        /// 实体对象
        /// </summary>
        private VirtualTimeEntity entity;

        /// <summary>
        /// 以秒做单位的计时器_
        /// </summary>
        private float Timer;

        public Text tiemText;

        /// <summary>
        /// 时间流速
        /// </summary>
        private int SecondV = 1;
        private int MinuteV = 0;
        private int HourV = 0;
        private int DayV = 0;
        private int MonthV = 0;
        private int YearV = 0;
 
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            entity = (VirtualTimeEntity)GetComponent<VirtualTimeDataModel>().DataEntity;
            datetime = DateTime.Now;
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > (1))
            {              
                datetime = datetime.AddYears(1 * YearV);
                datetime = datetime.AddMonths(1 * MonthV);
                datetime = datetime.AddDays(1 * DayV);
                datetime = datetime.AddHours(1 * HourV);
                datetime = datetime.AddMinutes(1 * MinuteV);
                datetime = datetime.AddSeconds(1* SecondV);
                tiemText.text = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                Timer = 0;
            }
        }

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //收到点击SureButtonTop按钮消息后逻辑处理
            if (evt.PropertyName.Equals("sureBtnTop"))
            {
                if((int)evt.NewValue > 0)
                {
                    datetime = entity.dataTime;
                }                                       
            }
            //收到点击SureButtonDown按钮消息后逻辑处理
            if (evt.PropertyName.Equals("sureBtnDown"))
            {
                if ((int)evt.NewValue > 0)
                {
                    VirtualTimeEntity entity = (VirtualTimeEntity)GetComponent<VirtualTimeDataModel>().DataEntity; 
                    //容错防空处理
                    if(!string.IsNullOrEmpty(entity.inputFildY))
                    {
                        YearV = int.Parse(entity.inputFildY);
                    }
                    if (!string.IsNullOrEmpty(entity.inputFildM))
                    {
                        MonthV = int.Parse(entity.inputFildM);
                    }
                    if (!string.IsNullOrEmpty(entity.inputFildD))
                    {
                        DayV = int.Parse(entity.inputFildD);
                    }
                    if (!string.IsNullOrEmpty(entity.inputFildh))
                    {
                        HourV = int.Parse(entity.inputFildh);
                    }
                    if (!string.IsNullOrEmpty(entity.inputFildm))
                    {
                        MinuteV = int.Parse(entity.inputFildm);
                    }
                    if (!string.IsNullOrEmpty(entity.inputFilds))
                    {
                        SecondV = int.Parse(entity.inputFilds);
                    }
                }
            }
        }
    }
}

