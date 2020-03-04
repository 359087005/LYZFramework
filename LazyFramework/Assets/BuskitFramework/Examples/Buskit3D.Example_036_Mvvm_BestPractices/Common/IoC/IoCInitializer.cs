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
using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_036_Mvvm_BestPractices
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
            //初始化时先清空所有注册实例
            InjectService.UnregisterAll();

            //注册实体工具
            InjectService.RegisterSingleton(new EntityUtils());

            //注册Logging工具
            InjectService.RegisterSingleton(new LoggingUtils());

            //注册DataModel对象池
            InjectService.RegisterSingleton(new ObjectPool<CommonDataModel>());

            //注册ViewModel对象池
            InjectService.RegisterSingleton(new ObjectPool<CommonViewModel>());
        }
    }
}

