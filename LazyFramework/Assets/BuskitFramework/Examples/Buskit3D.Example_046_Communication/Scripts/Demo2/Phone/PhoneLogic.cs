/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： PhoneLogic
* 创建日期：2019-04-10 14:12:31
* 作者名称：高大旺
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_046_Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class PhoneLogic : LogicBehaviour
    {
        public override void ProcessLogic(PropertyMessage evt)
        {
            switch (evt.PropertyName) {
                case "point":
                    transform.localPosition = (Vector3)evt.NewValue;
                    break;
                case "currentState":
                    int state = (int)evt.NewValue;
                    switch (state) {
                        //待机
                        case 0:
                            NomalState();
                            break;
                            //通话
                        case 1:
                            DoingState();
                            break;
                            //不在服务器
                        case 2:
                            NotService();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 默认状态
        /// </summary>
        public void NomalState()
        {
            GetComponent<Renderer>().material.color = Color.white;
            var entity = (ComminicationEntity)FindObjectOfType<ComminicationViewModel>().DataEntity;
            entity.btnOff++;
        }

        /// <summary>
        /// 通话状态
        /// </summary>
        public void DoingState()
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        /// <summary>
        /// 没有服务状态
        /// </summary>
        public void NotService()
        {
            GetComponent<Renderer>().material.color = Color.gray;
            var entity = (ComminicationEntity)FindObjectOfType<ComminicationViewModel>().DataEntity;
            entity.btnOff++;
        }
    }
}

