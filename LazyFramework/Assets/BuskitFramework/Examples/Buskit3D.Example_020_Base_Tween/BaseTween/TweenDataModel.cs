/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TweenDataModel
* 创建日期：2019-01-14 14:43:20
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit3D;
using System.Collections.Generic;

namespace Buskit3D.Example_020_Tween
{
     /// <summary>
     /// 动画数据载体
     /// </summary>
    [RequireComponent(typeof(TweenLogic))]
	public class TweenDataModel : DataModelBehaviour
	{

        //动画队列
        public List<TweenArgs> allTween = new List<TweenArgs>();

        /// <summary>
        /// Unity Method
        /// </summary>
        private void Awake()
        {
            TweenEntity data = new TweenEntity();
            DataEntity = data;
            Watch(this);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        protected override  void Start()
        {
            base.Start();
            gameObject.SetClick(a => ToNextTween());
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        public override void Update()
        {
            base.Update();
            InitMethod();
        }

        /// <summary>
        /// 测试触发动画方法
        /// </summary>
        public void InitMethod()
        {
            //中键添加关键帧
            if (Input.GetMouseButtonDown(2))
            {
                AddTweenArgs();
            }
        }

        /// <summary>
        /// 添加动画参数到队列当中
        /// </summary>
        public virtual void AddTweenArgs()
        {
            TweenEntity data = (TweenEntity)DataEntity;
            TweenArgs args;
            Vector3 screenZ = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenZ.z));
            args.targetValue = world;
            args.tweenTime = 2f;
            args.tweenName = allTween.Count.ToString();
            allTween.Add(args);
        }

        /// <summary>
        /// 播放列表中的下一个动画
        /// </summary>
        public virtual void ToNextTween()
        {
            if (allTween.Count > 0)
            {
                TweenEntity data = (TweenEntity)DataEntity;
                if (data.number >= allTween.Count)
                    return;
                TweenArgs args = allTween[data.number];
                data.tweenArgs = args;
                data.number += 1;
            }
        }
    }
}

