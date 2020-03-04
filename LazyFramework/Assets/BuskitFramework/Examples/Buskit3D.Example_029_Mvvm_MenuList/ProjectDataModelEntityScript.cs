/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProjectDataModelEntityScript
* 创建日期：2019-03-15 17:13:21
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.BusKit.Unity.Modules.DataWatch;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_033_MenuList
{
    [System.Serializable]
    public class ProjectDataModelEntityScript : BaseDataModelEntity 
	{
        /// <summary>
        /// 新建
        /// </summary>
        public int newButton;
        /// <summary>
        /// 保存
        /// </summary>
        public int saveButton;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool isSelect = false;
        /// <summary>
        /// 输入框内容
        /// </summary>
        public string inputFileContent;
        /// <summary>
        /// 是否打开面板
        /// </summary>
        public int isOpenMenu;
    }

    public class MenuItemEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int menuID;
        /// <summary>
        /// 读取按钮
        /// </summary>
        public int readButton;
        /// <summary>
        /// 删除按钮
        /// </summary>
        public int delButton;
        /// <summary>
        /// 方案名字
        /// </summary>
        public string itemName;
    }
}

