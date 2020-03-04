/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： YuXiPanel
* 创建日期：2019-12-13 11:41:05
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
	public class YuXiPanel : UIPanelBase
    {
        public override void OnEnter()
        {
            transform.Find("Image").gameObject.OnUIJianBian_Gradient(Color.white, Color.black, true);
        }

        public override void OnExit()
        {
            base.OnExit();
            //gameObject.SetActive(false);
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void ProcessEvent(Message msg)
        {
            if (msg.id == (ushort)UnitTest.UTEvents.UpdateYuXiContent)
            {
                transform.Find("Image/title").GetComponent<Text>().text = (string)(msg.arguments[0]);
                transform.Find("Image/content").GetComponent<Text>().text = (string)(msg.arguments[1]);
            }
            if (msg.id == (ushort)UnitTest.UTEvents.YuXiTi)
            {
                UIFrame.Instance.Open("tiShi");
                if ((bool)msg.arguments[0] == true && ((string)msg.arguments[1]).Equals("c"))
                {
                    MessageCenter.SendEvent(new Message((ushort)UnitTest.UTEvents.TiShi, "回答正确"));
                }
                else
                {
                    MessageCenter.SendEvent(new Message((ushort)UnitTest.UTEvents.TiShi, "回答错误"));
                }
            }
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {

            this.Register((ushort)UnitTest.UTEvents.UpdateYuXiContent);
            this.Register((ushort)UnitTest.UTEvents.YuXiTi);
            MessageCenter.SendEvent(new Message((ushort)UnitTest.UTEvents.UpdateYuXiContent, "标题", "内容"));

            var allToggles = GetComponentsInChildren<Toggle>();
            for (int i = 0; i < allToggles.Length; i++)
            {
                Toggle toggle = allToggles[i];
                toggle.onValueChanged.AddListener((b) =>
                {
                    if (b == true)
                    {
                        print("更新预习题");
                        MessageCenter.SendEvent(new Message((ushort)UnitTest.UTEvents.YuXiTi, b, toggle.transform.Find("Label").GetComponent<Text>().text));
                    }
                });
            }

        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
        }
    }
}

