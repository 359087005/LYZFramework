/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：实体类工具，用来查找一个GameObject上的实体对象、数据模型、业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace IoCAndTwoCommunication
{
    /// <summary>
    /// 旋转控制逻辑
    /// </summary>
    public class CommonLogic : LogicBehaviour
    {
        /// <summary>
        /// 实体工具(接口注入的好处在于仅仅暴露API,
        /// 被注入对象的字段和属性不会暴露，提高了被注入对象的使用安全性)
        /// </summary>
        [Inject]
        protected EntityUtils utilsEntity;

        /// <summary>
        /// 日志工具(接口注入的好处在于仅仅暴露API,
        /// 被注入对象的字段和属性不会暴露，提高了被注入对象的使用安全性)
        /// </summary>
        [Inject]
        protected LoggingUtils utilsLogging;

        /// <summary>
        /// 场景中所有舞者
        /// </summary>
        [Inject]
        protected ObjectPool<DataModelBehaviour> dataModels;

        /// <summary>
        /// 注入依赖的对象
        /// </summary>
        protected virtual void Start()
        {
            InjectService.InjectInto(this);
        }
    }
}


