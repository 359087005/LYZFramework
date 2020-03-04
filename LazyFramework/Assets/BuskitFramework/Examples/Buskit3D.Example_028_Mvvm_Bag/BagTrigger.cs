
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： BagTrigger
* 创建日期：2019-03-21 10:12:23
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Buskit3D.Example_36_Mvvm_Bag
{
    /// <summary>
    /// 
    /// </summary>
	public class BagTrigger : MonoBehaviour 
	{
        public static BagTrigger Instance;
        public Dictionary<int, BagItem> bagDic = new Dictionary<int, BagItem>();
        public Transform tabParent;
        public Transform itemParent;
        public ToggleGroup tabGroup;       

        private void Awake()
        {
            Instance = this;
            #region 背包Item
            BagItem bagItem1 = new BagItem(1, 1, 1);
            BagItem bagItem2 = new BagItem(2, 2, 1);
            BagItem bagItem3 = new BagItem(3, 3, 1);
            BagItem bagItem4 = new BagItem(4, 4, 1);
            BagItem bagItem5 = new BagItem(5, 5, 2);
            BagItem bagItem6 = new BagItem(6, 6, 2);
            BagItem bagItem7 = new BagItem(7, 7, 2);
            BagItem bagItem8 = new BagItem(8, 8, 3);
            bagDic.Add(1, bagItem1);
            bagDic.Add(2, bagItem2);
            bagDic.Add(3, bagItem3);
            bagDic.Add(4, bagItem4);
            bagDic.Add(5, bagItem5);
            bagDic.Add(6, bagItem6);
            bagDic.Add(7, bagItem7);
            bagDic.Add(8, bagItem8);
            #endregion            
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start () 
		{
            var dataEntity = GetComponent<BagViewModel>().DataEntity;
            BagEntity _entity = (BagEntity)dataEntity;
            _entity.initBag = 1;
        }

        public void changID() {
            var dataEntity = GetComponent<BagViewModel>().DataEntity;
            BagEntity _entity = (BagEntity)dataEntity;
            _entity.initBag ++;
        }
	}

    public struct BagItem
    {
        public int id;
        public int name;
        public int type;
        public BagItem(int id, int name, int type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }
    }
}

