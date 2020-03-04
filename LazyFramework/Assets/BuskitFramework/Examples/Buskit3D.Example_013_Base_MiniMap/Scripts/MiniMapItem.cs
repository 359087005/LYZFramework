/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MiniMapItem
* 创建日期：2018-12-19 09:25:22
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：小地图上显示的可交互的Item元素
******************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// Item元素
    /// </summary>
    public class MiniMapItem : MonoBehaviour 
	{
        //图标
        public Sprite graphic;

        //位置偏移
        public Vector3 offect;

        //UI实例
        public GameObject itemPrefab;

        //小地图的RectRoot
        public RectTransform mapUIRoot;

        //自身
        public Transform target;

        //颜色
        public Color graphColor=Color.white;

        //地图相机
        private UnityEngine.Camera miniCamera;

        //交互UI被点击事件
        public UnityEvent OnItemClick;

        //交互UI的rect
        private RectTransform iconItem;

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            if (mapUIRoot == null)
                mapUIRoot = GameObject.Find("MiniMapUIRoot").GetComponent<RectTransform>();
            if (miniCamera == null)
                miniCamera = GameObject.Find("MiniMapCamera").GetComponent<UnityEngine.Camera>();
            target = transform;
            InitItemUI();
        }

        /// <summary>
        /// 初始化小地图上的交互UI，并设置相关属性和事件
        /// </summary>
        public void InitItemUI()
        {
            GameObject item = Instantiate(itemPrefab, mapUIRoot.Find("MiniMapMask"));
            iconItem = item.GetComponent<RectTransform>();
            item.GetComponent<IconItem>().image = graphic;
            item.GetComponent<IconItem>().color = graphColor;
            item.SetClick(p => {
                OnItemClick.Invoke();
            }); 
        }

        /// <summary>
        /// 更新交互UI的实时位置
        /// </summary>
        public void UpdatePosition()
        {
            Vector3 pp = miniCamera.WorldToViewportPoint(target.position+offect);
            iconItem.anchoredPosition = MiniMapUtils.CalculateMiniMapPosition(pp, mapUIRoot);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        public void Update()
        {
            UpdatePosition();
        }
    }
}

