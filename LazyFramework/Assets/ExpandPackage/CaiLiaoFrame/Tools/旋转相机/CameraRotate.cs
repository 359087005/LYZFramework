
/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: XuanNiu.cs
  Author:汪海波       Version :1.0          Date: 
  Description:旋转摄像机
************************************************************/


using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

[RequireComponent(typeof(CameraRotateDataModel)), RequireComponent(typeof(CameraRotateLogic)), DisallowMultipleComponent]
public class CameraRotate : MonoBehaviour
{
    public static CameraRotate _instance;
    public static CameraRotate instance { set { } get { return _instance; } }
    [Header("相机目标点")]
    [SerializeField, TooltipAttribute("相机旋转的中心点")]
    public Transform target; //相机旋转的中心点
    [SerializeField, TooltipAttribute("是否可以旋转")]
    public bool isCanRot = true;
    [SerializeField, TooltipAttribute("相机与中心点的实时距离")]
    public float distance = 5.0f;
    [SerializeField, TooltipAttribute("最小距离")]
    float MinDist = 0.3f;
    [SerializeField, TooltipAttribute("最大距离")]
    float MaxDist = 10.0f;

    [HideInInspector]
    float rotSpeed = 5f;
    [HideInInspector]
    float scrollSpeed = 5f;

    [SerializeField, TooltipAttribute("最小俯仰角")]
    float xRotMinLimit = -20;
    [SerializeField, TooltipAttribute("最大俯仰角")]
    float xRotMaxLimit = 80;

    [SerializeField, TooltipAttribute("是否可以移动")]
    public bool isCanMove = true;
    [SerializeField, TooltipAttribute("移动速度")]
    public float moveSpeed = 0.5f;



    [SerializeField, TooltipAttribute("是否使用裁剪")]
    public bool useClip = false;
    [SerializeField, TooltipAttribute("相机观察的物体")]
    public Transform viewTrans; //相机观察的物体
    private Ray m_Ray;
    private float sphereCastRadius = 0.1f;
    [SerializeField, TooltipAttribute("使用裁剪忽略标签")]
    string dontClipTag = "dontClipTag";
    private RaycastHit[] m_Hits;
    private RayHitComparer m_RayHitComparer;
    private float m_CurrentDist;
    bool hitSomething;
    bool once, once1;
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (cam_BG != null)
            m_fcam_BGScal_X = cam_BG.transform.localScale.x;

        m_RayHitComparer = new RayHitComparer();

        m_CurrentDist = distance;
    }

    void Update()
    {
        // Debug.Log(distance);
        if (cam_BG != null)
            AdaptiveScreen();
    }

    void LateUpdate()
    {
        if (isAnim)
            return;
        if (Battlehub.RTHandles.BaseHandle.m_draggingTool != null)
            return;
        if (isCanRot)
        {
            UpdataRot();
        }
        if (isCanMove)
        {
            UpdateMove();
        }
        if (useClip)
        {
            if (hitSomething)
            {
                if (!once)
                {
                    // Debug.Log("进入");
                    preHitName = m_Hits[num].collider.gameObject.name;
                    curHitName = m_Hits[num].collider.gameObject.name;
                    once = true;
                    target.position = viewTrans.position;
                    transform.LookAt(target.position);
                    target.transform.rotation = transform.rotation;

                    GetComponent<CameraRotateDataModel>().entity.targetPos = target.localPosition;
                    GetComponent<CameraRotateDataModel>().entity.targetRot = target.localEulerAngles;
                    GetComponent<CameraRotateDataModel>().entity.cameraRot = transform.localEulerAngles;
                }
                else
                {
                    curHitName = m_Hits[num].collider.gameObject.name;
                    //遮挡的物体为不同的物体时，需要调整射线检测长度
                    if (curHitName != preHitName)
                    {
                        //Debug.Log("curHitName=" + curHitName + "preHitName=" + preHitName);
                        preHitName = curHitName;
                        once1 = true;
                    }
                }
            }
            else
            {
                if (once)
                {
                    // Debug.Log("退出");
                    once = false;
                }
            }
            transform.position = target.position - transform.forward * m_CurrentDist;
            GetComponent<CameraRotateDataModel>().entity.cameraPos = transform.localPosition;
        }
        else
        {
            transform.position = target.position - transform.forward * distance;
            GetComponent<CameraRotateDataModel>().entity.cameraPos = transform.localPosition;
        }
    }

    bool downMouse;
    Vector3 hitPos;
    int num;
    string preHitName = "null", curHitName = "null";
    void UpdataRot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            downMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            downMouse = false;
        }
        if (Input.GetMouseButton(0) && downMouse)
        {
            Vector3 a = transform.eulerAngles;
            a.y += Input.GetAxis("Mouse X") * rotSpeed;
            a.x -= Input.GetAxis("Mouse Y") * rotSpeed;

            a.x = Mathf.Clamp(TiaoZhengJiaoDu(a.x), xRotMinLimit, xRotMaxLimit);

            transform.rotation = Quaternion.Euler(a.x, a.y, 0);
            target.transform.rotation = Quaternion.Euler(a.x, a.y, 0);


            GetComponent<CameraRotateDataModel>().entity.cameraRot = transform.localEulerAngles;
            GetComponent<CameraRotateDataModel>().entity.targetRot = target.localEulerAngles;
        }

        distance = distance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, MinDist, MaxDist);

        GetComponent<CameraRotateDataModel>().entity.distance = distance;


        if (!useClip)
            return;
        //遮挡前移
        float targetDist = distance;
        Vector3 dir = viewTrans.position - transform.position;
        m_Ray.origin = viewTrans.position + dir.normalized * sphereCastRadius;
        m_Ray.direction = -dir.normalized;

        var cols = Physics.OverlapSphere(m_Ray.origin, sphereCastRadius);

        bool initialIntersect = false;
        hitSomething = false;

        // loop through all the collisions to check if something we care about
        for (int i = 0; i < cols.Length; i++)
        {
            if ((!cols[i].isTrigger) && !(cols[i].gameObject.CompareTag(dontClipTag)))
            {
                initialIntersect = true;
                break;
            }
        }

        // if there is a collision
        if (initialIntersect)
        {
            m_Ray.origin += dir.normalized * sphereCastRadius;

            // do a raycast and gather all the intersections
            if (once1)
            {
                once1 = false;
                m_Hits = Physics.RaycastAll(m_Ray, m_CurrentDist - sphereCastRadius);
            }
            else
            {
                m_Hits = Physics.RaycastAll(m_Ray, distance - sphereCastRadius);
            }
        }
        else
        {
            // if there was no collision do a sphere cast to see if there were any other collisions
            if (once1)
            {
                once1 = false;
                m_Hits = Physics.SphereCastAll(m_Ray, sphereCastRadius, m_CurrentDist + sphereCastRadius);
            }
            else
            {
                m_Hits = Physics.SphereCastAll(m_Ray, sphereCastRadius, distance + sphereCastRadius);
            }

        }

        // sort the collisions by distance
        Array.Sort(m_Hits, m_RayHitComparer);

        // set the variable used for storing the closest to be as far as possible
        float nearest = Mathf.Infinity;

        // loop through all the collisions
        hitPos = transform.position;
        num = 0;
        for (int i = 0; i < m_Hits.Length; i++)
        {
            if (m_Hits[i].distance < nearest && (!m_Hits[i].collider.isTrigger) &&
                !(m_Hits[i].collider.gameObject.CompareTag(dontClipTag)))
            {
                // change the nearest collision to latest
                nearest = m_Hits[i].distance;
                hitPos = m_Hits[i].point;
                hitSomething = true;
                num = i;
            }
        }

        if (hitSomething)
        {
            targetDist = Math.Abs(Vector3.Distance(target.position, hitPos));
            //Debug.DrawRay(m_Ray.origin, (hitPos - viewTrans.position), Color.red);
        }
        else
        {
            targetDist = distance;
        }

        m_CurrentDist = targetDist;
        m_CurrentDist = Mathf.Clamp(m_CurrentDist, MinDist, distance);

        // if (!hitSomething)
        //Debug.DrawRay(m_Ray.origin, -dir.normalized * (m_CurrentDist + sphereCastRadius), Color.red);

    }

    // comparer for check distances in ray cast hits
    public class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }

    float TiaoZhengJiaoDu(float angle)
    {
        if (angle > 180)
        {
            return angle - 360;
        }
        if (angle < -180)
        {
            return angle + 360;
        }

        return angle;
    }

    void UpdateMove()
    {
        if (Input.GetMouseButton(1))
        {
            target.Translate(-Input.GetAxis("Mouse X") * moveSpeed,
                  -Input.GetAxis("Mouse Y") * moveSpeed,
                0, Space.Self);

            GetComponent<CameraRotateDataModel>().entity.targetPos = target.transform.localPosition;
        }
    }

    bool isAnim;
    /// <summary>
    /// 摄像机动画
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="targeRot"></param>
    /// <param name="dis"></param>
    /// <param name="delay"></param>
    /// <param name="time"></param>
    /// <param name="useRecord"></param>
    /// <returns></returns>
    public Tweener MoveToDestination(Vector3 targetPos, Vector3 targeRot, float dis, float delay, float time, bool useRecord = false)
    {
        SetDelay(delay, () => isAnim = true);

        Tweener t = default(Tweener);

        target.transform.DOLocalMove(targetPos, time).SetDelay(delay);
        target.transform.DOLocalRotate(targeRot, time).SetDelay(delay);
        transform.DOLocalRotate(targeRot, time).SetDelay(delay);
        if (useRecord)
        {
            DOTween.To(() => transform.localScale, x =>
            {
                GetComponent<CameraRotateDataModel>().entity.targetPos = target.localPosition;
                GetComponent<CameraRotateDataModel>().entity.targetRot = target.localEulerAngles;
                GetComponent<CameraRotateDataModel>().entity.cameraRot = transform.localEulerAngles;
            }, Vector3.one, time).SetDelay(delay).SetEase(Ease.InOutQuad);
        }

        t = DOTween.To(() => distance, x =>
        {
            distance = x;
            transform.position = target.position - transform.forward * distance;
            if (useRecord)
            {
                GetComponent<CameraRotateDataModel>().entity.distance = distance;
                GetComponent<CameraRotateDataModel>().entity.cameraPos = transform.localPosition;
            }
        }
        , dis, time).SetDelay(delay).OnComplete(() =>
       {
           isAnim = false;
       });

        return t;
    }

    /// <summary>
    /// 朝向物体的摄像机动画
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="cameraStartPos"></param>
    /// <param name="dis"></param>
    /// <param name="time"></param>
    /// <param name="useRecord"></param>
    /// <returns></returns>
    public Tweener LookAtMoveToDestination(Vector3 targetPos, Vector3 cameraStartPos, float dis, float time, bool useRecord = false)
    {
        isAnim = true;
        Tweener t = default(Tweener);

        target.transform.position = targetPos;
        Quaternion dir = Quaternion.LookRotation(target.transform.position - transform.position);
        target.transform.eulerAngles = dir.eulerAngles;
        Vector3 pos = target.transform.position - (target.transform.position - cameraStartPos).normalized * dis;
        transform.DOMove(pos, time).SetEase(Ease.InOutQuad);
        transform.DORotate(dir.eulerAngles, time).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            isAnim = false;
        });
        if (useRecord)
        {
            DOTween.To(() => transform.localScale, x =>
            {
                GetComponent<CameraRotateDataModel>().entity.targetPos = target.localPosition;
                GetComponent<CameraRotateDataModel>().entity.targetRot = target.localEulerAngles;
                GetComponent<CameraRotateDataModel>().entity.cameraRot = transform.localEulerAngles;
                GetComponent<CameraRotateDataModel>().entity.cameraPos = transform.localPosition;
            }, Vector3.one, time).SetEase(Ease.InOutQuad);
        }

        t = DOTween.To(() => distance, x =>
        {
            distance = x;
            if (useRecord)
            {
                GetComponent<CameraRotateDataModel>().entity.distance = distance;
            }
        }, dis, time);

        return t;
    }

    void SetDelay(float delay, Action action)
    {
        StartCoroutine(DelayAction(delay, action));
    }
    IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }


    public void StopAnim()
    {
        isCanMove = true;
        isCanRot = true;

        target.DOKill();
        Camera.main.transform.DOKill();
    }

    //摄像机背景自适应
    float m_fScreenWidth = 960f;
    float m_fScreenHeight = 540f;
    float m_fcam_BGScal_X = 4.11f;
    float m_fScaleWidth;
    float m_fScaleHeight;
    [HideInInspector]
    public GameObject cam_BG;//摄像机背景
    float m_fhorizontal_Scale;//面片水平压缩比
    void AdaptiveScreen()
    {
        m_fScaleWidth = Screen.width / m_fScreenWidth;
        m_fScaleHeight = Screen.height / m_fScreenHeight;

        float f1 = ((960f / 540f)) * Screen.height;
        m_fhorizontal_Scale = Screen.width / f1;

        if (cam_BG != null)
            cam_BG.transform.localScale = new Vector3(m_fcam_BGScal_X * m_fhorizontal_Scale * (1 + 1.4f * (0.25f)), cam_BG.transform.localScale.y, cam_BG.transform.localScale.z);

        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(m_fScaleWidth, m_fScaleHeight, 1));
    }
}




















