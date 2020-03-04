/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： HelpPanellEntity
* 创建日期：2019-03-15 08:56:07
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
	/// <summary>
    ///帮助面板数据实体类
    /// </summary>
	public class HelpPanellEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// Dropdow
        /// </summary>
        [RestoreFireLogic]
        public int dropdownValue = 0;

        /// <summary>
        /// 显示Text
        /// </summary>
        [RestoreFireLogic]
        public string showText = "";
    }
}

