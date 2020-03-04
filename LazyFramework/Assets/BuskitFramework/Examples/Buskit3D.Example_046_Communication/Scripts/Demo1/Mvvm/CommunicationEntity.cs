/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommunicationEntity
* 创建日期：2019-04-09 14:00:18
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_045_IoCApplication
{
	/// <summary>
    ///
    /// </summary>
	public class CommunicationEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 拨电话
        /// </summary>
        public int ringUp = 0;
        /// <summary>
        /// 待机
        /// </summary>
        public int cellStandby = 0;
        /// <summary>
        /// 提示
        /// </summary>
        public string tip = "";
        /// <summary>
        /// 是否可以拨电话
        /// </summary>
        public bool isCanRingUp = false;
        /// <summary>
        /// 是否可以切换待机状态
        /// </summary>
        public bool isCellStandby = false;
        /// <summary>
        /// 拨打的电话号码
        /// </summary>
        public string phoneNumber = "";
    }
}

