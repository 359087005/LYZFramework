/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（Cube数据模型实体）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 数据模型实体类
    /// </summary>
    public class CubeDataModelEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 控制旋转速度
        /// </summary>
        [RestoreFireLogic]
        public float RotateSpeed;
    }
}

