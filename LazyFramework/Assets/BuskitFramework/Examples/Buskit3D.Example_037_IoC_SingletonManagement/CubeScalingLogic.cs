/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeScalingLogic
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：实体类工具，用来查找一个GameObject上的实体对象、数据模型、业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Training.IoC.A
{
    /// <summary>
    /// 旋转控制逻辑
    /// </summary>
    public class CubeScalingLogic : CommonLogic
    {
        /// <summary>
        /// 处理业务逻辑
        /// </summary>
        /// <param id="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("scale"))
            {
                CubeEntity entity = utilsEntity.GetEntity<CubeEntity>(gameObject);
                transform.localScale =
                    new Vector3(
                         entity.scale,
                         entity.scale,
                         entity.scale
                    );
                utilsLogging.Info(entity.scale.ToString());
                return;
            }
        }
    }
}


