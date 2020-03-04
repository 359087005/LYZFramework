/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：FactoryInitializer
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：工厂初始化器
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Buskit3D.Training.IoC.E
{
    /// <summary>
    /// 工厂初始化器
    /// </summary>
    public class FactoryInitializer : MonoBehaviour
    {
        /// <summary>
        /// 向IoC中注册CreateFoo函数，以便支持瞬态注入对象
        /// </summary>
        private void Awake()
        {
            new FooFactory().RegisterIoCFactory();
        }
    }
}


