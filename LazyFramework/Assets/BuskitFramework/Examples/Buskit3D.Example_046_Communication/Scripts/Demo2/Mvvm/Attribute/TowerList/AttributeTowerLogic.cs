/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AttributeTowerLogic
* 创建日期：2019-04-12 17:40:56
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine.UI;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeTowerLogic : LogicBehaviour
    {
        private AttributeToweEntity attributeToweEntity;

        private void Start()
        {
            attributeToweEntity = (AttributeToweEntity)GetComponent<AttributeTowerModel>().DataEntity;
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    ActiveUI(isShow);
                    break;
                case "back":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    else
                    {
                        Back();
                    }
                    break;
            }
        }

        public void Back()
        {
            var menuEntity = (AttributeMenuEntity)FindObjectOfType<AttributeMenuModel>().DataEntity;
            menuEntity.isShow = true;
            attributeToweEntity.isShow = false;
        }


        public void ActiveUI(bool isTrue)
        {
            if (isTrue)
            {
                transform.localScale = Vector3.one;
                //初始化列表
                InitTowerList();    
            }
            else
            {
                transform.localScale = Vector3.zero;
                //删除列表信息
                DelAllPrefab();
            }
        }


        public void InitTowerList() {

            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();

            Transform parent = GameObject.Find("TowerParent").transform;

            //遍历字典读取所有的信塔，加载预设
            foreach (var key in pool.objectsDic.Keys) {
                //把资源加载到内存中
                Object obj = Resources.Load("TowerList", typeof(GameObject));
                //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                GameObject prefab = Instantiate(obj) as GameObject;
                prefab.transform.SetParent(parent);
                var towerModel = pool.objectsDic[key];
                var entity = (TowerEntity)towerModel.DataEntity;
                prefab.transform.Find("name/Text").GetComponent<Text>().text = towerModel.DataEntity.objectID.ToString();
                prefab.transform.Find("Point/Text").GetComponent<Text>().text = entity.towerPoint;
                string strList = "";
                for (int i = 0; i < entity.phoneList.Count; i++) {
                    strList += entity.phoneList[i].ToString()+",";
                }
                if (string.IsNullOrEmpty(strList)) strList = "";
                else strList = strList.Remove(strList.Length - 1);
                prefab.transform.Find("List/Text").GetComponent<Text>().text = strList;
            }
        }

        public void DelAllPrefab() {
            Transform parent = GameObject.Find("TowerParent").transform;
            if (parent.childCount == 1) return;
            for (int i = 1; i < parent.childCount; i++) {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

    }
}

