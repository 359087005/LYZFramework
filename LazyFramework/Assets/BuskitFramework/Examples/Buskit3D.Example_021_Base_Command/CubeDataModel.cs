/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModel
* 创建日期：2018-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（Cube数据模型）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_021
{
    [RequireComponent(typeof(CubeDataModelLogic))]
    /// <summary>
    /// Cube对象模型
    /// </summary>
    public class CubeDataModel : DataModelBehaviour
    {
        /// <summary>
        /// 监视数据模型实体
        /// </summary>
        private void Awake()
        {
            CubeDataModelEntity entity = new CubeDataModelEntity();
            this.DataEntity = entity;
            Watch(this);

        }
    }

}
