
/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: XuanNiu.cs
  Author:汪海波       Version :1.0          Date: 
  Description:旋转物体
************************************************************/

using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(RotateObjectDataModel)), RequireComponent(typeof(RotateObjectLogic)), DisallowMultipleComponent]
public class RotateObject : MonoBehaviour {


    public Camera camera;
    public GameObject model;
    
    public bool isCanRotate = true;
    public float rotateSpeed = 10;

    public bool isCanScrollView = true;
    public float camViewSpeed = -200;//相机view变化速度
    public float minDistance=1, maxDistance=10;

    public bool isCanMove = false;
    public float moveSpeed = 1.8f;
	// Use this for initialization
	void Start () {

	}
	
	void Update () {

        if (isCanRotate)
        {
            RotModel(); 
        }
        if(isCanMove)
        {
            MoveModel();
        }
        if (isCanScrollView)
        {
            ViewModel();
        }
     
	}

    //物体旋转
    void RotModel()
    {
        if (Input.GetMouseButton(0)) {
            model.transform.RotateAround(model.transform.position, camera.transform.up, -rotateSpeed * Input.GetAxis("Mouse X"));
            model.transform.RotateAround(model.transform.position, camera.transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));

            GetComponent<RotateObjectDataModel>().entity.rot = model.transform.localEulerAngles;
            
        }
    }

    Vector3 preBtnPos, btnPos;
    //物体移动
    void MoveModel()
    {
        if(Input.GetMouseButtonDown(2))
        {
            preBtnPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            btnPos = Input.mousePosition;
            var rot = Quaternion.Euler(camera.transform.eulerAngles);
            var pos = rot * (new Vector3(1 * distance * (btnPos - preBtnPos).x * moveSpeed / 1000f, 1 * distance * (btnPos - preBtnPos).y * moveSpeed / 1000f, 0)) + model.transform.position;
            model.transform.position = pos;

            preBtnPos = btnPos;

            GetComponent<RotateObjectDataModel>().entity.pos = model.transform.localPosition;
        }
    }
    [HideInInspector]
    public float distance;
    //物体拉近拉远
    public void ViewModel()
    {
        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            distance = Mathf.Clamp((distance - Input.GetAxis("Mouse ScrollWheel")),minDistance,maxDistance);
            var rotation = Quaternion.LookRotation(model.transform.position - camera.transform.position);
            model.transform.position = rotation * (new Vector3(0, 0, distance)) + camera.transform.position;

            GetComponent<RotateObjectDataModel>().entity.pos = model.transform.localPosition;
        }
        else
        {
            distance = Vector3.Distance(camera.transform.position,model.transform.position);

            GetComponent<RotateObjectDataModel>().entity.distance = distance;
        }
      
    }
}
