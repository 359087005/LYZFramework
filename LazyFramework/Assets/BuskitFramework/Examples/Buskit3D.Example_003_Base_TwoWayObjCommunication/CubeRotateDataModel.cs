/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_003(Cube旋转数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_003
{
    /// <summary>
    /// Cube对象模型
    /// </summary>
    public class CubeRotateDataModel : DataModelBehaviour
    {
        /// <summary>
        /// 观察实体
        /// </summary>
        private void Awake()
        {
            CubeRotateDataEntity entity = new CubeRotateDataEntity();
            this.DataEntity = entity;
            Watch(this);
        }
        /// <summary>
        /// 观察自己同时监视Sphere
        /// </summary>
        protected override void Start()
        {           
            BindingTool bt = BindingTool.GetInstance();
            GameObject eventSource = transform.parent.Find("Sphere").gameObject;
            bt.Binding(this.gameObject, eventSource);
            base.Start();
        }
    }
}

