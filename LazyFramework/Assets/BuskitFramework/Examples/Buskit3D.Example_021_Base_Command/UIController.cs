/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CubeDataModelEntity
* 创建日期：2019-01-09 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：命令系统（UI、消息触发控制）
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.BusKit.Unity.Modules.Command;

namespace Com.Rainier.Buskit3D.Example_021
{
    /// <summary>
    /// UI控制
    /// </summary>
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        //Undo按钮对象
        private GameObject goUndo;
        //Redo按钮对象
        private GameObject goRedo;
        //模型对象
        private CubeDataModel cubeDataModdel;
        //Cube数据实体对象
        public CubeDataModelEntity cubeDataEntity;
        //命令数据实体对象
        private CommandDataEntity CmdEntity;
        //判断是否点击在物体上
        private bool IsClickedPart = false;

        /// <summary>
        /// 命令系统服务
        /// </summary>
        [Inject]
        IServiceCommand _commandService;


        private void Awake()
        {
            instance = this;
        }


        /// <summary>
        /// 注入命令系统
        /// </summary>
        private void Start()
        {
            goUndo = GameObject.Find("/Canvas/BtnUndo");
            goRedo = GameObject.Find("/Canvas/BtnRedo");
            CmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
            InjectService.InjectInto(this);
            StartCoroutine(UpdateCommandUIState());
        }

        /// <summary>
        /// undo按钮处理函数
        /// </summary>
        public void OnUndo()
        {
            CmdEntity.UndoMessage = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Redo按钮处理函数
        /// </summary>
        public void OnRedo()
        {
            CmdEntity.RedoMessage = System.Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 更新命令UI的状态
        /// </summary>
        IEnumerator UpdateCommandUIState()
        {
            //获取命令系统堆栈
            ICommandStack stack = _commandService.GetCommandStack();
            while (true)
            {
                if (stack.CanUndo())
                {
                    goUndo.GetComponent<Button>().interactable = true;
                }
                else
                {
                    goUndo.GetComponent<Button>().interactable = false;
                }

                if (stack.CanRedo())
                {
                    goRedo.GetComponent<Button>().interactable = true;
                }
                else
                {
                    goRedo.GetComponent<Button>().interactable = false;

                }
                yield return new WaitForSeconds(0.3f);
            }
        }

        /// <summary>
        /// 更新Undo和Redo按钮状态
        /// </summary>
        private void Update()
        {

            Debug.Log(cubeDataEntity);

            //点击需要删除的物体
            if (Input.GetMouseButton(0))
            {
                //射线检测，点击UI和空白处无反应
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject() && hit.collider.name == "Plane")
                    {
                        return;
                    }
                    //碰撞到物体上，返回物体信息
                    else
                    {
                        Debug.Log("砰砰砰");
                        cubeDataModdel = hit.collider.gameObject.GetComponent<CubeDataModel>();
                        cubeDataEntity = (CubeDataModelEntity)cubeDataModdel.DataEntity;

                        Debug.Log(cubeDataEntity);

                        if (cubeDataModdel.gameObject != null)
                            IsClickedPart = true;
                    }
                }
            }
        }

        /// <summary>
        /// 创建Cube操作
        /// </summary>
        public void OnCreate()
        {
            CommandDataEntity cmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
            CreateCommandStr str = new CreateCommandStr();
            //随机坐标
            str.Position = new Vector3(Random.Range(-7, 11), Random.Range(-4, 6), Random.Range(0, 50));
            cmdEntity.CreatePartMessage = str;
        }

        /// <summary>
        /// 删除Cube操作
        /// </summary>
        public void OnDelete()
        {
            if (IsClickedPart)
            {
                CommandDataEntity cmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
                DeleteCommandStr str = new DeleteCommandStr();
                str.ObjectID = cubeDataEntity.objectID;
                cmdEntity.DeletePartMessage = str;
                IsClickedPart = false;
            }
        }

        /// <summary>
        ///  改变Cube颜色
        /// </summary>
        public void OnChangeColor()
        {
            if (IsClickedPart)
            {
                CommandDataEntity cmdEntity = (CommandDataEntity)FindObjectOfType<CommandDataModel>().DataEntity;
                ColorCommandStr str = new ColorCommandStr();
                str.ObjectID = cubeDataEntity.objectID;
                //记录开始的颜色
                str.OldColor = cubeDataModdel.gameObject.GetComponent<MeshRenderer>().material.color;
                //随机新的颜色
                str.NewColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                cmdEntity.ColorParMessage = str;
                IsClickedPart = false;
            }
        }

        public void OnChangValue(float value)
        {
            Debug.Log(this.cubeDataEntity);
            if (cubeDataEntity != null) {
                cubeDataEntity.RotateSpeed = value;
                Debug.Log(value);
            }
        }        
    }
}

