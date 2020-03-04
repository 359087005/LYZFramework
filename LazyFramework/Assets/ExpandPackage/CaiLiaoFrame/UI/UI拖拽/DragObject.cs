/************************************************************************************
 Copyright (C), 2007-2016,BJ Rainier Tech. Co., Ltd.
 FileName: GenerateFoldersAndAddFileHead.cs
 Author:zqx        Version :1.0     Date: 2017-08-21     
 Description:移动UI和三维物体，但是三维物体能穿墙，且不会回弹
/************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool canMove = true;
    public enum ObjectType
    {
        Object3D,
        Object2D
    };
    private GameObject obj;//要移动的三维物体
    Vector3 screenPosition;//三维物体的屏幕坐标
    Vector3 mScreenPosition; //鼠标屏幕坐标
    Vector3 offset; //获得鼠标和对象之间的偏移量
    int i = 0;
    void Start()
    {
        scale = transform.localScale.x;
        if (obj == null)
            obj = gameObject;
    }

    void Update()
    {
        if (canMove)
            Move();
    }
    void Move()
    {
        switch (objectType)
        {
            case ObjectType.Object3D:
                Drag3DObject();
                break;
            case ObjectType.Object2D:
                Drag2DObject();
                break;
        }
    }
    #region  拖动三维物体
    void Drag3DObject()
    {
        if (Input.GetMouseButtonDown(0) && i == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)&&hit.transform.name == obj.name)
            {
                if (FirstViewControl.instance)
                {
                    save_IsCanRotate = FirstViewControl.instance.IsCanRotate();
                    FirstViewControl.instance.SetIsCanRotate(false);
                }

                //转换对象到当前屏幕位置
                screenPosition = Camera.main.WorldToScreenPoint(obj.transform.position);
                //print("screenPosition: " + screenPosition);

                //鼠标屏幕坐标
                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                //print("mScreenPosition: " + mScreenPosition);

                //获得鼠标和对象之间的偏移量,拖拽时相机应该保持不动
                offset = obj.transform.position - Camera.main.ScreenToWorldPoint(mScreenPosition);
                //print("offset: " + offset);

                print(offset);
                i = 1;
            }
        }

        if (Input.GetMouseButton(0) && i == 1)
        {
            //鼠标屏幕上新位置
            mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            // 对象新坐标 
            obj.transform.position = Camera.main.ScreenToWorldPoint(mScreenPosition) + offset;

            GetComponent<DragObjectDataModel>().entity.pos = obj.transform.localPosition;

        }
        if (Input.GetMouseButtonUp(0) && i == 1)
        {
            i = 0;
            if (FirstViewControl.instance)
                FirstViewControl.instance.SetIsCanRotate(save_IsCanRotate);
        }
       
    }
    #endregion

    #region 拖动UI
    public ObjectType objectType = ObjectType.Object2D;
    private Vector3 screenPoint;

    [Range(0, 1)]
    public float minX = 0, maxX = 1, minY = 0, maxY = 1;

    public bool useSuoFang = false;
    public float suoFangMin = 0.5f, suoFangMax = 2f;

    public float suoFangSpeed = 1f;
    public float scale;

    void Drag2DObject()
    {
        if (objectType == ObjectType.Object3D)
            return;
        if (useSuoFang && pointEnter)
        {
            float h = Input.GetAxis("Mouse ScrollWheel");
            if (h != 0)
            {
                scale = Mathf.Clamp(scale + suoFangSpeed * h, suoFangMin, suoFangMax);
                obj.transform.localScale = Vector3.one * scale;
                GetComponent<DragObjectDataModel>().entity.scale = scale;
            }
        }
    }


    private bool pointEnter;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectType == ObjectType.Object3D)
            return;
        pointEnter = true;
        if (useSuoFang)
        {
            if (FirstViewControl.instance)
            {
                save_IsCanScrollView = FirstViewControl.instance.IsCanScrollView();
                FirstViewControl.instance.SetIsCanScrollView(false);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectType == ObjectType.Object3D)
            return;
        pointEnter = false;
        if (useSuoFang)
        {
            if (FirstViewControl.instance)
                FirstViewControl.instance.SetIsCanScrollView(save_IsCanScrollView);
        }
    }

    bool save_IsCanRotate, save_IsCanScrollView;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (objectType == ObjectType.Object3D)
            return;
        offset = Input.mousePosition - obj.GetComponent<RectTransform>().position;

        if (FirstViewControl.instance)
        {
            save_IsCanRotate = FirstViewControl.instance.IsCanRotate();
            FirstViewControl.instance.SetIsCanRotate(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (objectType == ObjectType.Object3D)
            return;
        obj.GetComponent<RectTransform>().position = Input.mousePosition - offset;

        GetComponent<DragObjectDataModel>().entity.pos = obj.transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (objectType == ObjectType.Object3D)
            return;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        if (FirstViewControl.instance)
            FirstViewControl.instance.SetIsCanRotate(save_IsCanRotate);

        if (obj.GetComponent<RectTransform>().position.x < minX * screenWidth)
        {
            obj.transform.DOMoveX(minX * screenWidth, 0.1f);
        }
        if (obj.GetComponent<RectTransform>().position.x > maxX * screenWidth)
        {
            obj.transform.DOMoveX(maxX * screenWidth, 0.1f);
        }
        if (obj.GetComponent<RectTransform>().position.y < minY * screenHeight)
        {
            obj.transform.DOMoveY(minY * screenHeight, 0.1f);
        }
        if (obj.GetComponent<RectTransform>().position.y > maxY * screenHeight)
        {
            obj.transform.DOMoveY(maxY * screenHeight, 0.1f);
        }

        GetComponent<DragObjectDataModel>().entity.pos = obj.transform.localPosition;
    }
    public void Reset()
    {
        scale = 1f;
        obj.transform.localScale = Vector3.one * scale;
        GetComponent<DragObjectDataModel>().entity.scale = scale;
    }
    #endregion
}

