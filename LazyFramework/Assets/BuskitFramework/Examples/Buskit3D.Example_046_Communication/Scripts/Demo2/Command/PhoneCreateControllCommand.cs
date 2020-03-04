/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PhoneCreateControllCommand
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
	public class PhoneCreateControllCommand : AbsCommand
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
            Transform parent = GameObject.Find("Phone").transform;
            newObject = ReplayManager.GetInstance("Phone", Vector3.zero);
            ReplayManager.RegisterPrefab(newObject);
            newObject.transform.SetParent(parent);
            newObject.transform.localPosition = Position;

            //添加到对象池
            //注册本对象到对象池中
            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            var phoneModel = newObject.GetComponent<PhoneModel>();
            var phoneEntity = (PhoneEntity)phoneModel.DataEntity;
            pool.RegisterObject(phoneModel);
            if (!pool.objectsDic.ContainsKey(phoneEntity.objectID)) {
                pool.objectsDic.Add(phoneEntity.objectID, phoneModel);
            }
        }

        /// <summary>
        /// Undo操作
        /// </summary>
        public override void Undo()
        {
            newObject.SetActive(false);
            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            pool.RemoveObject(newObject.GetComponent<PhoneModel>());
            var phoneModel = newObject.GetComponent<PhoneModel>();
            var phoneEntity = (PhoneEntity)phoneModel.DataEntity;
            pool.objectsDic.Remove(phoneEntity.objectID);
        }

        /// <summary>
        /// Redo操作
        /// </summary>
        public override void Redo()
        {
            newObject.SetActive(true);
            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            var phoneModel = newObject.GetComponent<PhoneModel>();
            var phoneEntity = (PhoneEntity)phoneModel.DataEntity;
            pool.RegisterObject(phoneModel);
            if (!pool.objectsDic.ContainsKey(phoneEntity.objectID))
            {
                pool.objectsDic.Add(phoneEntity.objectID, phoneModel);
            }
        }        
    }
}

