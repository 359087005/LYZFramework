/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：UIShowPlayVideosInterfaceDataModel
* 创建日期：2018-1-11 13:36:42
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：事件分发器类
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/


namespace Com.Rainier.Buskit3D.Example_14_PlayVideos
{
    /// <summary>
    /// 事件分发器类
    /// </summary>
    public class UIShowPlayVideosInterfaceDataModel : DataModelBehaviour
	{
        /// <summary>
        /// 观察实体
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new UIShowPlayVideosInterfaceDataModelEntity();
            Watch(this);
        }
    }
}

