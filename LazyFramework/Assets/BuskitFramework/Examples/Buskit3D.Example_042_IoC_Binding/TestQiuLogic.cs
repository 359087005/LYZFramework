/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestQiuLogic
* 创建日期：2019-04-17 10:47:40
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
    public class TestQiuLogic : CommonLogic
    {
        protected override void Start()
        {
            base.Start();

        }

        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName)
            {
                case "currentState":
                    int value = (int)evt.NewValue;
                    switch (value)
                    {
                        case 0:
                            Debug.Log("手机当前离线");
                            break;
                        case 1:
                            Debug.Log("手机当前在线");
                            break;
                        case 2:
                            JLLJ();
                            break;
                    }
                    break;
                case "bindQiu":
                    if (evt.NewValue != null)
                    {


                    }
                    else
                    {

                    }
                    break;
                case "bindingTa":
                    if (evt.NewValue != null)
                    {
                        CFQN();
                    }
                    else
                    {
                        CFQW();
                    }
                    break;
                case "callList#Add":
                    int _value = (int)evt.NewValue;
                    AddQiu(_value);
                    break;
                case "callList#Remove":
                    RemoveQiu();
                    break;

            }
        }

        /// <summary>
        /// 触发器外
        /// </summary>
        public void CFQW()
        {
            var entity = utilsEntity.GetEntity<TestQiuEntity>(gameObject);
            entity.currentState = 0;
        }
        /// <summary>
        /// 触发器内
        /// </summary>
        public void CFQN()
        {
            var entity = utilsEntity.GetEntity<TestQiuEntity>(gameObject);
            entity.currentState = 1;
        }
        /// <summary>
        /// 触发器中
        /// </summary>
        public void JLLJ()
        {
            Debug.Log("我要打电话");
        }

        public void AddQiu(int value)
        {            
            Debug.Log("收到广播信号");
            var entity = utilsEntity.GetEntity<TestQiuEntity>(gameObject);
            if (value == entity.objectID) {
                Debug.Log("建立通讯成功");
            }
        }
        public void RemoveQiu()
        {
            Debug.Log("移除球");
        }
    }
}

