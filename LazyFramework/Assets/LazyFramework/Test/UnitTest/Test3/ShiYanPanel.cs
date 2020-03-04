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
    public class ShiYanPanel : UIPanelBase
    {
      
        // Start is called before the first frame update
        void Start()
        {
            showType = ShowType.hideOtherPanels;
            //var allBags = GetComponentsInChildren<BagBase>();
            //for (int i = 0; i < allBags.Length; i++)
            //{
            //    allBags[i].onPutIn = (item) =>
            //    {
            //      var c =  GameObject.CreatePrimitive(PrimitiveType.Cube);
            //        c.name = item.info;
            //        item.Deleted();
            //    };
            //}
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
    }
}
