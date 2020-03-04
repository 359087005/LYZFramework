/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： LeftEntity
* 创建日期：2019-03-15 08:59:09
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_31_Mvvm_UISystem1
{
	/// <summary>
    /// 选项卡的实体
    /// </summary>
	public class LeftItemEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 当前选中的选项卡ID
        /// </summary>
        public int tabID;
        /// <summary>
        /// 是否打开设置界面
        /// </summary>
        [RestoreFireLogic]
        public bool isOnSetting;
        /// <summary>
        /// 是否打开帮助界面
        /// </summary>
        [RestoreFireLogic]
        public bool isOnHelp;
        /// <summary>
        /// 是否打开游戏界面
        /// </summary>
        [RestoreFireLogic]
        public bool isOnGame;
    }
}

