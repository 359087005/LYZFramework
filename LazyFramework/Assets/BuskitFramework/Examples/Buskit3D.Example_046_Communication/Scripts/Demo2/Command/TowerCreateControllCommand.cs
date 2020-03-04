/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CubeCreateControllCommand
* 创建日期：2019-04-10 11:36:28
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.BusKit.Unity.Modules.Command;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    /// 
    /// </summary>
	public class TowerCreateControllCommand : AbsCommand
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
            //回放系统需要，实例化物体，添加编号,请记住动态实例化物体的方法
            newObject = ReplayManager.GetInstance("Terrain", Vector3.zero);
            newObject.transform.parent = GetNewPoint();
            //物体坐标从数据实体(CommandDataEntity)中获得
            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localScale = Vector3.one * 0.005f;
            var towerModel = newObject.GetComponent<TowerModel>();
            var entity = (TowerEntity)newObject.GetComponent<TowerModel>().DataEntity;
            entity.towerPoint = newObject.transform.parent.name;
            ReplayManager.RegisterPrefab(newObject);

            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            if (!pool.objectsDic.ContainsKey(entity.objectID))
            {
                pool.objectsDic.Add(entity.objectID, towerModel);
            }
        }

        /// <summary>
        /// Undo操作
        /// </summary>
        public override void Undo()
        {
            newObject.SetActive(false);
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            pool.RemoveObject(newObject.GetComponent<TowerModel>());
            var towerModel = newObject.GetComponent<TowerModel>();
            var towerEntity = (TowerEntity)towerModel.DataEntity;
            pool.objectsDic.Remove(towerEntity.objectID);
        }

        /// <summary>
        /// Redo操作
        /// </summary>
        public override void Redo()
        {
            newObject.SetActive(true);
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            var towerModel = newObject.GetComponent<TowerModel>();
            var towerEntity = (TowerEntity)towerModel.DataEntity;
            pool.RegisterObject(towerModel);
            if (!pool.objectsDic.ContainsKey(towerEntity.objectID))
            {
                pool.objectsDic.Add(towerEntity.objectID, towerModel);
            }
        }


        /// <summary>
        /// 得到位置
        /// </summary>
        private Transform GetNewPoint()
        {
            Vector3 vector3 = Vector3.zero;
            Transform plan = GameObject.Find("Plane").transform;
            for (int i = 0; i < plan.childCount; i++)
            {
                if (plan.GetChild(i).childCount == 0)
                {
                    return plan.GetChild(i);
                }
                else if (plan.GetChild(i).childCount > 0) {
                    bool isBackPoint = true;
                    for (int j = 0; j < plan.GetChild(i).childCount; j++) {
                        if (plan.GetChild(i).GetChild(j).gameObject.activeSelf) {
                            isBackPoint = false;
                            break;
                        }
                    }
                    if (isBackPoint) {
                        return plan.GetChild(i);
                    }
                }
            }
            return null;
        }

    }
}

