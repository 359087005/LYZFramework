/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AttrobuteStatisticsLogic
* 创建日期：2019-04-12 18:20:40
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
    public class AttributeStatisticsLogic : LogicBehaviour
    {

        AttributeStatisticsEntity attrobuteStatisticsEntity;

        private void Start()
        {
            attrobuteStatisticsEntity = (AttributeStatisticsEntity)GetComponent<AttributeStatisticsModel>().DataEntity;
        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {                
                case "back":
                    if (evt.OldValue == evt.NewValue)
                    {
                        return;
                    }
                    Back();
                    break;
                case "isShow":
                    bool isShow = (bool)evt.NewValue;
                    UIActive(isShow);
                    break;
                case "toggle0":
                    if (attrobuteStatisticsEntity.isShow == false) return;
                    bool isTrue = (bool)evt.NewValue;
                    if (isTrue) {
                        InitPhone();
                    }
                    break;
                case "toggle1":
                    if (attrobuteStatisticsEntity.isShow == false) return;
                    isTrue = (bool)evt.NewValue;
                    if (isTrue)
                    {
                        InitTower();
                    }
                    break;
                case "toggle2":
                    if (attrobuteStatisticsEntity.isShow == false) return;
                    isTrue = (bool)evt.NewValue;
                    if (isTrue)
                    {
                        InitPhoneDoing();
                    }
                    break;
                case "toggle3":
                    if (attrobuteStatisticsEntity.isShow == false) return;
                    isTrue = (bool)evt.NewValue;
                    if (isTrue)
                    {
                        InitPhoneLeave();
                    }
                    break;
            }
        }

        private void UIActive(bool isShow)
        {
            if(isShow)
            {
                transform.localScale = Vector3.one;
                InitPhone();
                attrobuteStatisticsEntity.toggle0 = true;
            }
            else {
                transform.localScale = Vector3.zero;
            }
        }

        private void Back()
        {
            var entity = (AttributeMenuEntity)FindObjectOfType<AttributeMenuModel>().DataEntity;
            entity.isShow = true;
            attrobuteStatisticsEntity.isShow = false;
        }

        /// <summary>
        /// 初始化手机
        /// </summary>
        public void InitPhone()
        {
            Debug.Log("InitPhone");
            InitTowerList(0);

        }
        /// <summary>
        /// 初始化塔
        /// </summary>
        public void InitTower() {
            Debug.Log("InitTower");
            InitTowerList(1);
        }
        /// <summary>
        /// 初始化通话中的手机
        /// </summary>
        public void InitPhoneDoing()
        {
            Debug.Log("InitPhoneDoing");
            InitTowerList(2);
        }
        /// <summary>
        /// 初始哈离线的手机
        /// </summary>
        public void InitPhoneLeave()
        {
            Debug.Log("InitPhoneLeave");
            InitTowerList(3);
        }

        public void InitTowerList(int index)
        {
            //删除当前的预设租
            DelAllPrefab();
            ObjectPool<TowerModel> poolTower = InjectService.Get<ObjectPool<TowerModel>>();
            ObjectPool<PhoneModel> poolPhone = InjectService.Get<ObjectPool<PhoneModel>>();
            Transform parent = GameObject.Find("StatisticsParent").transform;
            //生成新的预设租
            switch (index) {
                case 0:
                    foreach (var key in poolPhone.objectsDic.Keys)
                    {
                        var model = poolPhone.objectsDic[key];
                        var entity = (PhoneEntity)model.DataEntity;
                        Object obj = Resources.Load("TextList", typeof(GameObject));
                        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                        GameObject prefab = Instantiate(obj) as GameObject;
                        prefab.transform.SetParent(parent);
                        prefab.transform.Find("name/Text").GetComponent<Text>().text = entity.objectID.ToString();
                    }

                    break;
                case 1:
                    foreach (var key in poolTower.objectsDic.Keys)
                    {
                        var model = poolTower.objectsDic[key];
                        var entity = (TowerEntity)model.DataEntity;
                        Object obj = Resources.Load("TextList", typeof(GameObject));
                        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                        GameObject prefab = Instantiate(obj) as GameObject;
                        prefab.transform.SetParent(parent);
                        prefab.transform.Find("name/Text").GetComponent<Text>().text = entity.objectID.ToString();
                    }
                    break;
                case 2:
                    foreach (var key in poolPhone.objectsDic.Keys)
                    {
                        var model = poolPhone.objectsDic[key];
                        var entity = (PhoneEntity)model.DataEntity;
                        if (entity.currentState != 1) continue;
                        Object obj = Resources.Load("TextList", typeof(GameObject));
                        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                        GameObject prefab = Instantiate(obj) as GameObject;
                        prefab.transform.SetParent(parent);
                        prefab.transform.Find("name/Text").GetComponent<Text>().text = entity.objectID.ToString();
                    }
                    break;
                case 3:
                    foreach (var key in poolPhone.objectsDic.Keys)
                    {
                        var model = poolPhone.objectsDic[key];
                        var entity = (PhoneEntity)model.DataEntity;
                        if (entity.currentState != 2) continue;
                        Object obj = Resources.Load("TextList", typeof(GameObject));
                        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                        GameObject prefab = Instantiate(obj) as GameObject;
                        prefab.transform.SetParent(parent);
                        prefab.transform.Find("name/Text").GetComponent<Text>().text = entity.objectID.ToString();
                    }
                    break;
            }
        }

        public void DelAllPrefab()
        {
            Transform parent = GameObject.Find("StatisticsParent").transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}

