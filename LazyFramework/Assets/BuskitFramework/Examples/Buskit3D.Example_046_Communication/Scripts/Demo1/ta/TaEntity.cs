/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TaEntity
* 创建日期：2019-04-08 16:11:52
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.BusKit.Unity.Modules.DataWatch;
using Com.Rainier.Buskit3D;
using System.Collections.Generic;
using UnityEngine;

namespace Buskit3D.Example_045_IoCApplication
{
	/// <summary>
    ///
    /// </summary>
	public class TaEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 当前区域内的手机
        /// </summary>
        public WatchableList<int> currentExistList = new WatchableList<int>();

        /// <summary>
        /// 当前通讯手机
        /// </summary>
        public WatchableDictionary<int, int> currenUseDic = new WatchableDictionary<int, int>();
    }
}

