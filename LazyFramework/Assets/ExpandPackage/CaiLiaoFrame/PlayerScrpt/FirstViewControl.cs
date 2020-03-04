
/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 第一人称控制器
************************************************************/

using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using Com.Rainier.Buskit3D;

public enum PlayerControlState
{
    noUse,//不使用
    playerControl,//人物控制
    anim,//人物动画
    aI,//人物自动寻路
}

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(FirstViewControlDataModel)), RequireComponent(typeof(FirstViewControlLogic)), DisallowMultipleComponent]
public class FirstViewControl : MonoBehaviour
{

    /// <summary>    /// 单例公开属性    /// </summary>
    public static FirstViewControl instance { set { } get { return _instance; } }
    private static FirstViewControl _instance;
    [SerializeField]
    private PlayerControlState playerState = PlayerControlState.playerControl;
    [HideInInspector]
    public Transform characterTrans, mainCamTrans;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (characterTrans == null)
        {
            characterTrans = gameObject.transform;
            if (characterTrans.tag != "Player")
                Debug.LogError("请设置第一人称Tag为Player");
        }

        if (mainCamTrans == null)
        {
            mainCamTrans = Camera.main.transform;
            if (mainCamTrans == null)
                Debug.LogError("请设置主摄像机Tag为MainCamera");
        }

        IniQuaternion();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerControlState.playerControl)
        {
            JueSeKongZhi();
        }

    }
    private bool isCanSimpleMove = true, isCanRotate = true, isXuanNiu = false, isCanUpDown = true, isCanScrollView = true;
    private bool isTextInput;
    #region  变量赋值
    public void SetPlayerState(PlayerControlState pcs, bool useRecord = true)
    {
        if (useRecord)
        {
            ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).playerState = (int)pcs;
        }
        else
        {
            playerState = pcs;
        }
    }
    public PlayerControlState PlayerState()
    {
        return playerState;
    }
    public void SetIsCanRotate(bool b, bool useRecord = true)
    {
        if (useRecord)
        {
            ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).isCanRotate = b;
        }
        else
        {
            isCanRotate = b;
        }
    }
    public bool IsCanRotate()
    {
        return isCanRotate;
    }
    public void SetIsCanScrollView(bool b, bool useRecord = true)
    {
        if (useRecord)
        {
            ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).isCanScrollView = b;
        }
        else
        {
            isCanScrollView = b;
        }
    }
    public bool IsCanScrollView()
    {
        return isCanScrollView;
    }
    #endregion
    void JueSeKongZhi()
    {
        if (isCanSimpleMove && !isTextInput)
            //   SimpleMove();
            AddSpeedMove(moveSpeed);
        if (isCanRotate && !isXuanNiu)
            RotateCamera();
        if (isCanUpDown)
            CamUpDown();
        if (isCanScrollView && !isXuanNiu)
            CamViewScale();
    }


    #region  人物移动
    [Header("人物移动")]
    Vector3 movedir;
    [SerializeField, TooltipAttribute("人物移动速度")]
    private float moveSpeed = 0.05f;
    [Header("加速度")]
    [SerializeField]
    private float acceleration = 0.05f;
    float curSpeed = 0;

    /// <summary>
    /// 人物移动
    /// </summary>
    public void SimpleMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            float va = mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView;
            movedir = transform.TransformDirection(new Vector3(h, 0, v).normalized * moveSpeed * va * Time.deltaTime);
            characterTrans.GetComponent<CharacterController>().Move(movedir);

            //有重力的效果
            var velocity = GetComponent<CharacterController>().velocity;
            var currentMovementOffset = velocity * Time.deltaTime;
            var pushDownOffset = Mathf.Max(GetComponent<CharacterController>().stepOffset, new Vector3(currentMovementOffset.x, 0, currentMovementOffset.z).magnitude);

            currentMovementOffset = (-1) * pushDownOffset * Vector3.up;

            GetComponent<CharacterController>().Move(currentMovementOffset);


            FirstViewDataEntity fvde = (FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity;
            fvde.playerPos = characterTrans.localPosition;
        }
    }
    /// <summary>
    /// 人物加速移动
    /// </summary>
    /// <param name="maxSpeed"></param>
    public void AddSpeedMove(float maxSpeed)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            if (curSpeed < maxSpeed)
            {
                curSpeed += Time.deltaTime * acceleration;
            }
            else
            {
                curSpeed = maxSpeed;
            }
            float va = mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView;
            movedir = transform.TransformDirection(new Vector3(h, 0, v).normalized * curSpeed * va * Time.deltaTime);
            characterTrans.GetComponent<CharacterController>().Move(movedir);

            //有重力的效果
            var velocity = GetComponent<CharacterController>().velocity;
            var currentMovementOffset = velocity * Time.deltaTime;
            var pushDownOffset = Mathf.Max(GetComponent<CharacterController>().stepOffset, new Vector3(currentMovementOffset.x, 0, currentMovementOffset.z).magnitude);

            currentMovementOffset = (-1) * pushDownOffset * Vector3.up;

            GetComponent<CharacterController>().Move(currentMovementOffset);


            FirstViewDataEntity fvde = (FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity;
            fvde.playerPos = characterTrans.localPosition;
        }
        else
        {
            curSpeed = 0;
        }
    }
    #endregion

    #region 相机旋转
    [Header("相机旋转")]
    private float XSensitivity = 3f;
    private float YSensitivity = 3f;
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private bool clampVerticalRotation = true;
    private bool smooth = true;
    private float smoothTime = 5f;

    public void IniQuaternion(bool useRecord = true)
    {
        if (useRecord)
        {
            ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).iniQuaternionInt++;
        }
        else
        {
            m_CharacterTargetRot = characterTrans.localRotation;
            m_CameraTargetRot = mainCamTrans.localRotation;
        }

    }
    /// <summary>
    /// 相机旋转
    /// </summary>
    public void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
            if (smooth)
            {
                characterTrans.localRotation = Quaternion.Slerp(characterTrans.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                mainCamTrans.localRotation = Quaternion.Slerp(mainCamTrans.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);

            }
            else
            {
                characterTrans.localRotation = m_CharacterTargetRot;
                mainCamTrans.localRotation = m_CameraTargetRot;
            }

            FirstViewDataEntity fvde = ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity);
            fvde.playerRot = characterTrans.localEulerAngles;
            fvde.cameraRot = mainCamTrans.localEulerAngles;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (smooth)
            {
                IniQuaternion();
            }
        }

    }

    private float MinimumX = -60F;
    private float MaximumX = 60F;
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
    #endregion

    #region 相机上下移动
    [Header("相机上下移动")]
    private int dirNum = 0;//判断中建按下停止不动时 是向下还是向上移动

    private float shangXia;

    [SerializeField, TooltipAttribute("相机最低高度")]
    private float camHeightMin = 0.1f;//相机最低高度
    [SerializeField, TooltipAttribute("相机最高高度")]
    private float camHeightMax = 2f;//相机最高高度


    /// <summary>
    /// 相机上下移动
    /// </summary>
    public void CamUpDown()
    {
        if (Input.GetMouseButton(2))//
        {
            shangXia = shangXia + 0.5f * Input.GetAxis("Mouse Y");
            shangXia = Mathf.Clamp(shangXia, -5.3f, 5.3f);

            mainCamTrans.localPosition = new Vector3(mainCamTrans.localPosition.x, Mathf.Clamp(mainCamTrans.localPosition.y, camHeightMin, camHeightMax), mainCamTrans.localPosition.z);

            mainCamTrans.Translate(0, shangXia * 0.01f, 0, Space.World);


            FirstViewDataEntity fvde = ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity);
            fvde.cameraPos = mainCamTrans.localPosition;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            shangXia = 0;
        }
    }
    #endregion

    #region  相机view值变化
    //[Header("相机视角值变化")]
    private float camViewSpeed = -50;//相机view变化速度
    public void CamViewScale()
    {

        float h = Input.GetAxis("Mouse ScrollWheel");
        if (h != 0)
        {
            float value = Mathf.Clamp(mainCamTrans.GetComponent<Camera>().fieldOfView + camViewSpeed * h, 20, 60);

            DOTween.To(() => mainCamTrans.GetComponent<Camera>().fieldOfView, x => mainCamTrans.GetComponent<Camera>().fieldOfView = x, value, 0.2f).SetEase(Ease.Linear);

            ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).fov = mainCamTrans.GetComponent<Camera>().fieldOfView;
        }
    }


    #endregion

    //摄像机动画
    public void MoveToDestination(Vector3 pos, Vector3 ros, Vector3 cameraRos, float hight, float delay, float time, float View = 60, bool useRecord = false)
    {
        MoveToDestination(pos, ros, new Vector3(mainCamTrans.localPosition.x, hight, mainCamTrans.localPosition.z), cameraRos, delay, time, View, useRecord);

        //CommandManager.instance.ExecuteCommand(this, false, "playermove", playerPos, playerRot, camPos, _camRot, time, delay, fieldvalue);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos">目标点</param>
    /// <param name="ros">目标角度</param>
    /// <param name="cameraRos">相机位置</param>
    /// <param name="hight">高度</param>
    /// <param name="delay">延迟</param>
    /// <param name="View">视角</param>
    /// <param name="speed">速度</param>
    public void MoveToDestinationByPosition(Vector3 pos, Vector3 ros, Vector3 cameraRos, float hight, float delay, float View = 60, float speed = 1, bool useRecord = false)
    {
        float time = Vector3.Distance(characterTrans.position, pos);
        MoveToDestination(pos, ros, cameraRos, hight, delay, time, View, useRecord);
    }

    public Tweener MoveToDestination(Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 _camRot, float delay, float time, float fieldvalue, bool useRecord = false)
    {
        Tweener tt = default(Tweener);
        SetPlayerState(PlayerControlState.anim, useRecord);

        if (mainCamTrans.GetComponent<Camera>().fieldOfView < 59 && Mathf.Abs(mainCamTrans.GetComponent<Camera>().fieldOfView - fieldvalue) > 5)
        {
            DOTween.To(() => mainCamTrans.GetComponent<Camera>().fieldOfView, x =>
            {
                mainCamTrans.GetComponent<Camera>().fieldOfView = x;
                if (useRecord)
                {
                    ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).fov = mainCamTrans.GetComponent<Camera>().fieldOfView;
                }
            }, 60, 1f).SetDelay(delay).SetEase(Ease.InOutQuad);
            delay += 1;
        }
        this.transform.DOLocalMove(playerPos, time).SetRelative(false).SetDelay(delay).SetEase(Ease.InOutQuad);
        this.transform.DOLocalRotate(playerRot, time).SetRelative(false).SetDelay(delay).SetEase(Ease.InOutQuad);
        Camera.main.transform.DOLocalMove(camPos, time).SetRelative(false).SetDelay(delay).SetEase(Ease.InOutQuad);
        Camera.main.transform.DOLocalRotate(_camRot, time).SetRelative(false).SetDelay(delay).SetEase(Ease.InOutQuad);

        if (useRecord)
        {
            DOTween.To(() => transform.localScale, x =>
            {
                ((FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity).playerPos = characterTrans.localPosition;
                ((FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity).playerRot = characterTrans.localEulerAngles;
                ((FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity).cameraPos = mainCamTrans.localPosition;
                ((FirstViewDataEntity)GameObject.FindObjectOfType<FirstViewControlDataModel>().DataEntity).cameraRot = mainCamTrans.localEulerAngles;
            }, Vector3.one, time).SetDelay(delay).SetEase(Ease.InOutQuad);
        }
        tt = DOTween.To(() => mainCamTrans.GetComponent<Camera>().fieldOfView, x =>
        {
            mainCamTrans.GetComponent<Camera>().fieldOfView = x;
            if (useRecord)
            {
                ((FirstViewDataEntity)FirstViewControlDataModel.instance.DataEntity).fov = mainCamTrans.GetComponent<Camera>().fieldOfView;
            }
        }, fieldvalue, time).SetRelative(false).SetDelay(delay).SetEase(Ease.Linear);

        if (smooth)
        {
            tt.OnComplete(() =>
            {
                IniQuaternion(useRecord);
                SetPlayerState(PlayerControlState.playerControl, useRecord);
            });
        }
        return tt;
    }

    public void StopMoveToDestination()
    {
        //停止摄像机动画，重置参数
    }


}

