/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ObjectShapLogic
* 创建日期：2019-01-08 11:23:36
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：物体具体的逻辑处理
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D_Example_005_StorageSystem
{
    /// <summary>
    /// 实例化物体的行为逻辑
    /// </summary>
    public class ObjectShapLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("position"))
            {
                UpdateTrans((Vector3)evt.NewValue);
            }
            if (evt.PropertyName.Equals("normalColor"))
            {
                transform.GetComponent<MeshRenderer>().material.SetColor("_Color", (Color32)evt.NewValue);
            }
        }

        /// <summary>
        /// 更新Transform信息
        /// </summary>
        /// <param name="target"></param>
        public void UpdateTrans(Vector3 target)
        {
            transform.position = target;
        }
    }
}

