/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（Cube颜色控制命令）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using Com.Rainier.BusKit.Unity.Modules.Command;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// Cube颜色控制命令类
    /// </summary>
    public class CubeColorControllCommand : AbsCommand
    {
        /// <summary>
        /// 改变颜色的Cube对象
        /// </summary>
        public GameObject Colorobj = null;

        /// <summary>
        /// 目标物体UUID
        /// </summary>
        public int ColorObjectID ;

        /// <summary>
        /// 旧颜色
        /// </summary>
        public Color OldColor;

        /// <summary>
        /// 新颜色
        /// </summary>
        public Color NewColor;

        /// <summary>
        /// Execute操作
        /// </summary>
        public override void Execute()
        {
            //物体UUID从数据实体（CommanDataEntity）中获得
            CommandDataEntity entity = (CommandDataEntity)GameObject.FindObjectOfType<CommandDataModel>().DataEntity;
            CubeDataModel[] cubes = GameObject.FindObjectsOfType<CubeDataModel>();
            foreach (CubeDataModel cube in cubes)
            {
                if (((BaseDataModelEntity)cube.DataEntity).objectID.Equals(ColorObjectID))
                {
                    Colorobj = cube.gameObject;
                    Colorobj.GetComponent<Renderer>().material.color = NewColor;
                    return;
                }
            }
        }
        /// <summary>
        /// Undo操作
        /// </summary>
        public override void Undo()
        {
            Colorobj.GetComponent<Renderer>().material.color = OldColor;
        }

        /// <summary>
        /// Redo操作
        /// </summary>
        public override void Redo()
        {
            Colorobj.GetComponent<Renderer>().material.color = NewColor;
        }
    }
}

