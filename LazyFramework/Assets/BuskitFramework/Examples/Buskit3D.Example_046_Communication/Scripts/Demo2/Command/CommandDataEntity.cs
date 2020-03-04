
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CommandDataEntity
* 创建日期：2019-04-10 11:34:07
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
	public class CommandDataEntity : BaseDataModelEntity
    {

        /// <summary>
        /// 创建信号塔
        /// </summary>
        public CreateTowerCommandStr CreateTowerMessage = new CreateTowerCommandStr();

        /// <summary>
        /// 创建手机
        /// </summary>
        public CreatePhoneCommandStr CreatePhoneMessage = new CreatePhoneCommandStr();

        /// <summary>
        /// Undo操作消息
        /// </summary>
        public string UndoMessage = "";

        /// <summary>
        /// Redo操作消息
        /// </summary>
        public string RedoMessage = "";
    }

    /// <summary>
    /// 创信号塔
    /// </summary>
    public struct CreateTowerCommandStr
    {
        //物体坐标
        public Vector3 Position;
    }
    /// <summary>
    /// 创建手机
    /// </summary>
    public struct CreatePhoneCommandStr
    {
        //物体坐标
        public Vector3 Position;
    }
}

