/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： BagEntity
* 创建日期：2019-03-21 09:22:03
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_36_Mvvm_Bag
{
	/// <summary>
    ///
    /// </summary>
	public class BagEntity : BaseDataModelEntity 
	{
        /// <summary>
        /// 当前选项卡的ID
        /// </summary>
        [RestoreFireLogic]
        public int tabID = -1;
        /// <summary>
        /// 当前选中的Item的ID
        /// </summary>
        [RestoreFireLogic]
        public int itemID;
        /// <summary>
        /// 搜索框的内容
        /// </summary>
        [RestoreFireLogic]
        public string inputFieldContent;
        /// <summary>
        /// 按钮触发
        /// </summary>
        [RestoreFireLogic]
        public int buttonView;
        /// <summary>
        /// 触发初始化开关
        /// </summary>
        [RestoreFireLogic]
        public int initBag = 0;

    }
}

