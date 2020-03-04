using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScaleOperation : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject operationButton;
    RectTransform mySelf;
    [SerializeField] List<UIScalePoint> operationPoint = new List<UIScalePoint>();
    public void Update()
    {
       
    }
    public void Start()
    {
        if(GetComponent<RectTransform>()!=null)
        {
            mySelf = this.GetComponent<RectTransform>();
        }
    }
    public void ShowScaleFunc()
    {
        //InstanceObject();
    }
    public void ControlScale()
    {
        mySelf.localScale = new Vector2(mySelf.localScale.x, mySelf.localScale.y) ;
    }
    public void InstanceObject(RectTransform rectTransform,UIScaleDragType uIScaleDragType)
    {
        GameObject go = Instantiate(operationButton);
        go.AddComponent<RectTransform>();
        if(go.GetComponent<UIScalePoint>()==null)
        {
            go.AddComponent<UIScalePoint>();
        }
        go.SetActive(true);
        go.transform.parent = transform;
        operationPoint.Add(go.GetComponent<UIScalePoint>());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowScaleFunc();
    }
    
}
