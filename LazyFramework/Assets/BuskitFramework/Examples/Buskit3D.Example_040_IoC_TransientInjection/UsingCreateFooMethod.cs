/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UsingCreateFooMethod
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：使用CreateFoo函数创建一个对象
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Training.IoC.E
{
    /// <summary>
    /// 使用CreateFoo方法瞬态创建一个对象
    /// </summary>
    public class UsingCreateFooMethod : MonoBehaviour
    {
        Foo foo1 = null;

        Foo foo2 = null;

        /// <summary>
        /// 测试瞬态注入
        /// </summary>
        private void Start()
        {
            foo1 = InjectService.Get<Foo>();
            foo2 = InjectService.Get<Foo>();
            foo1.name = "创建了一个Foo类的实例（调用工厂方法完成实例的创建过程）并命名为foo1";
            foo2.name = "创建了一个Foo类的实例（调用工厂方法完成实例的创建过程）并命名为foo2";
            Debug.Log(foo1.name);
            Debug.Log(foo2.name);

            A a1 = InjectService.Get<Foo>().a;
            A a2 = InjectService.Get<Foo>().a;

            a1.id = "a1";
            a2.id = "a2";
            Debug.Log(a1.id);
            Debug.Log(a2.id);

        }
    }
}
