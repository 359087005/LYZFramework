/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：EntityUtils
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：正方体的数据模型
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Training.IoC.B
{
    /// <summary>
    /// 正方体的数据模型
    /// </summary>
    public class CubeTrigger : MonoBehaviour
    {
        /// <summary>
        /// 正方形物体
        /// </summary>
        public GameObject cubeObject;

        /// <summary>
        /// 实体工具
        /// </summary>
        [Inject]
        IEntityService entityUtils;

        /// <summary>
        /// 注入依赖的对象
        /// </summary>
        private void Start()
        {
            InjectService.InjectInto(this);
        }

        /// <summary>
        /// 旋转的回调函数
        /// </summary>
        /// <param id="newAngle"></param>
        public void CallbackSliderRotation(float newAngle)
        {
            if (cubeObject != null)
            {
                CubeEntity entity = entityUtils.GetEntity<CubeEntity>(cubeObject);
                entity.angle = newAngle;
            }
        }

        /// <summary>
        /// 缩放回调函数
        /// </summary>
        /// <param id="newScaling"></param>
        public void CallbackSliderScaling(float newScaling)
        {
            if (cubeObject != null)
            {
                CubeEntity entity = entityUtils.GetEntity<CubeEntity>(cubeObject);
                entity.scale = newScaling;
            }
        }
    }
}

