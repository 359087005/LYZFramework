/************************************************************
  Copyright (C), 2007-2017,BJ Rainier Tech. Co., Ltd.
  FileName: FirstViewControl.cs
  Author:�����       Version :1.0          Date: 
  Description: �����˳ƿ���ʱ����������ӽǵĿ���
************************************************************/

using System;
using UnityEngine;

    public class FreeLookCam : MonoBehaviour
    {
        private static FreeLookCam _instance;
        public static FreeLookCam instance { set { } get { return _instance; } }

        [HideInInspector]
        public Transform m_Target;//Player
        [HideInInspector]
        public Transform m_CamLocation; // the transform of the camera
        [HideInInspector]
        public Transform m_Pivot; // the point at which the camera pivots around

      //  [SerializeField]
        private float m_MoveSpeed = 10f;                      // How fast the rig will move to keep up with the target's position.
       // [Range(0f, 10f)]
       // [SerializeField]
        private float m_TurnSpeed = 2.75f;   // How fast the rig will rotate from user input.
     //   [SerializeField]
        private float m_TurnSmoothing = 5f;                // How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
        [SerializeField]
        private float m_TiltMax = 75f;                       // The maximum value of the x axis rotation of the pivot.
        [SerializeField]
        private float m_TiltMin = 45f;                       // The minimum value of the x axis rotation of the pivot.
     //   [SerializeField]
        private bool m_LockCursor = false;                   // Whether the cursor should be hidden and locked.
    //    [SerializeField]
        private bool m_VerticalAutoReturn = false;           // set wether or not the vertical axis should auto return

        private float m_LookAngle;                    // The rig's y axis rotation.
        private float m_TiltAngle;                    // The pivot's x axis rotation.
        private const float k_LookDistance = 100f;    // How far in front of the pivot the character's look target is.
        private Vector3 m_PivotEulers;
        private Quaternion m_PivotTargetRot;
        private Quaternion m_TransformTargetRot;

        private float m_CamDis;
        private float zoomSpeed = 10;
        private ProtectCameraFromWallClip pCFWC;
        
    [SerializeField, TooltipAttribute("�������С����")]
        public float minDis = 0.3f;
    [SerializeField, TooltipAttribute("�����������")]
        public float maxDis = 3f;

         void Awake()
        {
            _instance = this;
            pCFWC = transform.GetComponent<ProtectCameraFromWallClip>();

        }
        void Start()
         {
             m_Target = FirstViewControl.instance.gameObject.transform;
             m_Pivot = transform.Find("Pivot");
             if (m_Pivot == null)
                 Debug.LogError("FreeLookCam�ű�������������Pivot");
             if (m_Pivot != null)
                 m_CamLocation = m_Pivot.Find("TvcCamLocation");
             if (m_CamLocation == null)
                 Debug.LogError("Pivot��������TvcCamLocation");
             // Lock or unlock the cursor.
             Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
             Cursor.visible = !m_LockCursor;
             //m_PivotEulers = m_Pivot.rotation.eulerAngles;
             m_PivotEulers = m_Pivot.localEulerAngles;

             m_PivotTargetRot = m_Pivot.transform.localRotation;
             m_TransformTargetRot = transform.localRotation;

             m_CamDis = m_CamLocation.localPosition.z;

             InitialCamParameter();

         }
        /// <summary>
        /// ��ʼ��������Ĳ��������������Ǻ;���
        /// </summary>
        void InitialCamParameter()
         {
             if (m_Target == null)
                 m_Target = FirstViewControl.instance.gameObject.transform;
             transform.eulerAngles = m_Target.eulerAngles;

             m_LookAngle = m_Target.eulerAngles.y;
             m_TiltAngle = 20;//������Ϊ20��
             m_Pivot.localEulerAngles = new Vector3(m_TiltAngle, 0, 0);
             pCFWC.m_OriginalDist = 1.5f;//����Ϊ2��
         }

        void Update()
        {
           // 
        }

        protected void LateUpdate()
        {
            FollowTarget(Time.deltaTime);
            if (m_Target.GetComponent<ThirdViewControl>().playerState != PlayerControlState.noUse)
            { 
                HandleRotationMovement(); 
            } 
            else
            if (m_LockCursor && Input.GetMouseButtonUp(0))
            {
                Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !m_LockCursor;
            }
        }


        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


         void FollowTarget(float deltaTime)
        {
                if (m_Target == null) return;
                // Move the rig towards target position.
                transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime * m_MoveSpeed);
           
            
        }


        private void HandleRotationMovement()
        {
            if (Time.timeScale < float.Epsilon)
                return;

            // Read the user input
           
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                if (pCFWC && pCFWC.enabled)
                {
                    pCFWC.m_OriginalDist += zoomSpeed * Time.deltaTime;
                    pCFWC.m_OriginalDist = Mathf.Clamp(pCFWC.m_OriginalDist,minDis, maxDis);
                }
                else
                {
                    m_CamDis -= zoomSpeed * Time.deltaTime;
                    m_CamDis = Mathf.Clamp(m_CamDis, (-1) * maxDis, (-1) * minDis);
                    m_CamLocation.transform.localPosition = new Vector3(0f, 0f, m_CamDis);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                if (pCFWC && pCFWC.enabled)
                {
                    pCFWC.m_OriginalDist -= zoomSpeed * Time.deltaTime;
                    pCFWC.m_OriginalDist = Mathf.Clamp(pCFWC.m_OriginalDist, minDis, maxDis);
                }
                else
                {
                    m_CamDis += zoomSpeed * Time.deltaTime;
                    m_CamDis = Mathf.Clamp(m_CamDis, (-1) * maxDis, (-1) * minDis);
                    m_CamLocation.transform.localPosition = new Vector3(0f, 0f, m_CamDis);
                }
            }

           
            float x = 0;
            float y = 0;
            if (Input.GetMouseButton(0))
            {
                x = Input.GetAxis("Mouse X");
                y = Input.GetAxis("Mouse Y");
            }
            

            // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
            m_LookAngle += x*m_TurnSpeed;

            // Rotate the rig (the root object) around Y axis only:
            m_TransformTargetRot = Quaternion.Euler(0f, m_LookAngle, 0f);

            if (m_VerticalAutoReturn)
            {
                // For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
                // on mobile, vertical input is directly mapped to tilt value, so it springs back automatically when the look input is released
                // we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
                m_TiltAngle = y > 0 ? Mathf.Lerp(0, -m_TiltMin, y) : Mathf.Lerp(0, m_TiltMax, -y);
            }
            else
            {
                // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
                m_TiltAngle -= y*m_TurnSpeed;
                // and make sure the new value is within the tilt range
                m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);
            }

            // Tilt input around X is applied to the pivot (the child of this object)
			m_PivotTargetRot = Quaternion.Euler(m_TiltAngle, m_PivotEulers.y , m_PivotEulers.z);

			if (m_TurnSmoothing > 0)
			{
				m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_TurnSmoothing * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_TurnSmoothing * Time.deltaTime);
                
			}
			else
			{
				m_Pivot.localRotation = m_PivotTargetRot;
				transform.localRotation = m_TransformTargetRot;
			}
            

        }
       
        /// <summary>
        /// ���ýǶ�
        /// </summary>
        public void ResetRot()
        {
            InitialCamParameter();
            
        }
       
        public void SetParent(bool isParent)
        {
            if (isParent)
                transform.parent.parent = GameObject.Find("Player").transform;
            else
            {
                transform.parent.parent = GameObject.Find("GameMain").transform;
                transform.parent.eulerAngles = Vector3.zero;
                //transform.parent.position = Vector3.zero;
            }
        }
    }

