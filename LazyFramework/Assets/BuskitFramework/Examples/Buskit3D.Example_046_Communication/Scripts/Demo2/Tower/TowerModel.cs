/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：TowerModel
* 创建日期：2019-04-10 10:39:50
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：    信号塔的数据模型
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buskit3D.Example_046_Communication
{
	/// <summary>
    /// 
    /// </summary>
	public class TowerModel : DataModelBehaviour
    {
        /// <summary>
        /// 初始化实体对象
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new TowerEntity();
            this.Watch(this);
        }

        /// <summary>
        /// 注册本对象到对象池中
        /// </summary>
        protected override void Start()
        {
            base.Start();

            //注册本对象到对象池中
            ObjectPool<TowerModel> pool =
                InjectService.Get<ObjectPool<TowerModel>>();
            pool.RegisterObject(this);
        }

        /// <summary>
        /// 在对象池中删除本对象
        /// </summary>
        private void OnDestroy()
        {
            //在对象池中删除本对象
            ObjectPool<TowerModel> pool =
                InjectService.Get<ObjectPool<TowerModel>>();
            pool.RemoveObject(this);
        }

        public override void LoadStorageData()
        {
            base.LoadStorageData();
            Watch(this);

            StartCoroutine(Test());        
        }

        IEnumerator Test() {
            yield return null;
            var entity = (TowerEntity)DataEntity;
            Transform parent = GameObject.Find(entity.towerPoint).transform;
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one * 0.005f;
            entity.reflush++;
            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();
            if (!pool.objectsDic.ContainsKey(entity.objectID))
            {
                pool.objectsDic.Add(DataEntity.objectID, this);
            }
        }



        public string LateName = "";

        private void OnMouseDrag()
        {
            //鼠标发射射线，打印接收到射线的物体
            //射线检测，点击UI和空白处无反应
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.parent.name.Equals("Plane")) {
                    if (LateName.Equals(hit.collider.transform.name)) return;
                    else
                    {
                        LateName = hit.collider.transform.name;                        
                        var entity = (TowerEntity)DataEntity;
                        entity.towerPoint = LateName;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var phoneModel = other.gameObject.GetComponent<PhoneModel>();
            var phoneEntity = (PhoneEntity)phoneModel.DataEntity;
            phoneEntity.towerModelID = GetComponent<UniqueID>().UniqueId;
            phoneEntity.currentState = 0;
            var towerEntity = (TowerEntity)DataEntity;
            if(!towerEntity.phoneList.Contains(other.gameObject.GetComponent<UniqueID>().UniqueId))
                towerEntity.phoneList.Add(other.gameObject.GetComponent<UniqueID>().UniqueId);
        }

        private void OnTriggerExit(Collider other)
        {
            var phoneModel = other.gameObject.GetComponent<PhoneModel>();
            var phoneEntity = (PhoneEntity)phoneModel.DataEntity;
            phoneEntity.towerModelID = -1;
            phoneEntity.currentState = 2;
            var towerEntity = (TowerEntity)DataEntity;
            towerEntity.phoneList.Remove(other.gameObject.GetComponent<UniqueID>().UniqueId);
        }
    }
}

