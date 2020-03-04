
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using System;

namespace Buskit3D.Training.IoC.E
{
    /// <summary>
    /// Foo依赖A
    /// </summary>
    public class Foo
    {
        public string name = "";
        public A a;
    }

    /// <summary>
    /// A依赖B和C
    /// </summary>
    public class A
    {
        public string id = "";
        public B b;
        public C c;
    }

    /// <summary>
    /// B是一个简单类
    /// </summary>
    public class B
    {

    }

    /// <summary>
    /// C是一个简单类
    /// </summary>
    public class C
    {

    }

    /// <summary>
    /// Foo创建工厂
    /// </summary>
    public class FooFactory
    {
        /// <summary>
        /// 注册为IoC工厂方法
        /// </summary>
        public void RegisterIoCFactory()
        {
            InjectService.RegisterTransient<Foo>(CreateFoo);
        }

        /// <summary>
        /// 工厂方法封装了一个复杂的对象创建过程
        /// </summary>
        /// <returns></returns>
        public Foo CreateFoo()
        {
            Foo f = new Foo();
            f.a   = new A();
            f.a.b = new B();
            f.a.c = new C();
            return f;
        }
    }

}

