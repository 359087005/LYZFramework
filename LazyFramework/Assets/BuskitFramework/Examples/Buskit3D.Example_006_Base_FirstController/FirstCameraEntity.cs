/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： FirstCameraEntity
* 创建日期：2019-01-14 11:57:34
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：第一人称相机数据实体
******************************************************************************/

using Com.Rainier.Buskit3D;

namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 第一人称相机数据实体
    /// </summary>
    public class FirstCameraEntity : BaseDataModelEntity 
	{ 
        /// <summary>
        /// 代表物体旋转的四元数
        /// </summary>
        public UnityEngine.Quaternion localRotation;
	}
}

