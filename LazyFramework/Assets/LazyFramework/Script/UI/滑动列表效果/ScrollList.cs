/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ScrollList
* 创建日期：2019-12-19 16:50:12
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：滑动列表效果
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class ScrollList : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [Header("归位时长")]
        public float xifuDuration = 0.5f;
        [Header("scale曲线")]
        public AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [Header("收尾相连效果")]
        public bool endToEnd = true;
        public GameObject itemsParent;
        public Action<GameObject> onValueChanged;
        private GameObject[] allItems;
        private float width;

        //private float minscale = 0.3f;
        private float curClosestDistance;
        private int curIndex;
        /// <summary>
        /// 吸附归位
        /// </summary>
        private bool xiFuOn;

        private float xiFuTimeCounter;
        private Vector3 startPos;
        private float itemWidth;
        private float totalWidth;
        [Header("首尾相连的间隔")]
        public float endToEndSpacing;
        private HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup;
        public GameObject CurSelectedGameObject;

        public int CurIndex
        {
            get => curIndex;
            private set
            {
                if (value != curIndex)
                {
                    OnValueChanged(value);
                    curIndex = value;
                }
            }
        }

        public Vector2 referenceResolution = new Vector2(960f, 540f);

        private float ScreenScale
        {
            get
            {
                return Screen.width / referenceResolution.x;
            }
        }

        private void OnValueChanged(int index)
        {
            if (index < allItems.Length)
            {
                ControlSortingOrder(index);

                if (onValueChanged != null)
                    onValueChanged(allItems[index]);

                CurSelectedGameObject = allItems[index];
            }
        }

       

        /// <summary>
        /// 控制两侧遮挡顺序
        /// </summary>
        /// <param name="index"></param>
        private void ControlSortingOrder(int index)
        {
            //10->6, 11->6 , 12->7
            // 9 8 7 6 5  
            //0 
            // 1 2 3 4 5

            // 5 4 3 2 1
            //6
            // 7 8 9 0 1

            // 5 4 3 2  1 
            //6
            // 7 8 9 10 0 

            for (int i = 0; i < (allItems.Length - 1) / 2 + (allItems.Length - 1) % 2 + 1; i++)
            {
                var left = index - i;
                left = left < 0 ? allItems.Length + left : left;
                var right = index + i;
                right = right > allItems.Length - 1 ? right - allItems.Length : right;
                allItems[left].transform.SetSiblingIndex(0);
                allItems[right].transform.SetSiblingIndex(0);

            }

        }


        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Vector3 direction)
        {
            //移动
            var offset_x = direction.x;
            for (int i = 0; i < allItems.Length; i++)
            {

                RectMove(allItems[i], offset_x);

                #region 收尾相连
                if (endToEnd)
                {

                    //移出父物体边界后两边补齐
                    //向右移动补齐
                    float hideBord = (width / 2 + itemWidth / 2);
                    if (direction.x > 0 && allItems[i].GetComponent<RectTransform>().localPosition.x > hideBord)
                    {
                        RectMove(allItems[i], -totalWidth);
                    }
                    //向左移动补齐
                    else if (direction.x < 0 && allItems[i].GetComponent<RectTransform>().localPosition.x < -hideBord)
                    {
                        RectMove(allItems[i], totalWidth);
                    }

                }
                #endregion

            }

            UpdateScale();

        }

        private void RectMove(GameObject go, float offset_x)
        {
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            var anchoredPos = rectTransform.anchoredPosition;
            anchoredPos += (Vector2.right * offset_x);
            rectTransform.anchoredPosition = anchoredPos;
        }

        /// <summary>
        /// 根据本地坐标调整大小
        /// </summary>
        private void UpdateScale()
        {
            var closet = 0f;
            for (int i = 0; i < allItems.Length; i++)
            {
                var x = allItems[i].GetComponent<RectTransform>().localPosition.x;
                //print(all[i].gameObject.name + ":" + x);
                float abs_x = Mathf.Abs(x);
                if (i == 0)
                {
                    closet = abs_x;
                    //print("最近的" + closet);
                }
                float ratio = (1 - abs_x / (width / 2));

                var scale = scaleCurve.Evaluate(ratio);
                allItems[i].GetComponent<RectTransform>().localScale = Vector3.one * scale;

                if (abs_x <= closet)
                {
                    closet = abs_x;
                    if (!xiFuOn)
                        curClosestDistance = x;
                    CurIndex = i;
                }
            }
        }



        private void Awake()
        {

        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {


            width = itemsParent.GetComponent<RectTransform>().rect.width;

            //加载所有item
            allItems = new GameObject[itemsParent.transform.childCount];
            for (int i = 0; i < itemsParent.transform.childCount; i++)
            {
                allItems[i] = itemsParent.transform.GetChild(i).gameObject;
            }
            if (allItems.Length == 0)
            {
                return;
            }

            //单个item的宽度
            itemWidth = allItems[0].GetComponent<RectTransform>().rect.width;

            //没设置收尾间隔的话就采样一个
            if (endToEndSpacing == 0f && allItems.Length >= 2)
                endToEndSpacing = (allItems[1].GetComponent<RectTransform>().localPosition.x - allItems[0].GetComponent<RectTransform>().localPosition.x);

            totalWidth = allItems[allItems.Length - 1].GetComponent<RectTransform>().localPosition.x - allItems[0].GetComponent<RectTransform>().localPosition.x + endToEndSpacing;

            //初始化
            JumpTo(0);

            //关闭layout的影响
            if (endToEnd)
            {
                if (!horizontalOrVerticalLayoutGroup)
                    horizontalOrVerticalLayoutGroup = itemsParent.GetComponent<HorizontalOrVerticalLayoutGroup>();
                if (horizontalOrVerticalLayoutGroup)
                    horizontalOrVerticalLayoutGroup.enabled = false;
            }

            //跳转
            var jump = transform.Find("InputField/Button").GetComponent<Button>();
            var jumpInputField = transform.Find("InputField").GetComponent<InputField>();

            if (jump && jumpInputField)
            {
                jump.onClick.AddListener(() =>
                {
                    int index; ;
                    int.TryParse(jumpInputField.text, out index);
                    JumpTo(index);
                });
            }
        }

        public void JumpTo(int index)
        {
            if (index >= 0 && index < allItems.Length)
            {
                var distance = allItems[index].GetComponent<RectTransform>().localPosition.x;
                curClosestDistance = distance;
                xiFuOn = true;
            }
        }


        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
            //吸附归位
            if (xiFuOn)
            {
                float v = ((xiFuTimeCounter + Time.deltaTime) > xifuDuration ? (xifuDuration - xiFuTimeCounter) : Time.deltaTime);
                Move(Vector3.left * curClosestDistance * v / (xifuDuration));
                xiFuTimeCounter += Time.deltaTime;
                if (xiFuTimeCounter > xifuDuration)
                {
                    xiFuTimeCounter = 0f;
                    xiFuOn = false;
                }
            }
        }



        public void OnDrag(PointerEventData eventData)
        {

            var dis = Input.mousePosition - startPos;
            startPos = Input.mousePosition;
            Move(dis / ScreenScale);
        }



        public void OnEndDrag(PointerEventData eventData)
        {
            xiFuOn = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            xiFuOn = false;
            xiFuTimeCounter = 0f;
            startPos = Input.mousePosition;
        }
    }
}

