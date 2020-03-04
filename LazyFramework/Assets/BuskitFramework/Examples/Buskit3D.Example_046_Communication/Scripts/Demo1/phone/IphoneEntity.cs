/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IphoneEntity
* 创建日期：2019-04-08 16:05:08
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
	public class IphoneEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 所在的区域ID[-1表示不在服务区]
        /// </summary>
        public int areaID = -1;
        /// <summary>
        /// 当前状态[0:待机  1：去电  2：链接]
        /// </summary>
        public int currentState;
        /// <summary>
        /// 目标手机
        /// </summary>
        public int goldIphone;
	}
}

