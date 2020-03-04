/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：InputFieldAutoScale
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：绑定高度
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    [RequireComponent(typeof(Image))]
    public class BindingImageSize : MonoBehaviour
    {
        RectTransform myTrans;
        [SerializeField] RectTransform tarTrans;
        [SerializeField]BindingType bindingType;
        private void Start()
        {
            InitFunc();
        }
        private void FixedUpdate()
        {
            BindingSize();
        }
        private void InitFunc()
        {
            myTrans = this.GetComponent<RectTransform>();
            Vector2 temp = tarTrans.sizeDelta;
            tarTrans.anchorMax = new Vector2(0.5f, 0.5f);
            tarTrans.anchorMin = new Vector2(0.5f, 0.5f);
            tarTrans.sizeDelta = temp;
            if (tarTrans.GetComponent<ContentSizeFitter>() == null)
            {
                tarTrans.gameObject.AddComponent<ContentSizeFitter>();
            }
            tarTrans.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            tarTrans.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        private void BindingSize()
        {
            switch (bindingType)
            {
                case BindingType.height:
                    myTrans.sizeDelta = new Vector2(myTrans.sizeDelta.x, tarTrans.sizeDelta.y);
                    break;
                case BindingType.width:
                    myTrans.sizeDelta = new Vector2(tarTrans.sizeDelta.x, myTrans.sizeDelta.y);
                    break;
                case BindingType.widthAndHight:
                    myTrans.sizeDelta = tarTrans.sizeDelta;
                    break;
            }
        }
    }
    enum BindingType
    {
        height,
        width,
        widthAndHight
    }
}