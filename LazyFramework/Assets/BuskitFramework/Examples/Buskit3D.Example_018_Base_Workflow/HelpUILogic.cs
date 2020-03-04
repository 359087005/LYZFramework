/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： HelpUILogic
* 创建日期：2018-12-26 10:54:46
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_018_Workflow
{
    /// <summary>
    /// 帮助逻辑处理类
    /// </summary>
    public class HelpUILogic : LogicBehaviour
    {
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //UI开关处理
            if (evt.PropertyName.Equals("isShow"))
            {
                transform.GetChild(0).gameObject.SetActive((bool)evt.NewValue);
            }
        }
    }
}