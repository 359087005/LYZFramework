/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelRotateLogic
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：旋转业务逻辑处理
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class CubeDataModelLogic : LogicBehaviour
    {
        //Slider对象
        private Slider cubeSlider;
        //旋转速度
        private float speed;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            cubeSlider = GameObject.Find("RotationSlider").gameObject.GetComponent<Slider>();
        }

        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
                Debug.Log(evt.PropertyName);
            //处理旋转业务逻辑
            if (evt.PropertyName.Equals("RotateSpeed"))
            {
                speed = (float)evt.NewValue;
            }
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Update()
        {
            this.transform.Rotate(Vector3.forward * speed);
        }
    }
}

