/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：ShowValueLogic
* 创建日期：2018-04-07 10:58:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：MVVM例程，以一个简单的配置界面为例，说明MVVM使用方法
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_036_Mvvm_BestPractices
{
    /// <summary>
    /// 显示属性变化事件，测试属性绑定是否正确
    /// </summary>
    public class BagOperationLogic : CommonLogic
    {
        /// <summary>
        /// 显示属性变化事件，测试属性绑定是否正确
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("selectedHua"))
            {
                return;
            }

            if (evt.PropertyName.Equals("selectedNiao"))
            {
                return;
            }

            if (evt.PropertyName.Equals("selectedYu"))
            {
                return;
            }

            if (evt.PropertyName.Equals("selectedChong"))
            {
                return;
            }

            if (evt.PropertyName.Equals("selectedShou"))
            {
                return;
            }

            if (evt.PropertyName.Equals("selectedChong2"))
            {
                return;
            }

            if (evt.PropertyName.Equals("OpenClose"))
            {
                bool openClose = (bool)evt.NewValue;
                if (!openClose)
                {
                    gameObject.transform.localScale = Vector3.zero;
                }
                else
                {
                    gameObject.transform.localScale = Vector3.one;
                }
                return;
            }
        }
    }
}
