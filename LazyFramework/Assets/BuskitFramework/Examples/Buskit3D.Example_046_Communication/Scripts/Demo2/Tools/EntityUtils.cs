﻿/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：实体类工具，用来查找一个GameObject上的实体对象、数据模型、业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 实体类工具，用来查找一个GameObject上的实体对象、数据模型、业务逻辑
    /// </summary>
    public class EntityUtils
    {
        /// <summary>
        /// 获取一个GameObject上的实体对象
        /// </summary>
        /// <typeparam id="T"></typeparam>
        /// <param id="go"></param>
        /// <returns></returns>
        public T GetEntity<T>(GameObject go) where T : BaseDataModelEntity
        {
            T entity = null;
            DataModelBehaviour dm = go.GetComponent<DataModelBehaviour>();
            if (dm != null && dm.DataEntity is T)
            {
                entity = (T)dm.DataEntity;
            }
            return entity;
        }

        /// <summary>
        /// 获取上的实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public T GetEntity<T>(DataModelBehaviour dataModel) where T : BaseDataModelEntity
        {
            if(dataModel.DataEntity is T)
            {
                return (T)dataModel.DataEntity;
            }
            return null;
        }

        /// <summary>
        /// 获取物体的数据模型
        /// </summary>
        /// <typeparam id="T"></typeparam>
        /// <param id="go"></param>
        /// <returns></returns>
        public T GetDataModel<T>(GameObject go) where T : DataModelBehaviour
        {
            T dm = null;
            if(dm is T)
            {
                dm = (T)go.GetComponent<DataModelBehaviour>();
            }
            return dm;
        }

        /// <summary>
        /// 获取物体业务逻辑
        /// </summary>
        /// <typeparam id="T"></typeparam>
        /// <param id="go"></param>
        /// <returns></returns>
        public T GetLogic<T>(GameObject go) where T : LogicBehaviour
        {
            T logic = go.GetComponent<T>();
            return logic;
        }

        /// <summary>
        /// 获取物体上的所有业务逻辑
        /// </summary>
        /// <param id="go"></param>
        /// <returns></returns>
        public LogicBehaviour[] GetLogics(GameObject go)
        {
            return go.GetComponents<LogicBehaviour>();
        }
    }
}
