/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（命令数据模型）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 命令系统对象模型类
    /// </summary>
    [RequireComponent(typeof(CreateCommandLogic))]
    [RequireComponent(typeof(ColorCommandLogic))]
    [RequireComponent(typeof(DeleteCommandLogic))]
    public class CommandDataModel : DataModelBehaviour
    {      
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            CommandDataEntity entity = new CommandDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }
    }
}

