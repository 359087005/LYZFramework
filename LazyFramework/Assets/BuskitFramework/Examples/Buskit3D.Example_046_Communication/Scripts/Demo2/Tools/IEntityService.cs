/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 功能描述：IEntityService
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Example_046_Communication
{
    public interface IEntityService
    {
        /// <summary>
        /// 获取一个GameObject上的实体对象
        /// </summary>
        /// <typeparam id="T"></typeparam>
        /// <param id="go"></param>
        /// <returns></returns>
        T GetEntity<T>(GameObject go) where T : BaseDataModelEntity;

        /// <summary>
        /// 获取物体的数据模型
        /// </summary>
        /// <typeparam id="T"></typeparam>
        /// <param id="go"></param>
        /// <returns></returns>
        T GetDataModel<T>(GameObject go) where T : DataModelBehaviour;
    }

}

