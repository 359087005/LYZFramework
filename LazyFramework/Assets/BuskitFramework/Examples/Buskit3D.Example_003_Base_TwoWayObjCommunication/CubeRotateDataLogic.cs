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
    /// Cube对象业务逻辑类
    /// </summary>
    public class CubeRotateDataLogic : LogicBehaviour
    {
        //Cube实体对象
        private CubeRotateDataEntity entity;
        //旋转速度
        private float speed;

        private void Start()
        {
            entity = (CubeRotateDataEntity)GameObject.FindObjectOfType<CubeRotateDataModel>().DataEntity;
        }
        /// <summary>
        /// 收到消息后业务逻辑处理
        /// </summary>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //监视自身实体数据逻辑
            if (evt.PropertyName.Equals("SpeedZ"))
            {
                speed = (float)evt.NewValue;
            }
            //监视Sphere实体数据逻辑
            if (evt.PropertyName.Equals("Scale"))
            {
                speed = (float)evt.NewValue * 2;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * speed);
            if( speed >= 3)
            {
                entity.IsChangeColor = true;
            }
            else if(speed < 3)
            {
                entity.IsChangeColor = false;
            }
        }
    }
}

