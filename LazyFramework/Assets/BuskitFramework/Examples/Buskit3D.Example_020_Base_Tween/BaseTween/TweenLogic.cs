/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TweenLogic
* 创建日期：2019-01-14 14:43:31
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Buskit3D.Example_020_Tween
{
    /// <summary>
    /// 动画行为逻辑
    /// </summary>
    public class TweenLogic : LogicBehaviour
    {

        /// <summary>
        /// 对当前物体某个动画结束后，感兴趣的下一个观察者
        /// </summary>
        [Header("对当前物体某个动画结束后，感兴趣的下一个观察者")]
        public GameObject observer;

        public override void ProcessLogic(PropertyMessage evt)
        {
            if (evt.PropertyName.Equals("tweenArgs"))
            {
                DoSelfTween(evt);
            }
        }

        /// <summary>
        /// 动画结束，根据当前完成的动画名字，处理Next事件
        /// </summary>
        /// 
        public  virtual void OnComplete(TweenArgs args)
        {
           
           
            switch (args.tweenName)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 播放自身动画
        /// </summary>
        /// <param name="args"></param>
        public void DoSelfTween(PropertyMessage evt)
        {
                StartCoroutine(SimulateDoTween((TweenArgs)evt.NewValue));
        }

        /// <summary>
        /// 模拟Dotween
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator SimulateDoTween(TweenArgs args)
        {
            float timer = 0;
          
            while (args.tweenTime > timer)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, args.targetValue, Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            OnComplete(args);
        }
    }
}

