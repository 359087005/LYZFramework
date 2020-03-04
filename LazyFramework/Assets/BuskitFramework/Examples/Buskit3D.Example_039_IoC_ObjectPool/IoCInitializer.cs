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

namespace Buskit3D.Training.IoC.C
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
            //注册灯对象池
            if (InjectService.Get<ObjectPool<LightDataModel>>() == null)
            {
                InjectService.RegisterSingleton(new ObjectPool<LightDataModel>());
            }

            //注册舞者对象池
            if (InjectService.Get<ObjectPool<DancerDataModel>>() == null)
            {
                InjectService.RegisterSingleton(new ObjectPool<DancerDataModel>());
            }

            //注册实体工具
            if (InjectService.Get<IEntityService>() == null)
            {
                InjectService.RegisterSingleton<IEntityService>(new EntityServiceImpl());
            }

            //注册Logging工具
            if (InjectService.Get<ILoggingService>() == null)
            {
                InjectService.RegisterSingleton<ILoggingService>(new LoggingServiceImpl());
            }
        }
    }
}

