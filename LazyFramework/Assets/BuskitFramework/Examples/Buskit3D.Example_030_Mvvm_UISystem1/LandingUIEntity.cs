/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LandingUIEntity
* 创建日期：2019-03-14 10:32:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{

    public enum LeftItemType:int
    {
        设置 = 0,
        帮助 = 1,
        玩游戏 = 2
    }
	/// <summary>
    ///LandingUI数据实体类
    /// </summary>
	public class LandingUIEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 题目文本框
        /// </summary>
        [RestoreFireLogic]
        public string questionText = Random.Range(1, 100) + "+" + Random.Range(1, 100);

        /// <summary>
        /// 答案文本框
        /// </summary>
        [RestoreFireLogic]
        public string inputFieldText = "";

        /// <summary>
        /// 确认按钮
        /// </summary>
        [RestoreFireLogic]
        public int landingBtn = 0;

        /// <summary>
        /// 退出按钮
        /// </summary>
        [RestoreFireLogic]
        public int escBtn = 0;
    }
}

