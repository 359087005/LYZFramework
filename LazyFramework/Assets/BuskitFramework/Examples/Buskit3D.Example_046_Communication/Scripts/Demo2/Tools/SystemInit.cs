
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SceneInit
* 创建日期：2019-04-09 17:58:28
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.BusKit.Unity.Modules.Command;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Com.Rainier.Buskit3D;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
	public class SystemInit : MonoBehaviour
    {
        /// <summary>
        /// 长
        /// </summary>
        public int chang;
        /// <summary>
        /// 宽
        /// </summary>
        public int kuan;

        /// <summary>
        /// Undo按钮对象
        /// </summary>
        public GameObject goUndo;
        /// <summary>
        /// Redo按钮对象
        /// </summary>
        public GameObject goRedo;

        /// <summary>
        /// 命令数据实体对象
        /// </summary>        
        private CommandDataEntity CmdEntity;

        /// <summary>
        /// 命令系统服务
        /// </summary>
        [Inject]
        IServiceCommand _commandService;

        /// <summary>
        /// Entity实体工具
        /// </summary>
        [Inject]
        EntityUtils entityUtils;

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            InjectService.InjectInto(this);
            InitTerrain();

            CmdEntity = entityUtils.GetEntity<CommandDataEntity>(gameObject);

            StartCoroutine(UpdateCommandUIState());
        }

        /// <summary>
        /// 创建一个地形
        /// </summary>
        void InitTerrain()
        {
            int index = 0;
            Transform parent = GameObject.Find("Plane").transform;
            for (int i = 0; i < chang; i++)
            {
                for (int j = 0; j < kuan; j++)
                {
                    Object prefab = Resources.Load("TerrainCollier", typeof(GameObject));
                    GameObject terrain = Instantiate(prefab) as GameObject;
                    terrain.transform.position = new Vector3(-4.5f + 1.5f * j, 0, -4.55f + 0.866f * i);
                    terrain.transform.SetParent(parent);
                    terrain.name = (index++).ToString();

                    if (j < kuan - 1)
                    {
                        prefab = Resources.Load("TerrainCollier", typeof(GameObject));
                        terrain = Instantiate(prefab) as GameObject;
                        terrain.transform.position = new Vector3(-4.5f + 1.5f * j + 0.75f, 0, -4.55f + 0.866f * i + 0.433f);
                        terrain.transform.SetParent(parent);
                        terrain.name = (kuan * i + j * 2 + 2).ToString();
                        terrain.name = (index++).ToString();
                    }
                }
            }
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



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) {
                ObjectPool<PhoneModel> pool = InjectService.Get<ObjectPool<PhoneModel>>();
                Debug.Log(pool.Count());
                pool.Foreach(Foreach);
            }            
        }

        private void Foreach(int index, PhoneModel phoneModel) {
            Debug.Log(index+"__"+ phoneModel.gameObject.GetComponent<UniqueID>().UniqueId);
        }


    }
}

