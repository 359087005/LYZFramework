/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ServiceInitialize
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：服务初始化程序
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 服务初始化程序
    /// </summary>
    public class ServiceInitialize : MonoBehaviour
    {
        private void Awake()
        {
            if (InjectService.Get<IServiceCommand>() == null)
            {
                ServiceCommand commandService = new ServiceCommand();
                commandService.Initialize();
            }

            if (InjectService.Get<UIController>() == null)
            {
                InjectService.RegisterSingleton(new UIController());
            }
        }
    }
}

