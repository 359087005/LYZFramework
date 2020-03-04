/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Bag
* 创建日期：2019-12-13 15:18:48
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：背包系统,带有容量,可以互相转移物品
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class BagBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IProcessEvent
    {
        public static BagBase currentEnter, currentExit;

        private List<ItemInBagBase> items = new List<ItemInBagBase>();

        public List<ItemInBagBase> Items { get => items; private set => items = value; }

        public ushort size;
        private ushort count;
        public Action<ItemInBagBase> onPutIn,onTakeOut;

        public Text infoText;
        protected virtual void Start()
        {
            var allItem = GetComponentsInChildren<ItemInBagBase>();
            for (int i = 0; i < allItem.Length; i++)
            {
                PutIn(allItem[i]);
            }

            this.Register((ushort)UIFrame.CommonEvent.putDownItem);

            if (infoText)
            {
                Action<ItemInBagBase> updateBagInfo = (item1) =>
                {
                    var str = "";
                    for (int i = 0; i < Items.Count; i++)
                    {
                        str += Items[i].info + "\n";
                    }
                    str += "容量:" + Items.Count + "/" + size;
                    infoText.text = str;
                };

                onPutIn += updateBagInfo;
                onTakeOut += updateBagInfo;

            }
            //this.Register((ushort)UIFrame.CommonEvent.putDownItem);
        }

        private void Update()
        {

        }

        #region 接口
        public void OnPointerExit(PointerEventData eventData)
        {
            print("鼠标离开背包");
            BagBase.currentExit = this;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            print("鼠标进入背包");
            BagBase.currentEnter = this;

        }

        public virtual void ProcessEvent(Message msg)
        {
            if (msg.id == (ushort)UIFrame.CommonEvent.putDownItem)
            {
                if (((BagBase)msg.arguments[2]).Equals(this))
                {
                    ItemInBagBase item = (ItemInBagBase)msg.arguments[0];
                    BagBase oldBag = ((BagBase)msg.arguments[1]);

                    oldBag.TakeOut(item);
                    if (!this.PutIn(item))
                    {
                        oldBag.PutIn(item);
                    }

                }
            }
        }
        #endregion

        /// <summary>
        /// 放入
        /// </summary>
        /// <param name="item"></param>
        protected virtual bool PutIn(ItemInBagBase item)
        {
            //检查容量,和是否已经装入背包
            if (items.Count < size)
            {
                if (!items.Contains(item))
                {
                    print(name + "装入" + item.info);
                    Items.Add(item);
                    item.bag = this;
                    if(onPutIn!=null)
                    onPutIn(item);
                }
                item.transform.SetParent(transform);
                return true;
            }
            else
            {

                print(name+"容量已满");
                return false;
            }

        }


        /// <summary>
        /// 取出
        /// </summary>
        /// <param name="item"></param>
        public virtual void TakeOut(ItemInBagBase item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                print(name + "取出" + item.info);
                if (onTakeOut != null)
                    onTakeOut(item);
            }
        }
        [Serializable]
        public struct Padding
        {
            public float up, bottom, left, right;

        }
    }
}

