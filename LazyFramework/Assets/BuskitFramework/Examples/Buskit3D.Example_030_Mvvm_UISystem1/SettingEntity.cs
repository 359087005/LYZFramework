/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SettingEntity
* 创建日期：2019-03-15 15:02:28
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_29_Mvvm_Toggle
{
	/// <summary>
    ///
    /// </summary>
	public class SettingEntity : BaseDataModelEntity
	{
        /// <summary>
        /// 是否使用中文
        /// </summary>
        [RestoreFireLogic]
        public bool isChinese =true;

        /// <summary>
        /// 是否使用英文
        /// </summary>
        [RestoreFireLogic]
        public bool isEnglish =false;

        /// <summary>
        /// 声音
        /// </summary>
        [RestoreFireLogic]
        public float voice;
    }
}

