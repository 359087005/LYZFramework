/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AttributeListLogic
* 创建日期：2019-04-12 18:34:41
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Buskit3D.Example_046_Communication
{   
	/// <summary>
    /// 
    /// </summary>
	public class AttributeListLogic : LogicBehaviour 
	{
        AttributeListEntity attributeListEntity;

        private void Start()
        {
            attributeListEntity = (AttributeListEntity)GetComponent<AttributeListModel>().DataEntity;
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
            }
        }

        private void UIActive(bool isShow)
        {
            if (isShow)
            {
                transform.localScale = Vector3.one;
                InitTowerList();
            }
            else
            {
                transform.localScale = Vector3.zero;
                DelAllPrefab();
            }
        }

        private void Back()
        {
            var entity = (AttributeMenuEntity)FindObjectOfType<AttributeMenuModel>().DataEntity;
            entity.isShow = true;
            attributeListEntity.isShow = false;
        }


        public void InitTowerList()
        {

            ObjectPool<TowerModel> pool = InjectService.Get<ObjectPool<TowerModel>>();

            Transform parent = GameObject.Find("LinkListParent").transform;

            //遍历字典读取所有的信塔，加载预设
            foreach (var key in pool.objectsDic.Keys)
            {
                
                var model = pool.objectsDic[key];
                var entity = (TowerEntity)model.DataEntity;
                string strList = "";
                for (int i = 0; i < entity.towerList.Count; i++) {
                    strList += entity.towerList[i].ToString() + ">";
                }
                if (string.IsNullOrEmpty(strList))
                {
                    continue;
                }
                else {
                    strList = strList.Remove(strList.Length - 1);
                    //把资源加载到内存中
                    Object obj = Resources.Load("TextList", typeof(GameObject));
                    //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
                    GameObject prefab = Instantiate(obj) as GameObject;
                    prefab.transform.SetParent(parent);
                    prefab.transform.Find("name/Text").GetComponent<Text>().text = strList;
                }
            }
        }

        public void DelAllPrefab()
        {
            Transform parent = GameObject.Find("LinkListParent").transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}