/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：AssembleAccept3D_Ray
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：通过射线检测 进行装配
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Lazy
{
    public class AssembleAccept3D_Ray : AssembleAcceptBase
    {
        public Transform eyes;
        public float eyesDistance = 3;
        public float prepareSpeed = 0.5f;
        Vector3 screenPosition;
        Vector3 mScreenPosition; 
        Vector3 offset;
        RaycastHit[] hits;
        public float speed = 1;
        public delegate void OnAssembleAcceptEventHandler(object sender, AssembleContainerBase assembleContainerBase);
        public event Action OnDragBegin;
        public event OnAssembleAcceptEventHandler OnDragEnd;
        AssembleContainerBase tempContainer;
        bool isUse = false;
        bool prepareFinish = false;
        bool isButtonDown = false;

        Vector3 preParePoint;
  
        protected override void Start()
        {
            base.Start();
            if (eyes == null)
                eyes = Camera.main.transform;
        }
        private void Update()
        {
            Drag();
        }
        private void Drag()
        {
            if (!isActive) return;
            if (!isUse)
            {
                assembleManager.OnSelect.Invoke(this);
                PrepareAssmeble();
            }
            else if (prepareFinish)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    containerBase = null;
                    if (Physics.Raycast(ray, out hit, 100) && hit.transform.name == obj.name)
                    {
                        screenPosition = Camera.main.WorldToScreenPoint(obj.transform.position);
                        mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                        offset = obj.transform.position - Camera.main.ScreenToWorldPoint(mScreenPosition);
                        isButtonDown = true;
                    }
                    if (OnDragBegin != null)
                        OnDragBegin.Invoke();
                }
                if (Input.GetMouseButton(0) && isButtonDown)
                {
                    mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                    obj.transform.position = Camera.main.ScreenToWorldPoint(mScreenPosition) + offset;
                }
                if(Input.GetMouseButtonUp(0) && isButtonDown)
                {
                    isUse = false;
                    isActive = false;
                    isButtonDown = false;
                    tempContainer = FindContainer();
                    if (JugeAssmeble(tempContainer))
                    {
                        BeginAssmeble(tempContainer);
                    }
                    if (OnDragEnd != null)
                    {
                        OnDragEnd.Invoke(this, containerBase);
                    }
                }
            }
        }
        /// <summary>
        /// 装配预备
        /// </summary>
        void PrepareAssmeble()
        {
            isUse = true;
            prepareFinish = false;
            preParePoint = eyes.transform.position + eyes.forward * eyesDistance;
            obj.transform.DOMove(preParePoint, prepareSpeed).OnComplete(()=>
            {
                prepareFinish = true;
            });
        }
        /// <summary>
        /// 寻找容器
        /// </summary>
        /// <returns></returns>
        private AssembleContainerBase FindContainer()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 500);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<AssembleContainerBase>() != null && hits[i].transform.gameObject != gameObject)
                    return hits[i].transform.GetComponent<AssembleContainerBase>();
            }
            return null;
        }
        int curPointIndex = 0;
        /// <summary>
        /// 装配动画
        /// </summary>
        private void MoveFunc()
        {
            if (curPointIndex + 1 <= containerBase.animPoint.Count)
            {
                obj.transform.DOMove(containerBase.animPoint[curPointIndex].position, speed).OnComplete(() =>
                {
                    curPointIndex++;
                    if(curPointIndex+1 > containerBase.animPoint.Count)
                    {
                        FinishAssmeble();
                    }
                    MoveFunc();
                });
            }
            else
                return;
        }
        /// <summary>
        /// 开始装配
        /// </summary>
        /// <param name="_containerBase"></param>
        public override void BeginAssmeble(AssembleContainerBase _containerBase)
        {
            base.BeginAssmeble(_containerBase);
            if (tempContainer != null)
            {
                tempContainer = null;
                curPointIndex = 0;
                MoveFunc();
            }
        }
        /// <summary>
        /// 当装配物离开时
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerExit(Collider other)
        {
            if (containerBase==null) return;
            if (other.GetComponent<AssembleContainerBase>() != null)
            {
                if (containerBase == other.GetComponent<AssembleContainerBase>())
                {
                    containerBase.RelieveAssmeble();
                    RelieveAssmeble();
                }
            }
        }
    }
}
