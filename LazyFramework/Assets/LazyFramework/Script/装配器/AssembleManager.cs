/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：AssembleManager
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：装配管理器  用来管理所有装配物体和容器，提供通过名称查询物体、一些简单的多个物体拼装成功的条件
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Lazy
{
    public class AssembleManager : MonoSingleton<AssembleManager>
    {
        /// <summary>
        /// 装配物
        /// </summary>
        public List<AssembleAcceptBase> assembleAcceptBases = new List<AssembleAcceptBase>();
        /// <summary>
        /// 容器
        /// </summary>
        public List<AssembleContainerBase> assembleContainerBases = new List<AssembleContainerBase>();
      
        public delegate void AssembleEvtHandler(AssembleAcceptBase t);
        public AssembleEvtHandler OnSelect;
        private AssembleAcceptBase curAcccept = null;
        /// <summary>
        /// 当前装配物
        /// </summary>
        public AssembleAcceptBase CurAcccept
        {
            get
            {
                return curAcccept;
            }
            set
            {
                if (curAcccept != null)
                    curAcccept.OnAssembleBegin -= ListenFunc;
                curAcccept = value;
                if (curAcccept != null)
                    curAcccept.OnAssembleBegin += ListenFunc;
                   
            }
        }
        private void Start()
        {
            InitData();
            OnSelect += OnSelectFunc;
        }
        public void OnSelectFunc(AssembleAcceptBase _assembleAcceptBase)
        {
            CurAcccept = _assembleAcceptBase;
        }
        /// <summary>
        /// 初始化获取场景中所有装配脚本
        /// </summary>
        public void InitData()
        {
            assembleAcceptBases = null;
            assembleContainerBases = null;
            assembleAcceptBases = FindObjectsOfType<AssembleAcceptBase>().ToList();
            assembleContainerBases = FindObjectsOfType<AssembleContainerBase>().ToList();
        }
        public void SetAllAccept(bool isActive)
        {
            for (int i = 0; i < assembleAcceptBases.Count; i++)
            {
                assembleAcceptBases[i].isActive = isActive;
            }
        }
        public void SetAllContainer(bool isActive)
        {
            for (int i = 0; i < assembleContainerBases.Count; i++)
            {
                assembleContainerBases[i].isActivate = isActive;
            }
        }
        /// <summary>
        /// 查找容器（根据容器名字查找容器）
        /// </summary>
        /// <param name="name">容器名称</param>
        /// <param name="isActive">是否激活该容器（若不赋值，则仅查找）</param>
        /// <returns></returns>
        public AssembleContainerBase FindContainerByName(string name,bool? isActive = null)
        {
            for (int i = 0; i < assembleContainerBases.Count; i++)
            {
                if(assembleContainerBases[i].containerName == name)
                {
                    if (isActive != null)
                        assembleContainerBases[i].isActivate = (bool)isActive;
                    return assembleContainerBases[i];
                }
            }
            Debug.LogError("未找到容器：" + name);
            return null;
        }
        /// <summary>
        /// 查找容器（所有该类型的容器）
        /// </summary>
        /// <param name="assembleType">容器类型</param>
        /// <param name="isActive">是否激活该容器（若不赋值，则仅查找）</param>
        /// <returns></returns>
        public List<AssembleContainerBase> FindContainersByType(AssembleType assembleType,bool? isActive =null)
        {
            List<AssembleContainerBase> assembleContainerBasesTemp = new List<AssembleContainerBase>();
            for (int i = 0; i < assembleContainerBases.Count; i++)
            {
                if(assembleContainerBases[i].assembleType == assembleType&& assembleContainerBases[i].CanUse)
                {
                    if (isActive != null)
                        assembleContainerBases[i].isActivate = (bool)isActive;
                    assembleContainerBasesTemp.Add(assembleContainerBases[i]);
                }
            }
            if (assembleContainerBasesTemp.Count == 0) Debug.LogError("未找到类型为：" + assembleType.ToString() + " 的容器");
            return assembleContainerBasesTemp;
        }
        /// <summary>
        /// 查找需要装配的物体
        /// </summary>
        /// <param name="name">需要装配物体的名称</param>
        /// <param name="isActive">是否激活这个物体的装配功能（若不赋值，则仅查找）</param>
        /// <returns></returns>
        public AssembleAcceptBase FindAcceptByName(string name,bool? isActive = null)
        {
            for (int i = 0; i < assembleAcceptBases.Count; i++)
            {
                if (assembleAcceptBases[i].acceptName == name)
                {
                    if (isActive != null)
                        assembleAcceptBases[i].isActive = (bool)isActive;
                    return assembleAcceptBases[i];
                }
            }
            Debug.LogError("未找到需要装配的物品：" + name);
            return null;
        }
        /// <summary>
        /// 查找需要装配的物体
        /// </summary>
        /// <param name="assembleType">需要装配的物体名称</param>
        /// <param name="isActive">是否激活这些物体的装配功能（若不赋值，则仅查找）</param>
        /// <returns></returns>
        public List<AssembleAcceptBase> FindAcceptByType(AssembleType assembleType, bool? isActive = null)
        {
            List<AssembleAcceptBase> assembleAcceptBasesTemp = new List<AssembleAcceptBase>();
            for (int i = 0; i < assembleAcceptBases.Count; i++)
            {
                if (assembleAcceptBases[i].assembleType == assembleType)
                {
                    if (isActive != null)
                        assembleAcceptBases[i].isActive = (bool)isActive;
                    assembleAcceptBasesTemp.Add(assembleAcceptBases[i]);
                }
            }
            if (assembleAcceptBasesTemp.Count == 0) Debug.LogError("未找到类型为：" + assembleType.ToString() + " 的容器");
            return assembleAcceptBasesTemp;
        }
        #region 一些简单的匹配条件
        List<AssembleContainerBase> containeGroupInfos = new List<AssembleContainerBase>();
        Action OnContainerFullAction;
        /// <summary>
        /// 当容器中已满时调用 action
        /// </summary>
        /// <param name="_assembleContainerBases">参与检测的容器</param>
        /// <param name="action">执行的代码</param>
        public void OnContainerFull(List<AssembleContainerBase> _assembleContainerBases, Action action)
        {
            containeGroupInfos = _assembleContainerBases;
            OnContainerFullAction = action;
            CurAcccept.OnAssembleBegin += ListenFunc;
        }
        /// <summary>
        /// 当参数中容器都被填装时调用 Action
        /// </summary>
        /// <param name="_assembleContainerName">参与检测的容器</param>
        /// <param name="action">执行的代码</param>
        public void OnMatch_ContainerFull(List<string> _assembleContainerName, Action action)
        {
            for (int i = 0; i < _assembleContainerName.Count; i++)
            {
                containeGroupInfos.Add(FindContainerByName(_assembleContainerName[i]));
            }
            OnContainerFullAction = action;
        }
        private bool CheckMatch_ContainerFull()
        {
            for (int i = 0; i < containeGroupInfos.Count; i++)
            {
                if (containeGroupInfos[i].existObj == null) return false;
            }
            return true;
        }
        #region 当指定装配物装配到指定容器中时执行
        Action OnMatch_OneToOneAction;
        AssembleAcceptBase acceptBase_OneToOne;
        AssembleContainerBase AssembleContainerBase_OneToOne;
        /// <summary>
        /// 当装配器装配到指定容器中时调用 Action
        /// </summary>
        public void OnMatch_OneToOne(string _acceptName,string _containerName,Action action)
        {
            acceptBase_OneToOne = FindAcceptByName(_acceptName);
            AssembleContainerBase_OneToOne = FindContainerByName(_containerName);
            OnMatch_OneToOneAction = action;
        }
        public bool CheckMatch_OneToOne()
        {
            if (acceptBase_OneToOne.containerBase == AssembleContainerBase_OneToOne) return true;
            return false;
        }
        #endregion
        #region 当装配物装配到多个容器中任意一个容器时执行
        Action OnMatch_OneToManyAction;
        AssembleAcceptBase acceptBase_OneToMany;
        List<AssembleContainerBase> AssembleContainerBase_OneToMany = new List<AssembleContainerBase>();
        public void OnMatch_OneToMany(string _acceptName,List<string> _containerName, Action action)
        {
            acceptBase_OneToMany = FindAcceptByName(_acceptName);
            for (int i = 0; i < _containerName.Count; i++)
            {
                AssembleContainerBase assembleContainerBase;
                if ((assembleContainerBase = FindContainerByName(_containerName[i])) !=null)
                {
                    AssembleContainerBase_OneToMany.Add(assembleContainerBase);
                }
            }
            OnMatch_OneToManyAction = action;
        }
        public bool CheckMatch_OneToMany()
        {
            for (int i = 0; i < AssembleContainerBase_OneToMany.Count; i++)
            {
                if (acceptBase_OneToMany.containerBase == AssembleContainerBase_OneToMany[i]) return true;
            }
            return false;
        }
        #endregion
        public void ActiveGroup()
        {
            for (int i = 0; i < containeGroupInfos.Count; i++)
            {
                containeGroupInfos[i].isActivate = true;
            }
        }
        private void ListenFunc(AssembleAcceptBase _assembleAcceptBase)
        {
            if (OnContainerFullAction != null)
            {
                if (CheckMatch_ContainerFull())
                {
                    Debug.Log("成功配对ContainerFull");
                    OnContainerFullAction.Invoke();
                    containeGroupInfos.Clear();
                    OnContainerFullAction = null;
                }
            }
            if (OnMatch_OneToOneAction != null)
            {
                if (CheckMatch_OneToOne())
                {
                    Debug.Log("成功配对OneToOne");
                    OnMatch_OneToOneAction.Invoke();
                    acceptBase_OneToOne = null;
                    AssembleContainerBase_OneToOne = null;
                    OnMatch_OneToOneAction = null;
                }
            }
            if(OnMatch_OneToManyAction!=null)
            {
                if(CheckMatch_OneToMany())
                {
                    Debug.Log("成功配对OneToMany");
                    OnMatch_OneToManyAction.Invoke();
                    acceptBase_OneToMany = null;
                    AssembleContainerBase_OneToMany.Clear();
                    OnMatch_OneToManyAction = null;
                }
            }
        }
    }
    #endregion
    public class OneToMany
    {
        public string acceptName;
        public List<string> containerNames;
        public OneToMany(string _acceptName, List<string> _containerName)
        {
            acceptName = _acceptName;
            containerNames = _containerName;
        }
    }

    /// <summary>
    /// 组信息
    /// </summary>
    public class OneToOne
    {
        public string acceptName;
        public string containerName;
        public OneToOne(string _acceptName,string _containerName)
        {
            acceptName = _acceptName;
            containerName = _containerName;
        }
    }
}
