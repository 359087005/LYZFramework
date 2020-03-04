/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_003(CubeRotate业务逻辑)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_003
{
    /// <summary>
    /// Sphere业务逻辑处理类
    /// </summary>
    public class SphereDataLogic : LogicBehaviour
    {
        /// <summary>
        /// 收到消息后业务逻辑处理
        /// </summary>
        public override void ProcessLogic(PropertyMessage evt)
        {      
            if(evt.PropertyName.Equals("IsChangeColor"))
            {
                if((bool)evt.NewValue)
                {                   
                    this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                }else
                    this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;               
            }
            if (evt.PropertyName.Equals("Scale"))
            {
                float scale = (float)evt.NewValue;
                this.transform.localScale = new Vector3(scale, scale, scale);
            }         
        }
    }
}

