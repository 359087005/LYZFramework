/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：LightControlLogic
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：灯控制业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine;

namespace Buskit3D.Training.IoC.C
{
    /// <summary>
    /// 灯控制业务逻辑
    /// </summary>
    public class LightControlLogic : CommonLogic
    {
        /// <summary>
        /// 灯控制业务逻辑处理
        /// </summary>
        /// <param id="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            //处理颜色变化
            if (evt.PropertyName.Equals("localPositon"))
            {
                gameObject.transform.localPosition = (Vector3)evt.NewValue;
                return;
            }
        }
    }
}


