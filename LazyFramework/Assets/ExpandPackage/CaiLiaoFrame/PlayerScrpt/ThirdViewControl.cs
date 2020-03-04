/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:汪海波       Version :1.0          Date: 
  Description: 第三人称控制器
************************************************************/
using UnityEngine;
using System.Collections;

public class ThirdViewControl : MonoBehaviour
{
    private static ThirdViewControl _instance;
    public static ThirdViewControl instance { set { } get { return _instance; } }

    public PlayerControlState playerState = PlayerControlState.playerControl;

    private Animator m_Animator;
    [HideInInspector]
    public Transform m_CamLocation;
    Vector3 m_GroundNormal;

    float m_MovingTurnSpeed = 360;
    float m_StationaryTurnSpeed = 180;

   private bool stopAnim;
    void Awake()
    {
         _instance = this;
    }
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        if(m_Animator==null)
        {
            Debug.LogError("请给控制器添加动画组件");
        }

        m_CamLocation = FirstAndThirdViewSwitch.instance.tvcCamLocation;
    }
    void FixedUpdate()
    {
        if (!GetComponent<UnityEngine.AI.NavMeshAgent>() || (GetComponent<UnityEngine.AI.NavMeshAgent>() && !GetComponent<UnityEngine.AI.NavMeshAgent>().enabled))
        if (playerState == PlayerControlState.playerControl)
        {
           //第三人称控制且非自动寻路状态下
            float h;
            float v;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector3 m_CamForward = Vector3.Scale(m_CamLocation.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * m_CamLocation.right;

            Move(m_Move);
        }

        if (stopAnim)
        {

            m_Animator.SetFloat("Forward", 0, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", 0, 0.1f, Time.deltaTime);
        }

    }
   

    private float m_TurnAmount, m_ForwardAmount;
    public void Move(Vector3 m_Move)
    {
        if (m_Move.magnitude > 1f) m_Move.Normalize();
        m_Move = transform.InverseTransformDirection(m_Move);
       
        m_Move = Vector3.ProjectOnPlane(m_Move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(m_Move.x, m_Move.z);
        m_ForwardAmount = m_Move.z;

        if (m_TurnAmount < 3.14f)
        {
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

    }
    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }
    void UpdateAnimator()
    {
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
    }

    public void StopAnimator()
    {

        stopAnim = true;
        playerState = PlayerControlState.noUse;
    }
    public void StartAnimator()
    {
        stopAnim = false;
        playerState = PlayerControlState.playerControl;
    }
   
}
