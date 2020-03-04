/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeEntity
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：简单的正方体实体类
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D.Training.IoC.B
{
    /// <summary>
    /// 简单的正方体实体类
    /// </summary>
    public class CubeEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 旋转速度
        /// </summary>
        public float angle = 3f;

        /// <summary>
        /// 缩放
        /// </summary>
        public float scale = 1.0f;
    }
}


