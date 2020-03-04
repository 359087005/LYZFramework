/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ToggleEntityScript
* 创建日期：2019-03-15 11:35:10
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
	public class ToggleEntityScript : BaseDataModelEntity 
	{
        /// <summary>
        /// 
        /// </summary>
        [RestoreFireLogic]
        public bool isZeroON;
        [RestoreFireLogic]
        public bool isOneON;
        [RestoreFireLogic]
        public bool isTwoON;
        [RestoreFireLogic]
        public string content;
    }
}

