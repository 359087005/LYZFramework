/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： FirstCameraLogic
* 创建日期：2019-01-14 12:47:49
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 相机行为逻辑
    /// </summary>
    public class FirstCameraLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("cameraRotX"))
            {
                UpdataCameraRotation((float)evt.NewValue);
            }
        }

        /// <summary>
        /// 更新相机的欧拉角
        /// </summary>
        /// <param name="rotx"></param>
        private void UpdataCameraRotation(float rotx)
        {
            transform.localEulerAngles = new Vector3(rotx, 0, 0);
        }
    }
}

