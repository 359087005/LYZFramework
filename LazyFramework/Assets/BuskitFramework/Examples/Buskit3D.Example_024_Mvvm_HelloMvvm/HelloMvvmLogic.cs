/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：HelloMvvmLogic
* 创建日期：2019-03-31 14:30:17
* 作者名称：王志远
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述：
* 
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Aop;
using Com.Rainier.Buskit3D;
using UnityEngine;
using UnityEngine.UI;

namespace Buskit3D.Training.Mvvm.A
{
public class HelloMvvmLogic : LogicBehaviour
{
    public override void ProcessLogic(PropertyMessage evt)
    {
        if (evt.PropertyName.Equals("textSize"))
        {
            float newSize = (float)evt.NewValue;
            Text text = gameObject.transform.Find("HelloText").GetComponent<Text>();
            text.transform.localScale = new Vector3(newSize, newSize);
        }
    }
}
}


