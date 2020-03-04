/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PhoneEntity
* 创建日期：2019-04-10 11:10:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    ///
    /// </summary>
	public class PhoneEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 当前的手机状态【0：待机 1：通话 2：不在服务器】
        /// </summary>
        public int currentState = 2;
        /// <summary>
        /// 手机所处的信号塔数据模型
        /// </summary>
        public int towerModelID;
        /// <summary>
        /// 手机的位置
        /// </summary>
        public Vector3 point;
        /// <summary>
        /// 与我保持通讯的手机
        /// </summary>
        public int withPhoen;
	}
}

