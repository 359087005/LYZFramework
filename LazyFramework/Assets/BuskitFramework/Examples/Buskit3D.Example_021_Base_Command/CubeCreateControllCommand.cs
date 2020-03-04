/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（Cube创建命令）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using Com.Rainier.BusKit.Unity.Modules.Command;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// Cube创建命令
    /// </summary>
    public class CubeCreateControllCommand : AbsCommand
    {
        /// <summary>
        /// 加载的预设
        /// </summary>
        public GameObject LoadPrefab = null;

        /// <summary>
        /// 物体坐标
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// 创建的Cube件对象
        /// </summary>
        GameObject newObject = null;

        /// <summary>
        /// Execute操作
        /// </summary>
        public override void Execute()
        {
            GameObject Obj = (GameObject)Resources.Load("Example_03_Services_Command.Assets/Cube");             
            if (Obj != null)
            {             
                GameObject ParentRoot = GameObject.Find("Objects");           
                if (ParentRoot != null)
                {
                    //回放系统需要，实例化物体，添加编号,请记住动态实例化物体的方法
                    newObject = MonoBehaviour.Instantiate(Obj);
                    ReplayManager.RegisterPrefab(newObject);
                    newObject.transform.parent = ParentRoot.transform;
                    //物体坐标从数据实体(CommandDataEntity)中获得
                    newObject.transform.position = Position;
                }
            }
            else
            {
                Debug.Log("Obj Is NULL!!!!");
            }
        }

        /// <summary>
        /// Undo操作
        /// </summary>
        public  override void Undo()
        {
            newObject.SetActive(false);
        }

        /// <summary>
        /// Redo操作
        /// </summary>
        public override void Redo()
        {
            newObject.SetActive(true);
        }
    }
}

