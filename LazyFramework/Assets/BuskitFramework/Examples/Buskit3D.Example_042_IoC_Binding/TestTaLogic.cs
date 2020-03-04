/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestTaLogic
* 创建日期：2019-04-17 11:01:03
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace IoCAndTwoCommunication
{
    /// <summary>
    /// 
    /// </summary>
    public class TestTaLogic : CommonLogic
    {

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {
                case "testQiuEntityList#Add":
                    AddQiu();
                    break;
                case "testQiuEntityList#Remove":
                    RemoveQiu();
                    break;                
            }
        }

        public void AddQiu() {
            Debug.Log("添加了一个球");
        }

        public void RemoveQiu() {
            Debug.Log("删除了一个球");
        }
    }
}

