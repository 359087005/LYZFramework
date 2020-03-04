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
	public class TiShiPanel1 : UIPanelBase 
	{
        private Text tex;

        public override void OnEnter()
        {
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
            FindObjectOfType<ScrollList>().onValueChanged += TiShiPanel1_onValueChanged;
        }

        private void TiShiPanel1_onValueChanged(GameObject obj)
        {
            if(!tex)
            tex = transform.Find("Text").GetComponent<Text>();

            tex.text = obj.name;
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Update ()
		{
		}
	}
}

