/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： 
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 文本显示逻辑类
    /// </summary>
    public class ShowTextLogic : LogicBehaviour
    {
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //执行刷新文本
            if (evt.PropertyName.Equals("context"))
            {
                GetComponent<Text>().text = evt.NewValue.ToString();
            }
        }
    }
}
