/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： HelpUIModel
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 帮助UI的实体模型
    /// </summary>
    public class HelpUIModel : DataModelBehaviour
    {
        /// <summary>
        /// 监听实体
        /// </summary>
        void Awake()
        {
            HelpUIEntity helpUIEntity = new HelpUIEntity();
            this.DataEntity = helpUIEntity;
            Watch(this);
        }
    }
}