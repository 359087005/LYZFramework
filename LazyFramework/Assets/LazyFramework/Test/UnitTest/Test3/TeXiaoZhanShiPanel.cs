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
using Coffee.UIExtensions;

namespace Lazy
{
    /// <summary>
    /// 
    /// </summary>
	public class TeXiaoZhanShiPanel : UIPanelBase
    {
        private Text tex;
        public bool open;

        public override void OnEnter()
        {
            base.OnEnter();
            transform.Find("cover").gameObject.GetComponent<UITransitionEffect>().Hide();
            this.SetDelay(2f, () =>
            {
#if UNITY_2018_3_0

                transform.Find("Image").gameObject.OnUIDissolve(Color.blue, true);

                transform.Find("Image (2)").gameObject.OnUIShiny();
                transform.Find("Image (2)/Text").gameObject.OnUIShiny();
#endif

                transform.Find("Image (3)").gameObject.OnUIJianBian_Gradient(Color.white, Color.black, true);
                transform.Find("Image (3)/Text").gameObject.OnUIJianBian_Gradient(Color.white, Color.black, true);

                transform.Find("Image (4)").gameObject.OnUIJianBian_Gradient(Color.white, Color.black, Color.white, Color.black, 2f, 720);
                transform.Find("Image (4)/Text").gameObject.OnUIJianBian_Gradient(Color.white, Color.black, Color.white, Color.black, 2f, 720);
            });
        }

        public override void OnExit()
        {
            base.OnExit();
#if UNITY_2018_3_0

            transform.Find("cover").gameObject.GetComponent<UITransitionEffect>().Show();
#endif

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

        }

        private void Awake()
        {
            this.Register((ushort)UnitTest.UTEvents.TiShi);

        }
        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            //transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            //{
            //    MDUIFrame.Instance.CloseOtherPanels();
            //    FindObjectOfType<SceneLoadMgr>().LoadScene("UIEffect_Demo");
            //});
        }

        private void TiShiPanel1_onValueChanged(GameObject obj)
        {
            if (!tex)
                tex = transform.Find("Text").GetComponent<Text>();

            tex.text = obj.name;
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update()
        {
#if UNITY_2018_3_0
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                transform.Find("Image").gameObject.OnUIDissolve(Color.blue, open);
                open = !open;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                transform.Find("Image (2)").gameObject.OnUIShiny(45f);
                transform.Find("Image (2)/Text").gameObject.OnUIShiny(45f);

            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                transform.Find("Image (5)").gameObject.OnUIGray();
            }
#endif
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

                transform.Find("Image (3)").gameObject.OnUIJianBian_Gradient(Color.white, Color.blue, open);
                transform.Find("Image (3)/Text").gameObject.OnUIJianBian_Gradient(Color.white, Color.red, open);
                open = !open;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Color pur = new Color(1, 0, 1, 1);
                transform.Find("Image (4)").gameObject.OnUIJianBian_Gradient(Color.red, Color.blue, pur, pur, 2f, 720);
                transform.Find("Image (4)/Text").gameObject.OnUIJianBian_Gradient(Color.green, Color.yellow, pur, pur,  2f, 720);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                transform.Find("Image (6)").gameObject.OnUIOutLine(open);
                open = !open;

            }
        }
    }
}

