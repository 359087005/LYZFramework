/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PhoneModel
* 创建日期：2019-04-10 11:09:40
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine.EventSystems;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class PhoneModel : DataModelBehaviour
    {
        /// <summary>
        /// 初始化实体对象
        /// </summary>
        private void Awake()
        {
            this.DataEntity = new PhoneEntity();
            this.Watch(this);
        }

        /// <summary>
        /// 在对象池中删除本对象
        /// </summary>
        private void OnDestroy()
        {
            //在对象池中删除本对象
            ObjectPool<PhoneModel> pool =
                InjectService.Get<ObjectPool<PhoneModel>>();
            pool.RemoveObject(this);
        }

        public override void LoadStorageData()
        {
            base.LoadStorageData();
            Watch(this);

            Transform parent = GameObject.Find("Phone").transform;
            transform.SetParent(parent);

            ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
            if (!pool.objectsDic.ContainsKey(DataEntity.objectID))
            {
                pool.objectsDic.Add(DataEntity.objectID, this);
            }
        }

        private void OnMouseDrag()
        {
            //鼠标发射射线，打印接收到射线的物体
            //射线检测，点击UI和空白处无反应
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.parent.name.Equals("Plane") || hit.collider.transform.name.Contains("Terrain"))
                {
                    var entity = (PhoneEntity)DataEntity;
                    Vector3 vector3 = hit.collider.transform.position;
                    vector3.y = 0.125f;
                    entity.point = vector3;
                }
            }
        }
    }
}

