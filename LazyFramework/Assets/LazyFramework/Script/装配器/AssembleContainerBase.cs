using System;
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：AssembleContainerBase
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：容器基类
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Lazy
{
    public class AssembleContainerBase : MonoBehaviour
    {
        /// <summary>
        /// 容器名称
        /// </summary>
        public string containerName = "";
        /// <summary>
        /// 是否激活状态
        /// </summary>
        public bool isActivate = true;
        /// <summary>
        /// 已经装配次数
        /// </summary>
        [HideInInspector]
        public int useCount;
        /// <summary>
        /// 容器是否可用
        /// </summary>
        public bool CanUse
        {
            get
            {
                //if (!isActivate) return false;
                if (existObj != null) return false;
                return true;
            }
        }
        /// <summary>
        /// 容器类型
        /// </summary>
        public AssembleType assembleType;
        /// <summary>
        /// 容器中已有物品
        /// </summary>
        [HideInInspector] public AssembleAcceptBase existObj;
        /// <summary>
        /// 装配目标点
        /// </summary>
        public List<Transform> animPoint;
        [HideInInspector]
        public bool finishAssmeble = false;
        public delegate void AssembleEventHandler(AssembleContainerBase container = null);
        public AssembleEventHandler OnAssembleBegin;
        public AssembleEventHandler OnAssembleRelieve;
        protected virtual void Start()
        {
            if(containerName=="")
            {
                containerName = gameObject.name;
            }
            if(animPoint.Count==0)
            {
                animPoint.Add(transform);
            }
        }
        /// <summary>
        /// 开始装配
        /// </summary>
        /// <param name="_assembleAction"></param>
        public virtual void BeginAssmeble(AssembleAcceptBase _assembleAction)
        {
            finishAssmeble = false;
            existObj = _assembleAction;
            useCount++;
            if(OnAssembleBegin!=null)
            {
                OnAssembleBegin.Invoke(this);
            }
        }
        /// <summary>
        /// 结束装配
        /// </summary>
        public virtual void FinishAssmeble()
        {
            finishAssmeble = true;
        }
        /// <summary>
        /// 注销装配
        /// </summary>
        public virtual void RelieveAssmeble()
        {
            finishAssmeble = false;
            existObj = null;
            if(OnAssembleRelieve!=null)
            {
                OnAssembleRelieve.Invoke(this);
            }
        }
    }
}
