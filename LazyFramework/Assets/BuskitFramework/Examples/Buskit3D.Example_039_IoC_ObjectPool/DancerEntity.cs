/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：LightEntity
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：灯实体类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;
using UnityEngine;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 灯实体类
    /// </summary>
    public class DancerEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 是否在跳舞
        /// </summary>
        public bool isDancing = false;

        /// <summary>
        /// 0代表左旋，1代表右旋
        /// </summary>
        public int direction = -1;
    }
}


