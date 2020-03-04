/*******************************************************************************
* 版本声明：v1.0.0
* 类 名 称： MonoExpand
* 创建日期：2019-1-2 15:12:32
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：Mono扩展类,用于扩展mono方法
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lazy;
using System;
using UnityEngine.Events;
using HighlightingSystem;

public static partial class MonoExpand 
{
    #region 点击触发事件
    /// <summary>
    /// 注册物体触发事件（进入时）
    /// </summary>
    /// <param name="go">碰到的物体</param>
    /// <param name="func">事件</param>
    public static void OnTriggerEnter(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnTriggerEnterEvt += func;
    }
    /// <summary>
    /// 注销物体触发事件
    /// </summary>
    /// <param name="go">碰到的物体</param>
    /// <param name="func"></param>
    public static void OffTriggerEnter(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnTriggerEnterEvt -= func;
    }
    /// <summary>
    /// 注册物体触发事件（持续）
    /// </summary>
    /// <param name="go"></param>
    /// <param name="func"></param>
    public static void OnTriggerStay(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnTriggerStayEvt += func;
    }
    public static void OffTriggerStay(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnTriggerStayEvt -= func;
    }
    /// <summary>
    /// 注册物体触发事件（结束时）
    /// </summary>
    /// <param name="go"></param>
    /// <param name="func"></param>
    public static void OnTriggerExit(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnTriggerExitEvt += func;
    }
    /// <summary>
    /// 取消物体触发事件（结束时）
    /// </summary>
    /// <param name="go"></param>
    /// <param name="func"></param>
    public static void OffTriggerExit(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnTriggerExitEvt -= func;
    }

    public static void OnMouseClick_Once(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnClickOnceEvt += func;
    }

    public static void OnMouseClick(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnClickEvt += func;
    }
    public static void OffMouseClick(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnClickEvt -= func;
    }
    public static void OnMouseEnter(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnMouseEnterEvt += func;
    }
    public static void OffMouseEnter(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnMouseEnterEvt -= func;
    }
    public static void OnMouseExit(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnMouseExitEvt += func;
    }
    public static void OffMouseExit(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnMouseExitEvt -= func;
    }
    public static void OnMouseDown(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnMouseDownEvt += func;
    }
    public static void OffMouseDown(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnMouseDownEvt -= func;
    }
    public static void OnMouseUp(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) listener = go.AddComponent<ObjectListen>();
        listener.OnMouseUpEvt += func;
    }
    public static void OffMouseUp(this GameObject go, ListenEventHander func)
    {
        ObjectListen listener = go.GetComponent<ObjectListen>();
        if (listener == null) return;
        listener.OnMouseUpEvt -= func;
    }
    #endregion
    #region 装配
    static AssembleContainerBase assembleContainerBase_OneToOne;
    static AssembleAcceptBase assembleAcceptBase_OneToOne;
    /// <summary>
    /// 开启装配
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="_acceptName">装配物名称</param>
    /// <param name="_containerName">容器名称</param>
    public static void StartAssemble<T>(this T t,string _acceptName, string _containerName)where T:MonoBehaviour
    {
        assembleAcceptBase_OneToOne = AssembleManager.Instance.FindAcceptByName(_acceptName, true);
        assembleContainerBase_OneToOne = AssembleManager.Instance.FindContainerByName(_containerName, true);
        assembleAcceptBase_OneToOne.gameObject.OnHightligher();
        assembleAcceptBase_OneToOne.gameObject.OnMouseDown(OnAssembleStartFunction);
    }
    private static void OnAssembleStartFunction(GameObject go)
    {
        //Debug.Log("assembleContainerBase_OneToOne"+ assembleContainerBase_OneToOne.gameObject.name+ ".assembleAcceptBase_OneToOne"+ assembleAcceptBase_OneToOne.gameObject.gameObject.name);
        assembleAcceptBase_OneToOne.gameObject.OffHightligher(true);
        assembleContainerBase_OneToOne.gameObject.OnHightligher();
        assembleAcceptBase_OneToOne.gameObject.OnMouseUp(OnAssembleEndFunction);
    }
    private static void OnAssembleEndFunction(GameObject go)
    {
        assembleContainerBase_OneToOne.gameObject.OffHightligher(true);
        assembleAcceptBase_OneToOne.gameObject.OffMouseDown(OnAssembleStartFunction);
        assembleAcceptBase_OneToOne.gameObject.OffMouseUp(OnAssembleEndFunction);
        assembleContainerBase_OneToOne = null;
        assembleAcceptBase_OneToOne = null;
    }
    /// <summary>
    /// 开启装配指导
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public static void OnAssembleGuide<T>(this T t) where T : MonoBehaviour
    {
        AssembleManager.Instance.OnSelect += GuideFunc;
    }
    /// <summary>
    /// 关闭装配指导
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public static void OffAssembleGuide<T>(this T t) where T : MonoBehaviour
    {
        AssembleManager.Instance.OnSelect -= GuideFunc;
    }
    public static void GuideFunc(AssembleAcceptBase assembleAcceptBase)
    {
        List<AssembleContainerBase> assembleContainerBases = new List<AssembleContainerBase>();
        assembleContainerBases = AssembleManager.Instance.FindContainersByType(AssembleManager.Instance.CurAcccept.assembleType);
        for (int i = 0; i < assembleContainerBases.Count; i++)
        {
            assembleContainerBases[i].gameObject.OnHightligher();
            assembleContainerBases[i].gameObject.OnMouseUp(GuideMouseUp);
        }
    }
    public static void GuideMouseUp(GameObject go)
    {

    }


    static List<AssembleContainerBase> assembleContainerBases_OneToMany = new List<AssembleContainerBase>();
    static AssembleAcceptBase assembleAcceptBase_OneToMany;
    public static void StartAssemble<T>(this T t, string _acceptName) where T : MonoBehaviour
    {
        assembleAcceptBase_OneToMany = AssembleManager.Instance.FindAcceptByName(_acceptName, true);
        assembleContainerBases_OneToMany = AssembleManager.Instance.FindContainersByType(assembleAcceptBase_OneToMany.assembleType);
        assembleAcceptBase_OneToMany.gameObject.OnHightligher();
        //assembleAcceptBase_OneToMany.gameObject.OnMouseUp();
        for (int i = 0; i < assembleContainerBases_OneToMany.Count; i++)
        {
            assembleContainerBases_OneToMany[i].gameObject.OnHightligher();
        }
        assembleAcceptBase_OneToMany.gameObject.OnHightligher();
        assembleContainerBase_OneToOne.gameObject.OnMouseDown(OnAssembleStartFunction);
    }
    //private static void OnAssembleStart_OneToManyFunction(GameObject go)
    //{
    //    assembleAcceptBase_OneToOne.gameObject.OffHightligher(true);
    //    assembleContainerBase_OneToOne.gameObject.OnHightligher();
    //    assembleAcceptBase_OneToOne.gameObject.OnMouseUp(OnAssembleEndFunction);
    //}
    //private static void OnAssembleEndFunction(GameObject go)
    //{
    //    assembleContainerBase_OneToOne.gameObject.OffHightligher(true);
    //    assembleContainerBase_OneToOne.gameObject.OffMouseDown(OnAssembleStartFunction);
    //    assembleContainerBase_OneToOne.gameObject.OffMouseUp(OnAssembleEndFunction);
    //    assembleContainerBase_OneToOne = null;
    //    assembleContainerBase_OneToOne = null;
    //}
    #endregion
    #region 高亮
    public static GameObject OnUIHightligher(this GameObject go, bool useRecord = false)
    {
        ButtonMgr listener = go.GetComponent<ButtonMgr>();
        if (listener == null) listener = go.AddComponent<ButtonMgr>();
        listener.OnHight(useRecord);
        return go;
    }
    /// <summary>   /// 关闭高光   /// </summary>
    /// <param name="go"></param>
    public static GameObject OffUIHightligher(this GameObject go, bool deleteHighLighter = false)
    {
        ButtonMgr listener = go.GetComponent<ButtonMgr>();
        if (listener == null) listener = go.AddComponent<ButtonMgr>();
        listener.OffHight();
        return go;
    }
    /// <summary>   /// 开启高光   /// </summary>
    /// <param name="go"></param>
    /// <param name="useRecord">是否使用记录</param>
    public static GameObject OnHightligher(this GameObject go)
    {
        HighLighter listener = HighLighter.Get(go);
        listener.SetFlash(true);
        return go;
    }
    /// <summary>   /// 关闭高光   /// </summary>
    /// <param name="go"></param>
    public static GameObject OffHightligher(this GameObject go, bool deleteHighLighter = false)
    {
        HighLighter listener = HighLighter.Get(go);
        listener.SetFlash(false);
        if(deleteHighLighter)
        {
            UnityEngine.Object.Destroy(go.GetComponent<HighLighter>());
            UnityEngine.Object.Destroy(go.GetComponent<Highlighter>());
        }
        return go;
       
    }
    #endregion
    #region MonoBehaviour拓展
    public static Coroutine SetDelay<T>(this T t, float delay, UnityAction action) where T : MonoBehaviour
    {
        ProgramExtensions.Create();
        return ProgramExtensions.Instance.WaitForCompletion(delay, action);
    }
    public static void StopSetDelay<T>(this T t) where T : MonoBehaviour
    {
        if (ProgramExtensions.target == null)
            return;
        ProgramExtensions.Instance.Stop();
    }
    public static T GetOrAddComponent<T>(this GameObject go) where T : MonoBehaviour
    {
        var component = go.GetComponent<T>();
        if (!component)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }
    #endregion
}