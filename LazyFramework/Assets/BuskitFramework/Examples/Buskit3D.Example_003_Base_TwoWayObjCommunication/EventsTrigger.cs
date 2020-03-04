/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_003(消息触发器)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_003
{
    /// <summary>
    /// 触发器管理类
    /// </summary>
    public class EventsTrigger : MonoBehaviour
    {
        //Cube实体对象
        private CubeRotateDataEntity cubeEntity;
        //Sphere实体对象
        private SphereDataEntity sphereEntity;
        
        /// <summary>
        /// Unity Method
        /// </summary>
        private void Start()
        {
            cubeEntity = (CubeRotateDataEntity)GameObject.FindObjectOfType<CubeRotateDataModel>().DataEntity;
            sphereEntity = (SphereDataEntity)GameObject.FindObjectOfType<SphereDataModel>().DataEntity;
        }

        /// <summary>
        /// 速度控制器
        /// </summary>
        /// <param name="value"></param>
        public void OnCubeSliderValueChanged(float value)
        {
            cubeEntity.SpeedZ = value;
        }

        /// <summary>
        /// Sphere大小控制器
        /// </summary>
        /// <param name="value"></param>
        public void OnScaleSliderValueChanged(float value)
        {
            sphereEntity.Scale = value;
        }
    }
}

