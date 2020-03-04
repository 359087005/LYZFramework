/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TweenEntity
* 创建日期：2019-01-14 14:42:42
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：动画数据实体
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_020_Tween
{
    /// <summary>
    /// 关键帧
    /// </summary>
    public struct TweenArgs
    {
        public Vector3 targetValue;
        public float tweenTime;
        public string tweenName;
    }
	
    /// <summary>
    /// 数据实体
    /// </summary>
	public class TweenEntity : BaseDataModelEntity 
	{

        /// <summary>
        /// 动画参数
        /// </summary>
        public TweenArgs tweenArgs;

        /// <summary>
        /// 动画编号
        /// </summary>
        public int number = 0;       
       

	}
}

