/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestTaEntity
* 创建日期：2019-04-17 10:59:23
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

namespace IoCAndTwoCommunication
{   
	/// <summary>
    /// 
    /// </summary>
	public class TestTaEntity : BaseDataModelEntity 
	{
        public WatchableList<TestQiuEntity> testQiuEntityList = new WatchableList<TestQiuEntity>();

        /// <summary>
        /// 呼叫列表
        /// </summary>
        //public int beihuijiao = 0;
        [NoStorage]
        public WatchableList<int> callList = new WatchableList<int>();
    }
}

