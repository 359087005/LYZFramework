/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：FirstControlDataEntity
* 创建日期：2018-04-07 10:58:17
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述：第一人称数据实体
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
namespace Buskit3D_Example_006_FirstController
{
    /// <summary>
    /// 第一人称数据实体
    /// </summary>
    public class FirstDataEntity :BaseDataModelEntity
    {
        //player位置
        public Vector3 localPosition;
        //player旋转角度
        public float playerRotY;
        //相机旋转角度
        public float cameraRotX;
    }
}


