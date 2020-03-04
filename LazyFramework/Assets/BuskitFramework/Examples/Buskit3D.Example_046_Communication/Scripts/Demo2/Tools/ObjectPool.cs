/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ObjectPool
* 创建日期：2019-03-31 14:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：简单的对象池，用来管理某一类对象
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using System.Collections.Generic;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 简单的对象池
    /// </summary>
    public class ObjectPool<T> where T : class
    {
        public delegate void ForeachCallback(int i, T t);
        public Dictionary<int, T> objectsDic = new Dictionary<int, T>();

        /// <summary>
        /// 对象列表
        /// </summary>
        List<T> objects = new List<T>();

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <param id="obj"></param>
        public void RegisterObject(T obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param id="obj"></param>
        public void RemoveObject(T obj)
        {
            objects.Remove(obj);
        }

        /// <summary>
        /// 遍历对象池
        /// </summary>
        /// <param id="cb"></param>
        public void Foreach(ForeachCallback cb)
        {
            if (cb == null)
            {
                return;
            }
            int i = 0;
            foreach (T obj in objects)
            {
                cb(i, obj);
            }
        }

        /// <summary>
        /// 查找特定类型的对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public T1[] FindObjects<T1>() where T1:T
        {
            List<T1> list = new List<T1>();
            foreach(T item in objects)
            {
                if(item is T1)
                {
                    T1 tt = (T1)item;
                    list.Add(tt);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 查找T1类型的对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public T1 FindObject<T1>() where T1: T
        {
            foreach (T item in objects)
            {
                if (item is T1)
                {
                    T1 tt = (T1)item;
                    return tt;
                }
            }
            return default;
        }

        /// <summary>
        /// 获取大小
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return objects.Count;
        }

        /// <summary>
        /// 获取第几个对象
        /// </summary>
        /// <param id="index"></param>
        /// <returns></returns>
        public T Get(int index)
        {
            return objects[index];
        }

        /// <summary>
        /// 清除所有对象
        /// </summary>
        public void OnDestroy()
        {
            this.objects.Clear();
        }
    }
}


