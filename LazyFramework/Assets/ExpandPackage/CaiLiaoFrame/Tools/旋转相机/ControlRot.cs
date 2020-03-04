



using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

[RequireComponent(typeof(ControlRotDataModel)), RequireComponent(typeof(ControlRotLogic)), DisallowMultipleComponent]
public class ControlRot : MonoBehaviour
{
    public static ControlRot instance;   //zyq
    [Header("相机目标点")]
    public Transform target; //相机一点目标点
    public bool isCanRot = true;
    public float distance = 10.0f;
    public float MinDist = 0.3f;
    public float MaxDist = 10.0f;

    private float xSpeed = 250.0f;
    private float ySpeed = 250.0f;//120

    private bool isAnim;

    private float x = 0.0f;
    private float y = 0.0f;

    bool controlFlag = false;

    CreateUI pre;

    //标记是否能旋转和移动
    public bool isCanMove = true;
    public float moveSpeed = 0.5f;
    void Start()
    {
        instance = this;  //zyq
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //测试
        //MoveToDestination(Vector3.one, Vector3.one, 8, 2, 2);
        //LookAtMoveToDestination(Vector3.one, new Vector3(0,1,-10), 8, 2);
    }


    void LateUpdate()
    {
        if (isAnim)
            return;
        if (isCanRot)
        {
            UpdateRotate();
        }
        if (isCanMove)
        {
            UpdateMove();
        }
    }
    Ray ray;
    RaycastHit hit;
    void UpdateRotate()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 a = transform.eulerAngles;
            a.y += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            a.x -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            transform.rotation = Quaternion.Euler(a.x, a.y, a.z);

            target.transform.rotation = Quaternion.Euler(a.x, a.y, a.z);


            GetComponent<ControlRotDataModel>().entity.cameraRot = transform.localEulerAngles;
            GetComponent<ControlRotDataModel>().entity.targetRot = target.localEulerAngles;
        }

        distance = distance - Input.GetAxis("Mouse ScrollWheel") * 10;

        distance = Mathf.Clamp(distance, MinDist, MaxDist);

        transform.position = target.position - transform.forward * distance;

        GetComponent<ControlRotDataModel>().entity.distance = distance;
        GetComponent<ControlRotDataModel>().entity.cameraPos = transform.localPosition;
    }

    void UpdateMove()
    {

        if (Input.GetMouseButton(1))
        {
            target.Translate(-Input.GetAxis("Mouse X")  * moveSpeed,
                  -Input.GetAxis("Mouse Y")  * moveSpeed,
                0, Space.Self);

            distance = distance - Input.GetAxis("Mouse ScrollWheel") * 10;

            distance = Mathf.Clamp(distance, MinDist, MaxDist);

            transform.position = target.position - transform.forward * distance;

            GetComponent<ControlRotDataModel>().entity.targetPos = target.localPosition;
            GetComponent<ControlRotDataModel>().entity.distance = distance;
            GetComponent<ControlRotDataModel>().entity.cameraPos = transform.localPosition;
        }
    }
    /// <summary>
    /// 摄像机动画
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="targeRot"></param>
    /// <param name="dis"></param>
    /// <param name="delay"></param>
    /// <param name="time"></param>
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
                GetComponent<ControlRotDataModel>().entity.targetPos = target.localPosition;
                GetComponent<ControlRotDataModel>().entity.targetRot = target.localEulerAngles;
                GetComponent<ControlRotDataModel>().entity.cameraRot = transform.localEulerAngles;
            }, Vector3.one, time).SetDelay(delay);
        }

        t = DOTween.To(() => distance, x =>
        {
            distance = x;
            transform.position = target.position - transform.forward * distance;
            if (useRecord)
            {
                GetComponent<ControlRotDataModel>().entity.distance = distance;
                GetComponent<ControlRotDataModel>().entity.cameraPos = transform.localPosition;
            }
        }  , dis, time).SetDelay(delay).OnComplete(() =>
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
                GetComponent<ControlRotDataModel>().entity.targetPos = target.localPosition;
                GetComponent<ControlRotDataModel>().entity.targetRot = target.localEulerAngles;
                GetComponent<ControlRotDataModel>().entity.cameraRot = transform.localEulerAngles;
                GetComponent<ControlRotDataModel>().entity.cameraPos = transform.localPosition;
            }, Vector3.one, time).SetEase(Ease.InOutQuad);
        }

        t = DOTween.To(() => distance, x =>
        {
            distance = x;
            if (useRecord)
            {
                GetComponent<ControlRotDataModel>().entity.distance = distance;
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



}
















