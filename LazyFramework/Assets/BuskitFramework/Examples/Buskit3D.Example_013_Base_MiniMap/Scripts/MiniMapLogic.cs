
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MiniMapItem
* 创建日期：2018-12-19 09:25:22
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_013_MiniMap
{
    /// <summary>
    /// 小地图行为逻辑
    /// </summary>
    public class MiniMapLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("eventID"))
            {
                long eventID = (long)evt.NewValue;
                switch (eventID) {
                    case 0:
                        Debug.Log("000");
                        break;
                    case 1:
                        Debug.Log("111");
                        break;
                    case 2:
                        Debug.Log("222");
                        break;
                }
            }            
        }
    }
}
