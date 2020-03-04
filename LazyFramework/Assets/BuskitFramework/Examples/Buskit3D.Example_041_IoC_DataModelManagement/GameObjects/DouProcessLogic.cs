/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：CommonDataModel
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：实体类工具，用来查找一个GameObject上的实体对象、数据模型、业务逻辑
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine;

namespace Buskit3D.Training.IoC.E
{ 
    public class DouProcessLogic : CommonLogic 
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("visible"))
            {
                bool visible = (bool)evt.NewValue;
                if (visible)
                {
                    gameObject.transform.localScale = Vector3.one;
                }
                else
                {
                    gameObject.transform.localScale = Vector3.zero;
                }
            }
        }
    }
}
