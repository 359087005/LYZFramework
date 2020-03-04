using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机一些基本操作
/// </summary>
public class CameraOperation : MonoBehaviour
{
    [SerializeField] Camera curCamera;
    [SerializeField] Transform target;
    [SerializeField] float distance = 10;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] Vector3 curTarget;
    Vector3 curTargetRot;
    Vector3 oldTargetRot;
    Vector3 oldTargetPos;
    bool isBegin;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
          
            AutoCloseUp(3, target, ()=> { Debug.Log("1515"); });
        }
    }
    public void SetTarget(Transform tar)
    {
        target = tar;
    }
    public void CloseUp(Action onFinish=null)
    {
        if (isBegin) return;
        isBegin = true;
        oldTargetPos = curCamera.transform.position;
        oldTargetRot = curCamera.transform.eulerAngles;
        curTarget = target.position;
        curTargetRot = target.eulerAngles;
        StartCoroutine(LerpToTargetIE(onFinish));
    }
    public void CloseUp(float waitToBack, Action onBackFinish = null)
    {
        if (isBegin) return;
        isBegin = true;
        oldTargetPos = curCamera.transform.position;
        oldTargetRot = curCamera.transform.eulerAngles;
        curTarget = target.position;
        curTargetRot = target.eulerAngles;
        StartCoroutine(LerpToTargetIE(
             () => {
                 StartCoroutine(WaitToBack(waitToBack, () =>
                 {
                     if (onBackFinish != null)
                     {
                         onBackFinish.Invoke();
                     }
                 }));
             }));
    }
    public void CloseUp(float waitToBack,Transform tar,Action onBackFinish =null)
    {
        if (isBegin) return;
        SetTarget(tar);
        isBegin = true;
        oldTargetPos = curCamera.transform.position;
        oldTargetRot = curCamera.transform.eulerAngles;
        curTarget = target.position;
        curTargetRot = target.eulerAngles;
        StartCoroutine(LerpToTargetIE(
            () => {
                StartCoroutine(WaitToBack(waitToBack,()=>
                {
                    if(onBackFinish!=null)
                    {
                        onBackFinish.Invoke();
                    }
                }));
            }));
    }
    public void GoBack(Action onFinish = null)
    {
        if (isBegin) return;
        curTarget = oldTargetPos;
        curTargetRot = oldTargetRot;
        isBegin = true;
        StartCoroutine(LerpToTargetIE(onFinish));
    }
    public void AutoCloseUp(float waitToBack, Transform tar, Action onBackFinish = null)
    {
        if (isBegin) return;
        isBegin = true;
        oldTargetPos = curCamera.transform.position;
        oldTargetRot = curCamera.transform.eulerAngles;
        curTarget = tar.position;
        StartCoroutine(AutoLerpToTargetIE(
             () => {
                 StartCoroutine(WaitToBack(waitToBack, () =>
                 {
                     if (onBackFinish != null)
                     {
                         onBackFinish.Invoke();
                     }
                 }));
             }));
    }
    #region 内部方法
   
    IEnumerator WaitToBack(float time,Action onBackFinish=null)
    {
        yield return new WaitForSeconds(time);
        GoBack(onBackFinish);
    }
    IEnumerator LerpToTargetIE(Action onFinish=null)
    {
        if(curTarget==null)
        {
            Debug.Log("缺少目标物体");
            isBegin = false;
        }
        while(isBegin)
        {
            curCamera.transform.position = Vector3.Lerp(curCamera.transform.position, curTarget,moveSpeed*Time.deltaTime);
            curCamera.transform.rotation = Quaternion.Lerp(curCamera.transform.rotation, Quaternion.Euler(curTargetRot), moveSpeed*Time.deltaTime); 
            if (Vector3.Distance(curCamera.transform.position, curTarget)<=0.1f)
            {
                curCamera.transform.position = curTarget;
                curCamera.transform.eulerAngles = curTargetRot;
                isBegin = false;
                if(onFinish!=null)
                {
                    onFinish.Invoke();
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator AutoLerpToTargetIE(Action onFinish = null)
    {
        if (curTarget == null)
        {
            Debug.Log("缺少目标物体");
            isBegin = false;
        }
        while (isBegin)
        {
            curCamera.transform.position = Vector3.Lerp(curCamera.transform.position, curTarget,moveSpeed*Time.deltaTime);
            curCamera.transform.LookAt(curTarget);
            if (Vector3.Distance(curCamera.transform.position, curTarget) <= distance)
            {
                //curCamera.transform.position = curTarget;
                isBegin = false;
                if (onFinish != null)
                {
                    onFinish.Invoke();
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
