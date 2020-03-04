using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScalePoint : MonoBehaviour,IDragHandler
{
    [SerializeField] UIScaleDragType dragType;
    [SerializeField] UIScaleOperation scaleOperation;
    public void SetDragType(UIScaleDragType uIScaleDragType)
    {
        dragType = uIScaleDragType;
    }
    void Start()
    {
        if(transform.parent.GetComponent<UIScaleOperation>()!=null)
        {
            scaleOperation = transform.parent.GetComponent<UIScaleOperation>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch(dragType)
        {
            case UIScaleDragType.up:

                break;
        }
    }
}
public enum UIScaleDragType
{
    none,
    up,
    down,
    left,
    right,
    rightUp,
    rightDown,
    leftUp,
    leftDown
}

