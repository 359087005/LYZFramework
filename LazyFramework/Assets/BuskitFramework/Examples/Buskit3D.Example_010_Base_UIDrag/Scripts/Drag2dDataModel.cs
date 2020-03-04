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

using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_010_UIDrag
{
    /// <summary>
    /// 鼠标拖动2D（实现鼠标开始拖动UI接口）
    /// </summary>
    public class Drag2dDataModel : DataModelBehaviour
    {
        /// <summary>
        /// 观察实体
        /// </summary>
        private void Awake()
        {
            Drag2dDataEntity entity = new Drag2dDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }
    }
}

