/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： VirtualTimeEntity
* 创建日期：2019-03-15 17:12:55
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using System;

namespace Buskit3D.Example_34_Mvvm_VirtualTime
{
	/// <summary>
    ///虚拟时间数据实体类
    /// </summary>
	public class VirtualTimeEntity : BaseDataModelEntity 
	{
        //DataTime
        [RestoreFireLogic]
        public DateTime dataTime;

        //时间
        [RestoreFireLogic]
        public string dataTimeText = "";

        //小时
        [RestoreFireLogic]
        public string inputFildHour = "";
        //分钟
        [RestoreFireLogic]
        public string inputFildMinute = "";
        //确认按钮
        [RestoreFireLogic]
        public int sureBtnTop = 0;

        /// <summary>
        /// 时间流速
        /// </summary>
        //年
        [RestoreFireLogic]
        public string inputFildY = "";
        //月
        [RestoreFireLogic]
        public string inputFildM = "";
        //日
        [RestoreFireLogic]
        public string inputFildD= "";
        //时
        [RestoreFireLogic]
        public string inputFildh = "";
        //分
        [RestoreFireLogic]
        public string inputFildm = "";
        //秒
        [RestoreFireLogic]
        public string inputFilds = "1";
        //确认按钮
        [RestoreFireLogic]
        public int sureBtnDown = 0;
    }
}

