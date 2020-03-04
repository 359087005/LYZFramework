using UnityEngine;
using System.Collections;
using Battlehub.RTHandles;
using UnityEngine.EventSystems;
using DG.Tweening;


public class HandleController : MonoBehaviour
{
    private static HandleController _instance;
    public static HandleController instance { set { } get { return _instance; } }
    public enum HandleSelect
    {
        move,
        rotate,
    }
    //public PositionHandle[] positionHandleArr;
    //public RotationHandle[] rotationHandleArr;
    public Handle[] handleArr;
    public CameraRotate cameraRotate;
    public HandleSelect curHandleSelect = HandleSelect.move;
    public GameObject menuUI;
    public int selectID=-1;

    Ray ray;
    RaycastHit hit;
    Vector3 cameraPos, cameraRot;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        selectID = -1;
    }

    void LateUpdate()
    {
        
        //当弹出菜单后，不可再选择物体
        if (menuUI.activeInHierarchy)
            return;

        if (BaseHandle.m_draggingTool != null && FirstViewControl.instance.IsCanRotate())
        {
            FirstViewControl.instance.SetIsCanRotate(false,false);
        }
        else if (BaseHandle.m_draggingTool == null && !FirstViewControl.instance.IsCanRotate())
        {
            FirstViewControl.instance.SetIsCanRotate(true,false);
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            if (BaseHandle.m_draggingTool != null)
            {
                return;
            }

            if (curHandleSelect == HandleSelect.move)
            {
                MoveHandleClicked();
            }
            else
            {
                RotateHandleClicked();
            }
        }
    }
    /// <summary>
    /// 射线检测
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    bool RayCheck(out string name)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            name = hit.collider.name;
            return true;
        }
        name = "null";
        return false;
    }

    /// <summary>
    /// 移动选择
    /// </summary>
    void MoveHandleClicked()
    {
        string hitName;
        if (RayCheck(out hitName))
        {
            int id;
            if (Select(hit.collider.name, out id))
            {
                for (int i = 0; i < handleArr.Length; i++)
                {
                    if (id == i)
                        handleArr[i].posHandle.enabled = true;
                    else
                        handleArr[i].posHandle.enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < handleArr.Length; i++)
                {
                    handleArr[i].posHandle.enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < handleArr.Length; i++)
            {
                handleArr[i].posHandle.enabled = false;
            }
            selectID = -1;
        }
    }

    /// <summary>
    /// 旋转旋转
    /// </summary>
    void RotateHandleClicked()
    {
        string hitName;
        if (RayCheck(out hitName))
        {
            int id;
            if (Select(hit.collider.name, out id))
            {
                for (int i = 0; i < handleArr.Length; i++)
                {
                    if (id == i)
                    {
                        handleArr[i].rotHandle.enabled = true;
                    }
                    else
                    {
                        handleArr[i].rotHandle.enabled = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < handleArr.Length; i++)
                {
                    handleArr[i].rotHandle.enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < handleArr.Length; i++)
            {
                handleArr[i].rotHandle.enabled = false;
            }
            selectID = -1;
        }
    }

    /// <summary>
    /// 选择
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Select(string name, out int id)
    {
        for (int i = 0; i < handleArr.Length; i++)
        {
            if (name == handleArr[i].transform.name)
            {

                selectID = i;
                id = i;
                return true;
            }
        }
        id = -1;
        return false;
    }

    /// <summary>
    /// 右击
    /// </summary>
    public void MouseRightClicked()
    {
        menuUI.SetActive(!menuUI.activeInHierarchy);
    }

    /// <summary>
    /// 点击移动按钮
    /// </summary>
    public void SelectMove()
    {
        menuUI.SetActive(!menuUI.activeInHierarchy);
        if (curHandleSelect == HandleSelect.move)
            return;
        curHandleSelect = HandleSelect.move;
        handleArr[selectID].Move();
    }
    /// <summary>
    /// 点击旋转按钮
    /// </summary>
    public void SelectRotate()
    {
        menuUI.SetActive(!menuUI.activeInHierarchy);
        if (curHandleSelect == HandleSelect.rotate)
            return;
        curHandleSelect = HandleSelect.rotate;
        handleArr[selectID].Rotate();
    }
    /// <summary>
    /// 点击视角按钮
    /// </summary>
    public void SelectView()
    {
        menuUI.SetActive(!menuUI.activeInHierarchy);

        SwitchView(!cameraRotate.enabled);
    }

    /// <summary>
    /// 视角切换
    /// </summary>
    /// <param name="isToCamera"></param>
    public void SwitchView(bool isToCamera)
    {
        if (isToCamera)
        {
            cameraPos = Camera.main.transform.position;
            cameraRot = Camera.main.transform.eulerAngles;
            cameraRotate.enabled = true;
            cameraRotate.viewTrans = handleArr[selectID].transform;

            FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse);
            cameraRotate.LookAtMoveToDestination(handleArr[selectID].transform.position, transform.position, 3, 2);

        }
        else
        {
            cameraRotate.enabled = false;
            Camera.main.transform.DOMove(cameraPos, 2).SetDelay(0).SetEase(Ease.InOutQuad);
            Camera.main.transform.DORotate(cameraRot, 2).SetDelay(0).SetEase(Ease.InOutQuad);

            FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl);
            this.SetDelay(2,()=>FirstViewControl.instance.IniQuaternion());

        }
    }
    /// <summary>
    /// 移动到目标触发点
    /// </summary>
    /// <param name="name"></param>
    public void Trigger(string name)
    {

    }

}
