/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ItemInBag
* 创建日期：2019-12-13 15:20:20
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class ItemInBagBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IProcessEvent
    {
        /// <summary>
        /// 物品描述
        /// </summary>
        public string info;
        private Vector3 beginPos;
        public BagBase bag;
        private Action<ItemInBagBase> onPutDown, onPickUp;
        private Transform rootPanel;

        #region unity life 
        protected virtual void Start()
        {
            rootPanel = UIFrame.Instance.transform.Find("UIPanels");


        }
        #endregion

        #region 接口

        public void OnBeginDrag(PointerEventData eventData)
        {
            beginPos = GetMousePosition();
        }

        public void OnDrag(PointerEventData eventData)
        {

            var offset = GetMousePosition() - beginPos;
            beginPos = GetMousePosition();
            transform.Translate(offset, Space.World);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {

                this.GetComponent<Graphic>().raycastTarget = false;

                PickUp();
            }
            else
            {

            }

        }
        public void OnPointerUp(PointerEventData eventData)
        {

            this.GetComponent<Graphic>().raycastTarget = true;

            PutDown();


        }

        public virtual void ProcessEvent(Message msg)
        {

        }
        #endregion

        #region 行为

        private static Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        protected virtual void PickUp()
        {
            print("拿起" + this.info);

            transform.SetParent(rootPanel);
            if (onPickUp != null)
            {
                onPickUp(this);
            }
        }

        protected virtual void PutDown()
        {
            print("放下" + this.info);

            MessageCenter.SendEvent(new Message((ushort)UIFrame.CommonEvent.putDownItem, this, bag, BagBase.currentEnter));
            if (onPutDown != null)
            {
                onPutDown(this);
            }
        }

        public virtual void Deleted()
        {
            if (bag)
            {
                if (bag.Items.Contains(this))
                {
                    bag.TakeOut(this);
                }
                Destroy(gameObject);
            }
        }
        #endregion
    }
}

