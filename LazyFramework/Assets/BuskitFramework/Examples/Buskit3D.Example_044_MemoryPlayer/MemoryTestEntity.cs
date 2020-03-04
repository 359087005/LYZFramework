/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MemoryTestEntity
* 创建日期：2019-04-03 09:07:45
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.BusKit.Unity.Modules.DataWatch;
using Com.Rainier.Buskit3D;
using System;

namespace Buskit3D.Example_040_MemoryPlayer
{
    /// <summary>
    ///
    /// </summary>    
    public class MemoryTestEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// WatchableList
        /// </summary>
        public WatchableList<int> watchTableListInt = new WatchableList<int>();

        /// <summary>
        /// watchTableListFloat
        /// </summary>
        public WatchableList<float> watchTableListFloat = new WatchableList<float>();

        /// <summary>
        /// watchTableListStruct
        /// </summary>
        public WatchableList<ScoreStruct> watchTableListStruct = new WatchableList<ScoreStruct>();

        /// <summary>
        /// watchTableListStruct
        /// </summary>
        public WatchableList<ScoreClass> watchTableListClass = new WatchableList<ScoreClass>();

        /// <summary>
        /// watchTableDicInt
        /// </summary>
        public WatchableDictionary<int, int> watchTableDicInt = new WatchableDictionary<int, int>();

        /// <summary>
        /// watchTableDicFloat
        /// </summary>
        public WatchableDictionary<int, float> watchTableDicFloat = new WatchableDictionary<int, float>();

        /// <summary>
        /// watchTableDicStruct
        /// </summary>
        public WatchableDictionary<int, ScoreStruct> watchTableDicStruct = new WatchableDictionary<int, ScoreStruct>();

        /// <summary>
        /// watchTableDicClass
        /// </summary>
        public WatchableDictionary<int, ScoreClass> watchTableDicClass = new WatchableDictionary<int, ScoreClass>();
    }

    public struct ScoreStruct {
        public int score;
        public ScoreStruct(int score) {
            this.score = score;
        }
    }

    public class ScoreClass {
        public int score;
        public ScoreClass(int score)
        {
            this.score = score;
        }

        public override bool Equals(object obj)
        {
            return false;
        }
        public static new  bool ReferenceEquals(Object objA, Object objB)
        {
            return false;
        }
    }
}

