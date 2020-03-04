/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：RainierEventTriggerListener
* 创建日期：2018-12-28 10:58:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：基于UGUI的2D拖拽、右键菜单
* 修改记录：高大旺
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_010_UIDrag
{
    /// <summary>
    /// 拖拽逻辑处理类
    /// </summary>
    public class Drag2dLogic : LogicBehaviour,IPointerDownHandler
    {
        //当前位置
        private Vector3 _ThisPos;
        //当前物体的y坐标
        private float YSize;
        //当前物体的宽度
        private float XSize;
        //偏移量
        private Vector3 offsect;         
        //拖拽结构体实例
        private DragUIStr str;
        //实体
        private Drag2dDataEntity entity = null;
        //模型分发器数组
        private DataModelBehaviour[] array;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Start()
        {
            str = new DragUIStr();
            entity = (Drag2dDataEntity)GetComponent<Drag2dDataModel>().DataEntity;
            _ThisPos = this.transform.position;
            YSize = this.gameObject.transform.GetComponent<RectTransform>().rect.y;
            XSize = this.gameObject.transform.GetComponent<RectTransform>().rect.width;
            array = gameObject.GetComponentsInChildren<DataModelBehaviour>();
        }

        /// <summary>
        /// 处理业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //拖动消息
            if (evt.PropertyName.Equals("DragUIMessage"))
            {
                //如果在开始拖动
                if (((DragUIStr)evt.NewValue).isDragUI)
                {
                    DragUIStr str = (DragUIStr)evt.NewValue;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i].DataEntity.objectID == str.objectID)
                        {
                            array[i].transform.position = str.newPosition;
                        }
                    }
                }
            }
        }

        #region 委托注册的方法
        /// <summary>
        /// 开始UI拖拽 
        /// </summary>
        /// <param name="Obj"></param>
        public void OnMouseIsBegDrag(GameObject Obj)
        {
            offsect = Input.mousePosition - Obj.transform.parent.position;
            str.objectID = Obj.transform.parent.GetComponent<DataModelBehaviour>().DataEntity.objectID;
            str.oldPosition = Obj.transform.parent.position;
            str.objectID = Obj.transform.parent.GetComponent<DataModelBehaviour>().DataEntity.objectID;
            str.isDragUI = true;
        }

        /// <summary>
        /// 拖拽中
        /// </summary>
        /// <param name="Obj"></param>
        public void OnMouseIsDrag(GameObject Obj)
        {
            Obj.transform.parent.parent.SetAsLastSibling();
            Obj.transform.parent.position = Input.mousePosition - offsect;
            str.newPosition = Obj.transform.parent.position;
            str.objectID = Obj.transform.parent.GetComponent<DataModelBehaviour>().DataEntity.objectID;
            entity.DragUIMessage = str;
        }

        /// <summary>
        /// 结束拖拽
        /// </summary>
        /// <param name="Obj"></param>
        public void OnMouseIsEndDrag(GameObject Obj)
        {
            DragUIStr str = new DragUIStr();
            str.isDragUI = false;
        }
        #endregion

        #region 接口实现
        public void OnPointerDown(PointerEventData eventData)
        {
            //给拖拽相关的委托注册方法
            GameObject Obj = eventData.pointerCurrentRaycast.gameObject;
            if (!Obj.name.Equals("Title")) {
                return;
            }
            RainierEventTriggerListener listener = RainierEventTriggerListener.Get(Obj);
            listener.onBeginDragLeft += OnMouseIsBegDrag;
            listener.onDragLeft += OnMouseIsDrag;
            listener.onEndDragLeft += OnMouseIsEndDrag;
        }
        #endregion
    }
}

