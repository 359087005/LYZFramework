/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingLinkEntity
* 创建日期：2019-04-11 09:52:28
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    ///
    /// </summary>
	public class SettingLinkEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 信号塔的名字
        /// </summary>
        public int dropdown = -1;
        /// <summary>
        /// 保存的链路信息
        /// </summary>
        public string linkList = "";
        /// <summary>
        /// 确定按钮
        /// </summary>
        public int btnSure;

        public bool isShow = false;
    }
}

