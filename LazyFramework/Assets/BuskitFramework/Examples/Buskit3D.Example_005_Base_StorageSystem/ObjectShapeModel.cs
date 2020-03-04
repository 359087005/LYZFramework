/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D_Example_005_StorageSystem
* 类 名 称：ObjectShapeModel
* 创建日期：2018-04-07 10:58:17
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 功能描述：数据载体
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;

namespace Buskit3D_Example_005_StorageSystem
{
    /// <summary>
    /// 实例化物体的数据载体
    /// </summary>
    public class ObjectShapeModel : DataModelBehaviour
    {
        //自身颜色
        private Color32 selfColor;
       
        /// <summary>
        /// Unity Mothod
        /// </summary>
        private void Awake()
        {
            ObjectShapeEntity data = new ObjectShapeEntity();
            this.DataEntity = data;
            Watch(this);
        }

        /// <summary>
        /// Unity Mothod
        /// </summary>
        protected override void Start()
        {           
            base.Start();
            selfColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
            //开始拖拽
            gameObject.SetBeginDrag((p) =>
            {
                ObjectShapeEntity data = (ObjectShapeEntity)DataEntity;
                data.normalColor = Color.red;
            });
            //结束拖拽
            gameObject.SetEndDrag((p) =>
            {
                ObjectShapeEntity data = (ObjectShapeEntity)DataEntity;
                data.normalColor = selfColor;
            });
        }

        /// <summary>
        /// 鼠标拖拽
        /// </summary>
        private void OnMouseDrag()
        {
            Vector3 screenZ = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenZ.z));

            ObjectShapeEntity data = (ObjectShapeEntity)DataEntity;
            data.position = world;
        }

    }
}
