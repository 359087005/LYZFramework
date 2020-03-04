using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]private RectTransform m_RectTransform;
    void Start()
    {
        //m_RectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 vec;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out vec);
        m_RectTransform.position = vec;   
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
    }

}
