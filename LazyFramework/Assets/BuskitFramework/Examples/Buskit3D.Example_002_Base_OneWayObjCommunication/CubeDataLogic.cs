/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_002(业务逻辑)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_002
{
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class CubeDataLogic : LogicBehaviour
    {
        //Slider对象
        public Slider slider;
        //旋转速速
        private float speedZ;

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("SpeedZ"))
            {
                speedZ = (float)evt.NewValue;              
            }          
        }

        /// <summary>
        /// Cube旋转控制
        /// </summary>
        private void Update()
        {
            this.transform.Rotate(Vector3.forward * speedZ * 10);
        }
    }
}

