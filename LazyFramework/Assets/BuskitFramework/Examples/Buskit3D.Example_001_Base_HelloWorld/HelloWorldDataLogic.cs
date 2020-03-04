/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：UIController
* 创建日期：2019-01-08 11:30:17
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 功能描述：框架Demo_001(业务逻辑)
* 修改记录：
* 日期 描述：
* 
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Com.Rainier.Buskit3D.Example_001
{  
    /// <summary>
    /// 业务逻辑处理类
    /// </summary>
    public class HelloWorldDataLogic : LogicBehaviour
    {
        //logic字框
        public Text logicText;
        //entity字框
        public Text entityText;
        //“HelloWorld”UI
        public GameObject obj;

        /// <summary>
        /// 业务逻辑处理函数 
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(PropertyMessage evt)
        {
            Debug.Log("123");

            if(evt.PropertyName.Equals("IsShow"))
            {
                if ((bool)evt.NewValue)
                {
                    obj.gameObject.SetActive(true);
                }
                else
                    obj.gameObject.SetActive(false);
                entityText.text = "Entity实体数据变化:\n" + "IsShow:" + evt.OldValue + "->" + evt.NewValue;
                logicText.text = "Logic收到消息\n" + "Name:" + evt.PropertyName + "\n" + "OldValue:" + evt.OldValue + "\n" + "NewValue:" + evt.NewValue;
            }
        }
    }
}



