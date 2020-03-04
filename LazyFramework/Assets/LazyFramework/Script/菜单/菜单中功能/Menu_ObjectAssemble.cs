using Lazy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AssembleAccept3D_Ray))]
public class  Menu_ObjectAssemble : MenuFunctionBase
{
    AssembleAccept3D_Ray assembleAccept3D_Ray;
    protected  override void Init()
    {
        base.Init();
        funcName = "装配";
        assembleAccept3D_Ray = this.GetComponent<AssembleAccept3D_Ray>();
    }
    /// <summary>
    /// 开启功能
    /// </summary>
    protected override void StartFunc()
    {
        canUse = true;
        assembleAccept3D_Ray.isActive = true;
        assembleAccept3D_Ray.OnDragBegin += OnDragBegin;
        assembleAccept3D_Ray.OnDragEnd += OnDragEnd;
    }
    /// <summary>
    /// 当拖拽开始
    /// </summary>
    private void OnDragBegin()
    {

    }
    /// <summary>
    /// 当拖拽结束
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="assembleContainerBase"></param>
    private void OnDragEnd(object sender, AssembleContainerBase assembleContainerBase)
    {
        assembleAccept3D_Ray.isActive = false;
        OnFunctionEnd();
        assembleAccept3D_Ray.OnDragBegin -= OnDragBegin;
        assembleAccept3D_Ray.OnDragEnd -= OnDragEnd;
    }
}
