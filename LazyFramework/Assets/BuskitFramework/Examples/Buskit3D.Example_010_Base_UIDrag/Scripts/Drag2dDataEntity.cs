/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SenceADataModel
* 创建日期：2018-12-26 15:53:26
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    2D拖拽
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_010_UIDrag
{
    /// <summary>
    /// 拖拽消息数据结构
    /// </summary>
    public struct DragUIStr
    {
        /// <summary>
        /// 是否拖拽UI
        /// </summary>
        public bool isDragUI;
        /// <summary>
        /// 老的坐标
        /// </summary>
        public Vector2 oldPosition;
        /// <summary>
        /// 新的坐标
        /// </summary>
        public Vector2 newPosition;
        /// <summary>
        /// 当前拖拽对象ID
        /// </summary>
        public int objectID;
    }

    /// <summary>
    /// 数据实体类
    /// </summary>
    public class Drag2dDataEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 拖拽消息
        /// </summary>
        public DragUIStr DragUIMessage = new DragUIStr();
    }
}

