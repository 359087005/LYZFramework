/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：AssembleAcceptBase
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：装配物基类
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lazy
{
    /// <summary>
    /// 装配物类
    /// </summary>
    public class AssembleAcceptBase : MonoBehaviour
    {
        /// <summary>
        /// 物体名称
        /// </summary>
        public string acceptName;
        /// <summary>
        /// 是否激活装配
        /// </summary>
        public bool isActive = false;
        /// <summary>
        /// 装配次数
        /// </summary>
        [HideInInspector]public int useCount;
        /// <summary>
        /// 可置入的容器类型
        /// </summary>
        public AssembleType assembleType;
        /// <summary>
        /// 需要装配的物体
        /// </summary>
        [HideInInspector]public GameObject obj;
        /// <summary>
        /// 容器
        /// </summary>
        [HideInInspector]public AssembleContainerBase containerBase;
        public delegate void AssembleEventHandler(AssembleAcceptBase sender);
        /// <summary>
        /// 当装配开始时
        /// </summary>
        public event AssembleEventHandler OnAssembleBegin;
        /// <summary>
        /// 当装配结束时
        /// </summary>
        public event AssembleEventHandler OnAssembleFinish;
        /// <summary>
        /// 当装配解除时
        /// </summary>
        public event AssembleEventHandler OnAssembleRelieve;
        [HideInInspector]public AssembleManager assembleManager;
        protected virtual void Start()
        {
            if (acceptName == "")
            {
                acceptName = gameObject.name;
            }
            if(obj== null)
            {
                obj = gameObject;
            }
            if(assembleManager==null)
            {
                assembleManager = AssembleManager.Instance;
            }
        }
        /// <summary>
        /// 判断能否装配
        /// </summary>
        /// <param name="_assembleContainerBase"></param>
        /// <returns></returns>
        public virtual bool JugeAssmeble(AssembleContainerBase _assembleContainerBase)
        {
            if (_assembleContainerBase == null) return false;//如果射线没有打到容器，则不能执行装配
            if (containerBase != null) return false;//如果当前装配物已被装配，则不能执行装配
            if (!_assembleContainerBase.CanUse) return false;//如果容器不可用，则不能执行装配
            if (_assembleContainerBase.assembleType != assembleType) return false;//如果容器类型不匹配，则不能执行装配
            if (_assembleContainerBase.gameObject == gameObject) return false;//如果物体既是装配物又是容器，防止自身装配至自身的容器中。
            return true;
        }
        /// <summary>
        /// 开始装配
        /// </summary>
        /// <param name="_containerBase"></param>
        public virtual void BeginAssmeble(AssembleContainerBase _containerBase)
        {
            useCount++;
            containerBase = _containerBase;
            containerBase.BeginAssmeble(this);
            if (OnAssembleBegin!=null)
                OnAssembleBegin.Invoke(this);
              
        }
        /// <summary>
        /// 装配完成
        /// </summary>
        public virtual void FinishAssmeble()
        {
            if (containerBase != null)
            {
                containerBase.FinishAssmeble();
                if (OnAssembleFinish != null)
                    OnAssembleFinish.Invoke(this);
            }
        }
        /// <summary>
        /// 注销装配
        /// </summary>
        public virtual void RelieveAssmeble()
        {
            if (containerBase != null)
            {
                if (OnAssembleRelieve != null)
                    OnAssembleRelieve.Invoke(this);
            }
               
        }
    }
}
