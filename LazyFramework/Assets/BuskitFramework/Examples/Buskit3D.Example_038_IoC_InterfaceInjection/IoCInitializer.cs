/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：依赖注入初始化器
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine;

namespace Buskit3D.Training.IoC.B
{
    /// <summary>
    /// 依赖注入初始化器
    /// </summary>
    public class IoCInitializer : MonoBehaviour
    {
        /// <summary>
        /// 执行初始化过程
        /// </summary>
        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// 执行初始化过程
        /// </summary>
        //[RuntimeInitializeOnLoadMethod]
        public void Initialize()
        {
            //注册实体工具
            if (InjectService.Get<IEntityService>() == null)
            {
                InjectService.RegisterSingleton<IEntityService>(new EntityServiceImpl());
                
            }

            //注册Logging工具
            if (InjectService.Get<ILoggingService>() == null)
            {
                InjectService.RegisterSingleton<ILoggingService>(new LoggingServiceImplForWebGL());
                //InjectService.RegisterSingleton<ILoggingService>(new LoggingServiceImplForWindows());
                //ILoggingService具备2个版本的实现类,其他MonoBehavior中注入的是IEntityService接口，
                //在不同条件下使用不同的接口实现，只需要在这里注册不同的接口实现，注入端不需要修改任何代码。
            }
        }
    }
}

