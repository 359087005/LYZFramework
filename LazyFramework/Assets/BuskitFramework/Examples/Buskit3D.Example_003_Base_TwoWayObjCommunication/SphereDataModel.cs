/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_003(Sphere数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
namespace Com.Rainier.Buskit3D.Example_003
{
    /// <summary>
    /// Sphere对象模型类
    /// </summary>
    public class SphereDataModel : DataModelBehaviour
    {
        private void Awake()
        {
            SphereDataEntity entity = new SphereDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }

        /// <summary>
        /// 监听Cube对象
        /// </summary>
        protected override void Start()
        {
            BindingTool bt = BindingTool.GetInstance();
            GameObject eventSource = transform.parent.Find("Cube").gameObject;
            bt.Binding(this.gameObject, eventSource);
            base.Start();
        }
    }
}

