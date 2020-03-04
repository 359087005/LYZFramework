/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MemoryTestLogic
* 创建日期：2019-04-03 09:08:38
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_040_MemoryPlayer
{

    /// <summary>
    /// 
    /// </summary>
    public class MemoryTestLogic : LogicBehaviour
    {
        MemoryTestEntity entity;
        private void Start()
        {
            entity = (MemoryTestEntity)FindObjectOfType<MemoryTestDataModel>().DataEntity;

        }
        public override void ProcessLogic(PropertyMessage evt)
        {
            Debug.Log(evt.PropertyName);
            switch (evt.PropertyName)
            {
                case "watchTableList#Add":
                    Debug.Log(evt.NewValue);
                    break;
                case "watchTableList#Remove":
                    Debug.Log(evt.NewValue);
                    break;
                case "watchTableListStruct#RemoveAt":
                    Debug.Log(evt.NewValue);
                    break;
                case "watchTableList#Clear":
                    Debug.Log(evt.NewValue);
                    break;
                case "watchTableList#Insert":
                    Debug.Log(evt.NewValue);
                    break;
                case "watchTableList#[]":
                    Debug.Log(evt.NewValue);
                    break;
            }
        }
    }
}

