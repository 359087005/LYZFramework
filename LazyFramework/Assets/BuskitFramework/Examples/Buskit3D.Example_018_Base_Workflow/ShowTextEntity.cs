/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ShowTextEntity
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 设置UI实体
    /// </summary>
    public class ShowTextEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 文本上下文
        /// </summary>
        [RestoreFireLogic] 
        public string context = "";
    }
}
