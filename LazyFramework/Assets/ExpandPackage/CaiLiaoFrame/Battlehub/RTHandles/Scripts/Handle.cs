using Battlehub.RTHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Handle : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public PositionHandle posHandle;
    [HideInInspector]
    public RotationHandle rotHandle;

    public bool isCollisionDrop;
    public string[] ignoreCollisionDropName;
    public string destiantionPosTriggerName;

    //[HideInInspector]
    public Axis mAxis = Axis.xyz;
    public float minAngle = Mathf.NegativeInfinity, maxAngle = Mathf.Infinity;

    void Start()
    {
        if (this.GetComponent<PositionHandle>() == null)
            gameObject.AddComponent<PositionHandle>();
        if (this.GetComponent<RotationHandle>() == null)
            gameObject.AddComponent<RotationHandle>();

        posHandle = GetComponent<PositionHandle>();
        rotHandle = GetComponent<RotationHandle>();
        posHandle.enabled = false;
        rotHandle.enabled = false;
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (posHandle.enabled || rotHandle.enabled)
                HandleController.instance.MouseRightClicked();
        }
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        if (HandleController.instance.menuUI.activeInHierarchy)
            return;
        if (GetComponent<Rigidbody>())
        {
            Destroy(this.GetComponent<Rigidbody>());
        }
        if (gameObject.GetComponent<CharacterController>() == null)
        {
            gameObject.AddComponent<CharacterController>();
            gameObject.GetComponent<CharacterController>().skinWidth = 0.001f;
        }
    }

    /// <summary>
    /// 移动时，碰撞检测
    /// </summary>
    /// <param name="hit"></param>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //当物体设置为碰撞后下落
        if (isCollisionDrop )
        {
            //碰到地面和桌子时，不自由下落
            for (int i = 0; i < ignoreCollisionDropName.Length; i++)
            {
                if (hit.collider.name == ignoreCollisionDropName[i])
                    return;
            }

            posHandle.enabled = false;
            posHandle.Delete();
            HandleController.instance.selectID = -1;
            if (gameObject.GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
            if (gameObject.GetComponent<CharacterController>() != null)
            {
                Destroy(GetComponent<CharacterController>());
            }
        }
    }



    /// <summary>
    /// 当碰到某个触发器，自动吸附
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name == destiantionPosTriggerName)
        {
            Debug.Log(other.gameObject.name + "触发器");
            HandleController.instance.Trigger(other.gameObject.name);
        }
    }


    /// <summary>
    /// 设置物体是否可以控制移动,旋转
    /// </summary>
    /// <param name="enable"></param>
    public void SetHandle(bool enable)
    {
        if (HandleController.instance.curHandleSelect == HandleController.HandleSelect.move)
        {
            posHandle.enabled = enable;
        }
        else
        {
            rotHandle.enabled = enable;
        }
    }


    public void Move()
    {
        None();
        posHandle.enabled = true;
    }
    public void Rotate()
    {
        
        None();
        rotHandle.enabled = true;
    }
    private void None()
    {
        posHandle.enabled = false;
        rotHandle.enabled = false;
    }

}
