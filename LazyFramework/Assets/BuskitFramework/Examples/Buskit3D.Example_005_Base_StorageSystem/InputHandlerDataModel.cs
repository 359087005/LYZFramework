/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： InputHandler
* 创建日期：2019-01-08 11:24:40
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：事件触发器
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D_Example_005_StorageSystem
{
    /// <summary>
    /// 输入触发
    /// </summary>
    public class InputHandlerDataModel : DataModelBehaviour 
	{
        //实例化的预制体列表
        private string[] names = new string[] { "Cube","Sphere","Capsule"};

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            InputHandlerEntity data = new InputHandlerEntity();
            this.DataEntity = data;
            Watch(this);
          
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        protected override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        public override  void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RightMouseEvent();
             
            }
            base.Update();
        }

        /// <summary>
        /// 屏幕点到世界坐标
        /// </summary>
        /// <returns></returns>
        private Vector3 GetWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }

        /// <summary>
        /// 鼠标右键点击事件
        /// </summary>
        /// <param name="go"></param>
        private void RightMouseEvent()
        {
            InputHandlerEntity data = (InputHandlerEntity)DataEntity;
            PrefabInfo info;
            info.nameOfPrefab = names[Random.Range(0, 3)];
            info.position = GetWorldPosition();
            data.prefabInfo = info;
            
        }

        /// <summary>
        /// 场景还原
        /// </summary>
        public override void LoadStorageData()
        {
            base.LoadStorageData();
        }
    }

    /// <summary>
    /// 要生成的预制体的数据结构
    /// </summary>
    public struct PrefabInfo
    {
        /// <summary>
        /// 要生成的预制体名字
        /// </summary>
        public string nameOfPrefab;

        /// <summary>
        /// 初始位置
        /// </summary>
        public Vector3 position;
    }

    /// <summary>
    /// 输入触发实体
    /// </summary>
    public class InputHandlerEntity : BaseDataModelEntity
    {
        /// <summary>
        /// 预制体信息
        /// </summary>
        public PrefabInfo prefabInfo;
    }
}

