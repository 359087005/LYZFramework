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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lazy
{
    public class MainMenuPanel : UIPanelBase
    {
        void Start()
        {
            transform.FindFormAllChild<Button>("btn开始实验").GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadSceneManager.Instance.LoadScene("LazyDemo02");
                UIFrame.Instance.ClearAll();
                UIFrame.Instance.goBack_btn.SetActive(false);
                UIFrame.Instance.canClose = true;
            });
            transform.FindFormAllChild<Button>("btn背包").onClick.AddListener(() =>
            {
                UIFrame.Instance.Open("背包");
            });
            transform.FindFormAllChild<Button>("btn滑动列表").onClick.AddListener(() =>
            {
                UIFrame.Instance.Open("滑动列表");
            });

            transform.FindFormAllChild<Button>("btn特效展示").onClick.AddListener(() =>
            {
                UIFrame.Instance.Open("特效展示");
            });
            transform.FindFormAllChild<Button>("btn特效展示").gameObject.OnMouseEnter((g) =>
            {
                g.OnUIJianBian_Gradient(Color.green, Color.blue, Color.yellow, Color.green, 1f, 360f);
            });
            transform.FindFormAllChild<Button>("btn特效展示").gameObject.OnMouseExit((g) =>
            {
                g.OnUIJianBian_Gradient(Color.white, Color.white);
            });
            transform.FindFormAllChild<Button> ("btn滑动列表").gameObject.OnMouseEnter((g)=>
            {
                g.OnUIJianBian_Gradient(Color.white, Color.black, true);
            });
            transform.FindFormAllChild<Button>("btn滑动列表").gameObject.OnMouseExit((g) =>
            {
                g.OnUIJianBian_Gradient(Color.black, Color.white, false);
            });

            //UIFrame.Instance.homePage_btn.GetComponent<Button>().onClick.AddListener(() =>
            //{
            //    MessageCenter.SendEvent(new Message((ushort)UIFrame.CommonEvent.homePage, this));
            //});
        }

        // Update is called once per frame
        void Update()
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
            
        }

        public override void OnExit()
        {
            base.OnExit();

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

            switch (msg.id)
            {
                case (ushort)UnitTest.UTEvents.openYuXiPanel:
                    UIFrame.Instance.Open("预习");
                    break;
                case (ushort)UnitTest.UTEvents.startShiYan:
                    //FindObjectOfType<SceneLoadMgr>().LoadScene("02", () =>
                    //{
                        UIFrame.Instance.Open("背包");
                    //});
                    break;
                default:
                    break;
            }
        }
    }
}
