/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SphereTweenDataModel
* 创建日期：2019-01-14 17:15:35
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Example_020_Tween
{
    /// <summary>
    /// 球体数据载体
    /// </summary>
    [RequireComponent(typeof(SphereTweenLogic))]
    public class SphereTweenDataModel : TweenDataModel
	{
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            TweenEntity data = new TweenEntity();
            DataEntity = data;
            Watch(this);
        }
       

    }
}

