/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-14 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：虚拟时间(数据实体)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using System;
namespace Com.Rainier.Buskit3D.Example_015
{
    /// <summary>
    /// 数据实体类
    /// </summary>
    public class VirtualTimeDataEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 日常时间控制
        /// </summary>
       [RestoreFireLogic]
        public DateTime DayTime;

        /// <summary>
        /// 时间流速控制
        /// </summary>
         [RestoreFireLogic]
        public int AddYears = 0;
        [RestoreFireLogic]
        public int AddMonths = 0;
        [RestoreFireLogic]
        public double AddDays = 0;
        [RestoreFireLogic]
        public double AddHours = 0;
        [RestoreFireLogic]
        public double AddMinutes = 0;
        [RestoreFireLogic]
        public double AddSeconds = 1;

        /// <summary>
        /// 时间流速设定确认控制
        /// </summary>
        [RestoreFireLogic]
        public bool IsTimeVelocity = false;
    }
}

