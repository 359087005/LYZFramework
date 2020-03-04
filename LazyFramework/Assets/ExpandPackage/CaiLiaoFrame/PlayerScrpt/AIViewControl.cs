/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 当需要应用自动寻路时的四视角控制,
************************************************************/

using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;

public class AIViewControl : MonoBehaviour
{
    private static AIViewControl _instance;
    public static AIViewControl instance { set { } get { return _instance; } }

    [HideInInspector]
    public Transform playerTrans, mainCamTrans;
    [SerializeField, TooltipAttribute("从寻路切换到常规dotween动画的最小距离")]
    public float min_Distance;//从寻路切换到常规dotween动画的最小距离

    private UnityEngine.AI.NavMeshAgent player_Nav;//自动寻路组件
    private bool isXunLuIng;//是否正在寻路

    private List<Vector3> targetPosList = new List<Vector3>();//自动寻路目标点

    public delegate void MoveToDestinationAction();
    private MoveToDestinationAction moveToAction;
    /// <summary>
    /// 自动寻路房间
    /// </summary>
    public enum Room
    {
        aRoom,
        bRoom,
        cRoom,
        dRoom,
    }
    private Room curRoom = Room.aRoom;
    void Awake()
    {

        _instance = this;
    }
    void Start()
    {

        player_Nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (player_Nav == null)
        {
            Debug.LogWarning("请给控制器添加自动寻路组件");
        }

        if (playerTrans == null)
        {
            playerTrans = gameObject.transform;
            if (playerTrans.tag != "Player")
                Debug.LogError("请设置第一人称Tag为Player");
        }

        if (mainCamTrans == null)
        {
            mainCamTrans = Camera.main.transform;
            if (mainCamTrans == null)
                Debug.LogError("请设置主摄像机Tag为MainCamera");
        }

        if (player_Nav) player_Nav.enabled = false;

    }

    void Update()
    {
        if (FirstViewControl.instance.PlayerState() == PlayerControlState.aI)
        {
            ZiDingXunLu();
        }
    }

    void ZiDingXunLu()
    {
        if (player_Nav && isXunLuIng)
        {
            float dis = Math.Abs(Vector3.Distance(transform.position, targetPosList[0]));
            if (targetPosList.Count > 1)
            {
                // 当存在多点自动寻路时，实时判断离目标点距离，当小于0.2，切换到下一个目标点。
                if (dis <= 0.2f)
                {
                    player_Nav.destination = targetPosList[1];
                    player_Nav.enabled = true;
                    targetPosList.RemoveAt(0);
                }
            }
            else
            {
                // 当自动寻路时，实时判断离目标点距离，当小于min_Distance，切换到dotween动画。
                if (dis <= min_Distance)
                {
                    isXunLuIng = false;
                    player_Nav.enabled = false;
                    FirstViewControl.instance.SetPlayerState(PlayerControlState.anim);
                    moveToAction();
                    targetPosList.RemoveAt(0);
                }
            }
        }
    }

    /// <summary>
    /// 外调方法
    /// </summary>
    /// <param name="room">房间编号</param>
    /// <param name="_targetPos">自动寻路目标点</param>
    /// <param name="playerPos">目标点Player位置</param>
    /// <param name="playerRot">目标点player角度</param>
    /// <param name="camPos">目标点Camera位置</param>
    /// <param name="camRot">目标点Camera角度</param>
    /// <param name="time">运动时间</param>
    /// <param name="OnComplete">回调方法</param>
    public void MoveToDestination(Room room, Vector3 _targetPos, Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 camRot, float time, float fieldofView, Action OnComplete = null)
    {
        targetPosList.Add(_targetPos);

        if (room == curRoom)
        {
            //不需要应用寻路系统
            FirstViewControl.instance.SetPlayerState(PlayerControlState.anim);
            FirstViewControl.instance.MoveToDestination(playerPos, playerRot, camPos, camRot, 0, time, 60);
            StartCoroutine(Delay(time, OnComplete));
        }
        else
        {
            //需要应用寻路系统
            FirstViewControl.instance.SetPlayerState(PlayerControlState.aI);
            isXunLuIng = true;
            mainCamTrans.DOMoveY(1.6f, 0.7f);
            mainCamTrans.DOLocalRotate(Vector3.zero, 0.7f);
            DOTween.To(() => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView, x => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView = x, 60, 0.7f).OnComplete(() =>
            {
                player_Nav.enabled = true;
                player_Nav.destination = targetPosList[0];

                moveToAction = () =>
                {
                    FirstViewControl.instance.MoveToDestination(playerPos, playerRot, camPos, camRot, 0, 1f, fieldofView).OnComplete(() =>
                    {
                        if (OnComplete != null)
                            OnComplete.Invoke();
                    });
                };
              
            });
        }
    }

    /// <summary>    /// 单点自动寻路    /// </summary>
    /// <param name="_targetPos"></param>
    /// <param name="playerPos">目标点Player位置</param>
    /// <param name="playerRot">目标点player角度</param>
    /// <param name="camPos">目标点Camera位置</param>
    /// <param name="camRot">目标点Camera角度</param>
    /// <param name="time">运动时间</param>
    /// <param name="OnComplete">回调方法</param>
    public void MoveToDestination(Vector3 _targetPos, Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 camRot, float time, float fieldofView, Action OnComplete = null)
    {
        targetPosList.Add(_targetPos);

        //需要应用寻路系统
        FirstViewControl.instance.SetPlayerState(PlayerControlState.aI);
        isXunLuIng = true;
        mainCamTrans.DOMoveY(1.6f, 0.7f);
        mainCamTrans.DOLocalRotate(Vector3.zero, 0.7f);
        DOTween.To(() => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView, x => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView = x, 60, 0.7f).OnComplete(() =>
        {
            player_Nav.enabled = true;
            player_Nav.destination = targetPosList[0];

            moveToAction = () =>
            {
                FirstViewControl.instance.MoveToDestination(playerPos, playerRot, camPos, camRot, 0, 1f, fieldofView).OnComplete(() =>
                {
                    if (OnComplete != null)
                        OnComplete.Invoke();
                });
            };
           
        });
    }

    /// <summary>
    /// 多点自动寻路
    /// </summary>
    /// <param name="_targetPosList"></param>
    /// <param name="playerPos">目标点Player位置</param>
    /// <param name="playerRot">目标点player角度</param>
    /// <param name="camPos">目标点Camera位置</param>
    /// <param name="camRot">目标点Camera角度</param>
    /// <param name="time">运动时间</param>
    /// <param name="OnComplete">回调方法</param>
    public void MoveToDestination(List<Vector3> _targetPosList, Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 camRot, float time, float fieldofView, Action OnComplete = null)
    {
        
        foreach (var pos in _targetPosList)
        {
            targetPosList.Add(pos);
        }

        //需要应用寻路系统
        FirstViewControl.instance.SetPlayerState( PlayerControlState.aI);
        isXunLuIng = true;
        mainCamTrans.DOMoveY(1.6f,0.7f);
        mainCamTrans.DOLocalRotate(Vector3.zero,0.7f);
        DOTween.To(() => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView, x => mainCamTrans.gameObject.GetComponent<Camera>().fieldOfView = x, 60, 0.7f).OnComplete(() =>
        {
            player_Nav.enabled = true;
            player_Nav.destination = targetPosList[0];

            moveToAction = () =>
            {
                FirstViewControl.instance.MoveToDestination(playerPos, playerRot, camPos, camRot, 0, 1f, fieldofView).OnComplete(() =>
                {
                    if (OnComplete != null)
                        OnComplete.Invoke();
                });
            };
           
        });
    }


    /// <summary>    /// 正常位移 非自动寻路    /// </summary>
    /// <param name="playerPos">目标点Player位置</param>
    /// <param name="playerRot">目标点player角度</param>
    /// <param name="camPos">目标点Camera位置</param>
    /// <param name="camRot">目标点Camera角度</param>
    /// <param name="time">运动时间</param>
    /// <param name="OnComplete">回调方法</param>
    public void MoveToDestination(Vector3 playerPos, Vector3 playerRot, Vector3 camPos, Vector3 camRot, float time, Action OnComplete = null)
    {
        //不需要应用寻路系统
        FirstViewControl.instance.SetPlayerState(PlayerControlState.anim);
        FirstViewControl.instance.MoveToDestination(playerPos, playerRot, camPos, camRot, 0, time, 60);
        StartCoroutine(Delay(time, OnComplete));
    }

    IEnumerator Delay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    /// <summary>
    /// 实时确定当前Player在哪个房间
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerStay(Collider coll)
    {
        if (coll.name == "aRoom")
        {
            curRoom = Room.aRoom;
        }
        else if (coll.name == "bRoom")
        {
            curRoom = Room.bRoom;
        }
    }


}
