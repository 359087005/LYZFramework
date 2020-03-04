/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：依赖注入初始化器
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_046_Communication
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
            ///命令系统注入
            if (InjectService.Get<IServiceCommand>() == null)
            {
                ServiceCommand commandService = new ServiceCommand();
                commandService.Initialize();
            }

            //注册实体工具
            if (InjectService.Get<IEntityService>() == null)
            {
                InjectService.RegisterSingleton<IEntityService>(new EntityServiceImpl());
            }

            //注册信号塔对象池
            if (InjectService.Get<ObjectPool<TowerModel>>() == null)
            {
                InjectService.RegisterSingleton(new ObjectPool<TowerModel>());
            }

            //注册手机对象池
            if (InjectService.Get<ObjectPool<PhoneModel>>() == null)
            {
                InjectService.RegisterSingleton(new ObjectPool<PhoneModel>());
            }


            //注册实体工具
            if (InjectService.Get<EntityUtils>() == null)
            {
                InjectService.RegisterSingleton(new EntityUtils());
            }
        }
    }
}

