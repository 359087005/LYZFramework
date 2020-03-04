/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_002(数据模型)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_002
{
    /// <summary>
    /// 对象模型类
    /// </summary>
    public class CubeDataModel : DataModelBehaviour
    {
        private void Awake()
        {
            Watch(this);
        }
        /// <summary>
        /// 对象间单向通信，一个对象监视另一个对象的变化
        /// </summary>  
        protected override void Start()
        {          
            base.Start();
            BindingTool bt = BindingTool.GetInstance();
            GameObject eventSource = GameObject.Find("Canvas/SpeedController");
            bt.Binding(this.gameObject, eventSource);         
        }
    }
}

