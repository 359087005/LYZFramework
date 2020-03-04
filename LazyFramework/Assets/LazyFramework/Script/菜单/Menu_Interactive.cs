using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Lazy
{
    public class Menu_Interactive : MonoBehaviour, IPointerDownHandler
    {
        private MenuFunctionBase functionBase;
        public MenuFunctionBase FunctionBase
        {
            set
            {
                functionBase = value;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if(Input.GetMouseButtonDown(1))
            {
                EventManager.SendMessage(EventTopic.FUNC_MENU, this, gameObject);
            }
        }
    }
}
