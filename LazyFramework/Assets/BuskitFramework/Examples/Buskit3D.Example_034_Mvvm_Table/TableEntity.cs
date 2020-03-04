/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：RowTitleEntity
* 创建日期：2019-04-01 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.DataWatch;

namespace Buskit3D.Example_032_Mvvm_Table
{
    public class TableEntity : BaseDataModelEntity
    {
        //不记录数据变化事件（仅仅在运行阶段有效）
        [NoStorage]
        public WatchableList<RowDataEntity> rowData = new WatchableList<RowDataEntity>();

        //当数据恢复结束后修改为true，通知更新显示
        [RestoreFireLogic]
        public bool isLoaded = false;
    }
}

