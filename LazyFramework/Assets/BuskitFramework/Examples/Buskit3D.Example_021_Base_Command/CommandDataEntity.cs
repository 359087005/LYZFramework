/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（命令数据实体）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// 创建物体命令参数
    /// </summary>
    public struct CreateCommandStr
    {
        //物体坐标
        public Vector3 Position;
    }

    /// <summary>
    /// 删除物体命令参数
    /// </summary>
    public struct DeleteCommandStr
    {
        //物体识别ID
        public int ObjectID;
    }

    /// <summary>
    /// 改变颜色参数
    /// </summary>
    public struct ColorCommandStr
    {
        //物体UUID
        public int ObjectID;
        /// <summary>
        /// 物体新颜色和旧颜色（回放需要Color32类型）
        /// </summary>
        public Color32 NewColor;
        public Color32 OldColor;
    }

    public struct DannyMoveCommandStr {
        //物体UUID
        public int ObjectID;

        /// <summary>
        /// 物体原来的位置和新的位置
        /// </summary>
        public Vector3 NewPoint;
        public Vector3 OldPoint;
    }

    /// <summary>
    /// 命令系统数据实体类
    /// </summary>
    public class CommandDataEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 创建物体消息
        /// </summary>
        //[RestoreFireLogic]
        public CreateCommandStr CreatePartMessage = new CreateCommandStr();

        /// <summary>
        /// 删除物体消息
        /// </summary>
        public DeleteCommandStr DeletePartMessage = new DeleteCommandStr();

        /// <summary>
        /// 改变物体颜色消息
        /// </summary>
        public ColorCommandStr ColorParMessage = new ColorCommandStr();

        /// <summary>
        /// Undo操作消息
        /// </summary>
        public string UndoMessage = "";

        /// <summary>
        /// Redo操作消息
        /// </summary>
        public string RedoMessage = "";
    }
}

