/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TowerEntity
* 创建日期：2019-04-10 10:42:00
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    /// 塔的数据实体
    /// </summary>
	public class TowerEntity : BaseDataModelEntity
	{
        /// <summary>
        /// 信号塔的位置
        /// </summary>
        public string towerPoint;

        /// <summary>
        /// 刷新
        /// </summary>
        public int reflush = 0;

        /// <summary>
        /// 信号塔范围内的手机
        /// </summary>
        [RestoreFireLogic]
        public WatchableList<int> phoneList = new WatchableList<int>();

        /// <summary>
        /// 信号塔范围内的通讯手机
        /// </summary>
        [RestoreFireLogic]
        public WatchableList<int> phoneUseList = new WatchableList<int>();

        /// <summary>
        /// 信号塔的链路关系
        /// </summary>
        [RestoreFireLogic]
        public WatchableList<int> towerList = new WatchableList<int>();
    }
}