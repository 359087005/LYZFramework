/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TweenData
* 创建日期：2019-02-12 10:21:43
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System.Collections.Generic;
using UnityEngine;
namespace Buskit3D.Example_020_Tween
{
    /// <summary>
    /// 节点数据
    /// </summary>
    public class TweenData : MonoBehaviour 
	{
        /// <summary>
        /// 存储所有的Transform变化节点
        /// </summary>

        public List<AllData> Data = new List<AllData>();
#if UNITY_EDITOR
        //临时数据
        public List<TransData> pointData = new List<TransData>();
#endif

        public List<TransData> GetPath(int index)
        {
            return  new List<TransData>(Data[index].data);
        }
        public TransData GetTransData(int pathIndex,int index)
        {
            return Data[pathIndex].data[index];
        }
        public Vector3 GetPosition(int pathIndex, int index)
        {
            return Data[pathIndex].data[index].localPosition;
        }
        public Vector3 GetAngles(int pathIndex, int index)
        {
            return Data[pathIndex].data[index].localAngles;
        }
        public Vector3 GetScale(int pathIndex, int index)
        {
            return Data[pathIndex].data[index].localScale;
        }
    }
    /// <summary>
    /// 节点去不路径数据
    /// </summary>
    [System.Serializable]
    public class AllData
    {
        public List<TransData> data = new List<TransData>();
    }

    /// <summary>
    /// 节点数据
    /// </summary>
    [System.Serializable]
    public struct TransData
    {
        public Vector3 localPosition;
        public Vector3 localAngles;
        public Vector3 localScale;
    }
}

