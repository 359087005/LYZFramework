/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIframeTest2
* 创建日期：2020-01-08 17:13:50
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class UIframeTest2 : MonoBehaviour
    {
        public UIPanelBase panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8, panel9;
        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            UIFrame.Instance.homePage_btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                MessageCenter.SendEvent(new Message((ushort)UIFrame.CommonEvent.homePage, panel1));
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p1/Button").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel2);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p1/Button (1)").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel3);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p1/Button (2)").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel4);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p1/Button (3)").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel5);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p2/Button").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel6);

            });
            GameObject.Find("GameMain/Canvas/UIPanels").transform.Find("p2/Button (1)").gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel7);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p2/Button (2)").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel8);
            });
            GameObject.Find("GameMain/Canvas/UIPanels/p2/Button (3)").SetClick((g) =>
            {
                UIFrame.Instance.Open(panel9);
            });
            panel3.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel4);
            });

            panel4.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel5);
            });
            panel5.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel1);
            });
            panel6.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel7);
            });

            panel7.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel8);
            });
            panel8.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel9);
            });
            panel9.GetComponentInChildren<Button>().gameObject.SetClick((g) =>
            {
                UIFrame.Instance.Open(panel2);
            });


            UIFrame.Instance.Open(panel1);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
        }
    }
}

