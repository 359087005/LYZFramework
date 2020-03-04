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
	public class TiShiPanel : UIPanelBase 
	{
        public override void OnEnter()
        {
            //gameObject.SetActive(true);
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
            if (msg.id==(ushort)UnitTest.UTEvents.TiShi)
            {
               transform.Find("Text").GetComponent<Text>().text=(string)(msg.arguments[0]);
            }
        }

        private void Awake()
        {
            this.Register((ushort)UnitTest.UTEvents.TiShi);

        }
        /// <summary>
        /// Unity Method
        /// </summary>
        void Start () 
		{
            this.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                MessageCenter.SendEvent(new Message((ushort)UIFrame.CommonEvent.goBack));
            });
            this.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() =>
            {
                MessageCenter.SendEvent(new Message((ushort)UIFrame.CommonEvent.goBack));
            });
        }
	
		/// <summary>
        /// Unity Method
        /// </summary>
		void Update ()
		{
		}
	}
}

