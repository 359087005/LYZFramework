/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：容器类功能的实体类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.BusKit.Unity.Modules.DataWatch;
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_004_PanelClass
{
    /// <summary>
    /// 容器类的实体
    /// </summary>
    [System.Serializable]
    public class PanelClassEntity : BaseDataModelEntity
    {
        /// <summary>
        /// watch封装的字典
        /// </summary>
        [RestoreFireLogic]
        public WatchableDictionary<int, string> dataDic = new WatchableDictionary<int, string>();
        /// <summary>
        /// watch封装的动态数组
        /// </summary>
        [RestoreFireLogic]
        public WatchableArrayList dataArrayList = new WatchableArrayList();
        /// <summary>
        /// watch封装的集合
        /// </summary>
        [RestoreFireLogic]
        public WatchableList<int> dataList = new WatchableList<int>();
    }
}