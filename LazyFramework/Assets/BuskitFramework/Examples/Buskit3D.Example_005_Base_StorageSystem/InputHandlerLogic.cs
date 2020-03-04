/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： InputHandlerLogic
* 创建日期：2019-01-10 11:52:39
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：输入处理逻辑
******************************************************************************/
using UnityEngine;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D_Example_005_StorageSystem
{
    /// <summary>
    /// 输入逻辑处理
    /// </summary>
    public class InputHandlerLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("prefabInfo"))
            {
                //生成预制体方式1
                PrefabInfo info = (PrefabInfo)evt.NewValue;
                GameObject go = ReplayManager.GetInstance(info.nameOfPrefab, info.position);
                //方式2
                //Instantiate(Resources.Load("your prefab path"));

                //注册预制体
                ReplayManager.RegisterPrefab(go);
            } 
        }
    }
}

