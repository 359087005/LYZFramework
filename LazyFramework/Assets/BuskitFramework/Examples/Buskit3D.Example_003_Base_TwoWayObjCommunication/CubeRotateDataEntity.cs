/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_003(Cube旋转数据实体)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

namespace Com.Rainier.Buskit3D.Example_003
{
    /// <summary>
    /// 数据实体类
    /// </summary>
    public class CubeRotateDataEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 速度
        /// </summary>
        [RestoreFireLogic]
        public float SpeedZ;
        /// <summary>
        /// 是否改变颜色
        /// </summary>
        [RestoreFireLogic]
        public bool IsChangeColor;

    }
}

