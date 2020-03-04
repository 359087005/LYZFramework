/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MiniMapItem
* 创建日期：2018-12-19 09:25:22
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：小地图上显示的可交互的Item元素
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 小地图数据实体载体
    /// </summary>
    public class MinniMapModel : DataModelBehaviour
    {
        /// <summary>
        /// 观察数据实体
        /// </summary>
        private void Awake()
        {            
            DataEntity = new MiniMapEntity();
            Watch(this);
        }
    }
}
