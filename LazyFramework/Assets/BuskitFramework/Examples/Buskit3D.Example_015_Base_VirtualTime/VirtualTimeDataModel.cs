/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-14 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：虚拟时间(数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_015
{
    /// <summary>
    /// 对象模型类
    /// </summary>
    [RequireComponent(typeof(VirtualTimeDataLogic))]
    public class VirtualTimeDataModel : DataModelBehaviour
    {
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            VirtualTimeDataEntity entity = new VirtualTimeDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }
    }
}

