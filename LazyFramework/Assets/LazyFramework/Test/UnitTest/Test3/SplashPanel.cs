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
using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    public class SplashPanel : UIPanelBase
    {
        private void Start()
        {
            this.Register((ushort)UnitTest.UTEvents.enterMainMenu);
            transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                UIFrame.Instance.Open("主菜单");
            });
        }

        public override void OnEnter()
        {
            base.OnEnter();
            UIFrame.Instance.goBack_btn.SetActive(false);
            UIFrame.Instance.homePage_btn.SetActive(false);
            GetComponent<UITransitionEffect>().Show();

        }

        public override void OnExit()
        {
            base.OnExit();
            GetComponent<UITransitionEffect>().Hide();

        }

        public override void OnPause()
        {

            base.OnPause();
            UIFrame.Instance.goBack_btn.SetActive(true);
         
            UIFrame.Instance.homePage_btn.SetActive(true);
            GetComponent<UITransitionEffect>().Hide();

        }

        public override void OnResume()
        {
            base.OnResume();
            UIFrame.Instance.goBack_btn.SetActive(false);
      
            UIFrame.Instance.homePage_btn.SetActive(false);
            GetComponent<UITransitionEffect>().Show();
        }
        public override void ProcessEvent(Message msg)
        {
            if (msg.id == (ushort)UnitTest.UTEvents.enterMainMenu)
            {
                UIFrame.Instance.Open("主菜单");
            }
        }
    }


}
