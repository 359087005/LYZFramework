/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ProcessControl
* 创建日期：2019-12-2 14:05:25
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：物体监听事件
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Lazy
{
    public delegate void ListenEventHander(GameObject go);
    public delegate void OnClickPressingHandler(GameObject go, float _delayTime, UnityAction unityAction = null);
    public partial class ObjectListen : MonoBehaviour, IPointerClickHandler,IPointerUpHandler,IPointerExitHandler,IPointerEnterHandler,IPointerDownHandler
    {
        #region 鼠标监听
        public ListenEventHander OnClickOnceEvt;
        public ListenEventHander OnClickEvt;
        public OnClickPressingHandler OnClickPressingEvt;
        IEnumerator WaitToTrigger(float _delay,UnityAction unityAction)
        {
            yield return new WaitForSeconds(_delay);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickOnceEvt != null)
            {
                OnClickOnceEvt.Invoke(gameObject);
                OnClickOnceEvt = null;
            }
            if(OnClickEvt!=null)
            {
                OnClickEvt.Invoke(gameObject);
            }
        }
        public ListenEventHander OnMouseUpEvt;
        public void OnPointerUp(PointerEventData eventData)
        {
            if(OnMouseUpEvt!=null)
            {
                OnMouseUpEvt.Invoke(gameObject);
            }
        }
        public ListenEventHander OnMouseExitEvt;
        public void OnPointerExit(PointerEventData eventData)
        {
            if (OnMouseExitEvt != null)
            {
                OnMouseExitEvt.Invoke(gameObject);
            }
        }
        public ListenEventHander OnMouseEnterEvt;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if(OnMouseEnterEvt!=null)
            {
                OnMouseEnterEvt.Invoke(gameObject);
            }
        }
        public ListenEventHander OnMouseDownEvt;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnMouseDownEvt != null)
            {
                OnMouseDownEvt.Invoke(gameObject);
            }
        }
        #endregion
        #region 触发监听
        public ListenEventHander OnTriggerEnterEvt;
        public void OnTriggerEnter(Collider other)
        {
            if (OnTriggerEnterEvt != null)
            {
                OnTriggerEnterEvt.Invoke(other.gameObject);
            }
        }
        public ListenEventHander OnTriggerStayEvt;
        public void OnTriggerStay(Collider other)
        {
            if (OnTriggerExitEvt != null)
            {
                OnTriggerStayEvt.Invoke(other.gameObject);
            }
        }
        public ListenEventHander OnTriggerExitEvt;
        public void OnTriggerExit(Collider other)
        {
            if (OnTriggerExitEvt != null)
            {
                OnTriggerExitEvt.Invoke(other.gameObject);
            }
        }
        #endregion
    }
}
