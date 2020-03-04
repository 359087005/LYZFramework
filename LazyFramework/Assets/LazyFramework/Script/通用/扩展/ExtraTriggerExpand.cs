/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：
* 类 名 称：ExtraTriggerEXpand
* 创建日期：2018-05-08 20:47:54
* 作者名称：
* CLR 版本：4.0.30319.42000
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Com.Rainier.Buskit3D;
using Lazy;

/// <summary></summary>
public static  class ExtraTriggerExpand
	{
    

    public static void SetUIClick(this GameObject go, ButtonMgr.VoidDelegate function)
        {
            ButtonMgr listener = go.GetComponent<ButtonMgr>();
            if (listener == null) listener = go.AddComponent<ButtonMgr>();

            listener.onClickLeft = function;
        }
      
      
        /// <summary>    /// 设备认知   /// </summary>
        /// <param name="go"></param>
        /// <param name="name"></param>
        public static GameObject OneselfName(this GameObject go, string name)
        {
          //  Method listener = Method.Get(go);
            RenZhiControl.instance.RenZhiObj.Add(go);
            RenZhiControl.instance.RenZhiNmae.Add(name);
            go.AddHover(RayControl.instance.MouseHoved);
            go.AddExit(RayControl.instance.MouseExit);
            return go;
        }

        /// <summary>   ///步骤提示   /// </summary>
        /// <param name="go"></param>
        /// <param name="Tip">提示内容</param>
        /// <param name="useRecord">是否使用记录</param>
        /// <returns></returns>
        public static void BuZhouTiShi(this MonoBehaviour mb, string Tip,bool useRecord = false)
        {
            if (useRecord)
            {
                TiShiDataEntity tiShiEntity = (TiShiDataEntity)(TiShiTextControl.instance.gameObject.GetComponent<TiShiDataModel>().DataEntity);
                tiShiEntity.tiShiStr = Tip;
            }
            else
            {
                if (TiShiTextControl.GetInstance())
                    TiShiTextControl.GetInstance().ChangeText(Tip);
            }
        }
        /// <summary>
        /// 添加点击事件
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="id"></param>
        public static GameObject AddClickedObj(this GameObject go, Dictionary<string,GameObject> objDic)
        {
            if(!objDic.ContainsValue(go))
            objDic.Add(go.name,go);
            return go;
        }

        /// <summary>
        /// 步骤提示
        /// </summary>
        /// <param name="mb"></param>
        /// <param name="id"></param>
        public static void  BuZhouTiShi(this MonoBehaviour mb, int id)
        {
            if (TiShiTextControl.GetInstance())
                TiShiTextControl.GetInstance().ChangeText(id);
        }
	}


