/************************************************************
? Copyright (C), 2007-2016,BJ Rainier Tech. Co., Ltd.
? FileName: RotateObject.cs
? Author:haibo Version :1.0 Date: 2016-1-1
? Description:旋钮
************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(XuanNiuDataModel)), RequireComponent(typeof(XuanNiuLogic)), DisallowMultipleComponent]
public class XuanNiu : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public enum RotateAxle
    {
        x_Axis,
        y_Axis,
        z_Axis,
    }
    [HideInInspector]
    public Transform target;
    public bool isRot;
    public float maxAngle = Mathf.Infinity;
    public float minAngle = -Mathf.Infinity;
    public RotateAxle rotateAxle;
    public float roteSpeed = -0.01f;
    public float hasrotAngles;//已经转过的角度
    private Vector3 NextMousePos;

    public void init(bool isRotate, float maxAngle, float minAngle, float speed, RotateAxle axle)
    {
        this.maxAngle = maxAngle;
        this.minAngle = minAngle;
        this.roteSpeed = speed;
        this.rotateAxle = axle;
        this.isRot = isRotate;
    }
    
    void Start()
    {
        target = gameObject.transform;
      
    }
   public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        NextMousePos = Input.mousePosition;
    }
    
   public void  OnDrag(PointerEventData eventData)
    {
        RotTarget();
    }
    
   public void OnEndDrag(PointerEventData eventData)
    {

    }
   
    public void RotTarget()
    {
        Vector3 offset = Input.mousePosition - NextMousePos;
        Vector3 TargetToScreen = Camera.main.WorldToScreenPoint(target.position);
        Vector3 dir1 = offset;
        Vector3 dir2 = NextMousePos - TargetToScreen;
        float direction = Vector3.Cross(dir2, dir1).z;
        if (isRot)
        {
            switch (rotateAxle)
            {

                case RotateAxle.x_Axis:
                    hasrotAngles += direction * roteSpeed;

                    hasrotAngles = Mathf.Clamp(hasrotAngles, minAngle, maxAngle);
                    target.localEulerAngles = new Vector3(hasrotAngles, target.localEulerAngles.y, target.localEulerAngles.z);
                    break;
                case RotateAxle.y_Axis:
                    hasrotAngles += direction * roteSpeed;

                    hasrotAngles = Mathf.Clamp(hasrotAngles, minAngle, maxAngle);
                    target.localEulerAngles = new Vector3(target.localEulerAngles.x, hasrotAngles, target.localEulerAngles.z);
                    break;
                case RotateAxle.z_Axis:

                    hasrotAngles += direction * roteSpeed;

                    hasrotAngles = Mathf.Clamp(hasrotAngles, minAngle, maxAngle);
                    target.localEulerAngles = new Vector3(target.localEulerAngles.x, target.localEulerAngles.y, hasrotAngles);

                    break;
            }

            GetComponent<XuanNiuDataModel>().entity.rot = target.localEulerAngles;
            GetComponent<XuanNiuDataModel>().entity.hasrotAngles = hasrotAngles;
        }
           
        NextMousePos = Input.mousePosition;

    }



}
