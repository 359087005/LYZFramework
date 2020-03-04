
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestQiuModel
* 创建日期：2019-04-17 10:45:21
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace IoCAndTwoCommunication
{
    /// <summary>
    /// 
    /// </summary>
	public class TestQiuModel : CommonDataModel
    {

        private void Awake()
        {
            this.DataEntity = new TestQiuEntity();
            Watch(this);
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void OnEvent(PropertyMessage evt)
        {

            bool isContentVlaue = false;
            for (int i = 0; i < watchTable.Items.Count; i++)
            {
                if (watchTable.Items[i].Path.Equals(evt.PropertyName))
                {
                    isContentVlaue = true;
                }
            }
            if (!isContentVlaue) return;

            base.OnEvent(evt);
        }
    }
}

