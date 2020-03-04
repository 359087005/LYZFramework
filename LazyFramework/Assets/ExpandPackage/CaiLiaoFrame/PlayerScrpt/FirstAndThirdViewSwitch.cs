/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 第一人称和第三人称控制器切换
************************************************************/

using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
    FirstView,
    ThirdView,
}
public class FirstAndThirdViewSwitch : MonoBehaviour
{

    private static FirstAndThirdViewSwitch _instance;
    public static FirstAndThirdViewSwitch instance { set { } get { return _instance; } }

    //[HideInInspector]
    public PlayerState curState = PlayerState.ThirdView;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Transform mainCamTrans;
    //第一人称切换到第三人称时，摄像机需要先移动到一个起始高度
    [HideInInspector]
    public float fvcHeight = 1.6f;
    [SerializeField, TooltipAttribute("第三人称摄像机位置")]
    public Transform tvcCamLocation;
    [HideInInspector]
    public List <GameObject> playerRenderList;

    private UnityEngine.AI.NavMeshAgent agent;
    private bool switchIng;
    [HideInInspector]
    public bool canSwitch = true;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

        if (player == null)
        {
            player = gameObject.transform;
            if (player.tag != "Player")
                Debug.LogError("请设置第一人称Tag为Player");
        }

        if (mainCamTrans == null)
        {
            mainCamTrans = Camera.main.transform;
            if (mainCamTrans == null)
                Debug.LogError("请设置主摄像机Tag为MainCamera");
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.name == "YunDongYuan")
                playerRenderList.Add(gameObject.transform.GetChild(i).gameObject);
        }

        FreeLookCam.instance.SetParent(false);
        if(curState == PlayerState.FirstView)
        {
            IniFVCView(curState);
        }
        else
        {
            InstantSwitch(curState);
        }
        
    }

    void IniFVCView(PlayerState state)
    {
        curState = state;
        FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
        ThirdViewControl.instance.playerState = PlayerControlState.noUse;

        tvcCamLocation.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = true;

        FreeLookCam.instance.m_Pivot.localEulerAngles = new Vector3(20, 0, 0);
        FreeLookCam.instance.transform.localEulerAngles = player.eulerAngles;
        
        FirstViewControl.instance.IniQuaternion();

        foreach (var obj in playerRenderList)
        {
            obj.SetActive(false);
        }
    }
    void Update()
    {
        if (canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (curState == PlayerState.FirstView)
                {
                    Switch(PlayerState.ThirdView);
                }
                else
                {
                    Switch(PlayerState.FirstView);
                }
            }
        }

        if (agent != null)
            ZiDongXunLu();

    }

    /// <summary>
    /// 人称切换
    /// </summary>
    /// <param name="curState"></param>
    public void Switch(PlayerState state)
    {
        Debug.Log("Switch");
        if (curState == state || switchIng)
            return;
        switchIng = true;
        curState = state;
        //第三人称转向第一人称
        if (state == PlayerState.FirstView)
        {
            FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
            ThirdViewControl.instance.playerState = PlayerControlState.noUse;
            ThirdViewControl.instance.StopAnimator();

            tvcCamLocation.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = false;

            FreeLookCam.instance.m_Pivot.DOLocalRotate(new Vector3(20, 0, 0), 1);
            FreeLookCam.instance.transform.DORotate(player.eulerAngles, 1);
            FreeLookCam.instance.m_CamLocation.DOLocalMoveZ(0, 1f).OnComplete(() =>
            {
                tvcCamLocation.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = true;
                mainCamTrans.parent = player;
                switchIng = false;
                FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
                FirstViewControl.instance.IniQuaternion();
            });

        }
        //第一人称转向第三人称
        else
        {
            FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
            ThirdViewControl.instance.playerState = PlayerControlState.noUse;
            FreeLookCam.instance.ResetRot();
            //延迟一点，是为了确定第三人称摄像机最终位置
            StartCoroutine(SetDelay(0.1f, () =>
            {
                mainCamTrans.transform.localPosition = new Vector3(0, fvcHeight,0.3f);
                mainCamTrans.parent = tvcCamLocation;

                mainCamTrans.transform.localEulerAngles = Vector3.zero;
                DOTween.To(() => mainCamTrans.GetComponent<Camera>().fieldOfView, x => mainCamTrans.GetComponent<Camera>().fieldOfView = x, 60, 1f);
                mainCamTrans.transform.DOLocalMove(new Vector3(0,0,0.3f), 1).OnComplete(() =>
                {
                    switchIng = false;
                    ThirdViewControl.instance.playerState = PlayerControlState.playerControl;
                    ThirdViewControl.instance.StartAnimator();
                });
            }));
        }
    }

    /// <summary>
    /// 直接切换，没有切换动画
    /// </summary>
    public void InstantSwitch(PlayerState state)
    {
        Debug.Log("InstantSwitch");
        curState = state;
        //第三人称转向第一人称
        if (state == PlayerState.FirstView)
        {
            FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
            ThirdViewControl.instance.playerState = PlayerControlState.noUse;
            ThirdViewControl.instance.StopAnimator();

            tvcCamLocation.parent.parent.GetComponent<ProtectCameraFromWallClip>().enabled = true;

            FreeLookCam.instance.m_Pivot.localEulerAngles = new Vector3(20, 0, 0);
            FreeLookCam.instance.transform.localEulerAngles = player.eulerAngles;
            mainCamTrans.parent = player;
            mainCamTrans.transform.localPosition = new Vector3(0, fvcHeight,0.3f); 
            FirstViewControl.instance.IniQuaternion();

            foreach (var obj in playerRenderList)
            {
                obj.SetActive(false);
            }
            
        }
        //第一人称转向第三人称
        else if (state == PlayerState.ThirdView)
        {
            FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
            ThirdViewControl.instance.playerState = PlayerControlState.playerControl;
            FreeLookCam.instance.ResetRot();

            mainCamTrans.parent = tvcCamLocation;
            mainCamTrans.transform.localEulerAngles = Vector3.zero;
            mainCamTrans.transform.localPosition = Vector3.zero;
            mainCamTrans.GetComponent<Camera>().fieldOfView = 60;
            ThirdViewControl.instance.StartAnimator();

            foreach (var obj in playerRenderList)
            {
                obj.SetActive(true);
            }
            
        }
    }


    [ContextMenu("FirstView")]
    public void FirstView()
    {
        curState = PlayerState.FirstView;

        mainCamTrans.parent = transform;
        mainCamTrans.transform.localPosition = new Vector3(0, fvcHeight, 0.3f); 

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.name == "YunDongYuan" )
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
        GetComponent<ThirdViewControl>().playerState = PlayerControlState.noUse;
    }
    [ContextMenu("ThirdView")]
    public void ThirdView()
    {
        curState = PlayerState.ThirdView;

        mainCamTrans.parent = tvcCamLocation;
        mainCamTrans.transform.localEulerAngles = Vector3.zero;
        mainCamTrans.transform.localPosition = Vector3.zero;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.name == "YunDongYuan")
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }

        FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
        GetComponent<ThirdViewControl>().playerState = PlayerControlState.playerControl;
    }


    /// <summary>
    /// 主摄像机进入Player
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Main Camera")
        {
            foreach (var obj in playerRenderList)
            {
                obj.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 主摄像机离开Player
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerExit(Collider coll)
    {
        if (coll.name == "Main Camera" && curState != PlayerState.FirstView)
        {
            foreach (var obj in playerRenderList)
            {
                obj.SetActive(true);
            }
        }
    }

    IEnumerator SetDelay(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action();
    }


    #region  自动寻路,第一人称和第三人称可随时切换的自动寻路
    /// <summary>
    /// 自动寻路触发事件
    /// </summary>
    /// <param name="go"></param>
    public void BtnClicked(Transform target)
    {
        if (agent == null)
            return;
        agent.enabled = true;
        agent.SetDestination(target.position);
        preventOnce = true;
    }

    /// <summary>
    /// 当刚开始寻路时，agent.remainingDistance为0，为避免自动判断结束寻路，需要跳过一帧。
    /// </summary>
    bool preventOnce;

    void ZiDongXunLu()
    {
        if (agent.enabled)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {

                if (curState == PlayerState.ThirdView)
                {
                    ThirdViewControl.instance.Move(agent.desiredVelocity);
                }
            }
            else
            {
                if (preventOnce)
                {
                    preventOnce = false;
                    return;
                }

                agent.enabled = false;

                if (curState == PlayerState.FirstView)
                {
                    FirstViewControl.instance.IniQuaternion();
                    FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
                }
                else if (curState == PlayerState.ThirdView)
                {
                    ThirdViewControl.instance.playerState = PlayerControlState.playerControl;
                }

                //Debug.Log("寻路结束");
            }
        }
    }

    #endregion
}
