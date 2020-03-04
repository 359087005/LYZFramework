#if UNITY_2018_3_2
#define unity2018
#endif

/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIEffectExtension
* 创建日期：2019-12-12 14:43:39
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：一些ui特效拓展,闪光,溶解,渐变色等
* 
******************************************************************************/

using UnityEngine;
using DT = DG.Tweening;
using DG.Tweening;
using UnityEngine.UI;
using Coffee.UIExtensions;
using System.Collections.Generic;
using System.Diagnostics;
using Lazy;

public static class UIEffectExtension
{
    private static Dictionary<GameObject, (DT.Core.TweenerCore<Vector2, Vector2, DT.Plugins.Options.VectorOptions> dis, Tweener alpha)> UIOutLine_Cache = new Dictionary<GameObject, (DT.Core.TweenerCore<Vector2, Vector2, DT.Plugins.Options.VectorOptions> dis, Tweener alpha)>();
#if UNITY_2018_3_0

    /// <summary>
    /// 显示图片或文字,溶解动画特效
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIDissolve(this GameObject go, Color color, bool open)
    {
        var image = go.GetComponent<Image>();
        var text = go.GetComponent<Text>();
        if (!image && !text) return go;
        var dissolve = GetOrAddComponent<UIDissolve>(go);
        dissolve.color = color;
        if (image)
            GetOrAddComponent<Mask>(go).showMaskGraphic = true;
        if (!open)
        {
            dissolve.Play();
        }
        else
        {
            DOTween.To(() => dissolve.effectFactor, x => dissolve.effectFactor = x, 0f, 1f);
        }

        return go;
    }

    /// <summary>
    /// 显示图片或文字,溶解中的状态
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="effectvalue">0不溶解,1完全溶解</param>
    /// <returns></returns>
    public static GameObject OnUIDissolve(this GameObject go, Color color, float effectvalue)
    {
        var image = go.GetComponent<Image>();
        var text = go.GetComponent<Text>();
        if (!image && !text) return go;
        if (image)
            GetOrAddComponent<Mask>(go).showMaskGraphic = true;

        var dissolve = GetOrAddComponent<UIDissolve>(go);
        dissolve.color = color;
        dissolve.effectFactor = effectvalue;
        return go;
    }

    [Conditional("unity2018")]
    /// <summary>
    /// 显示图片或文字,高光划过特效
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns> 
    public static void OnUIShiny(this GameObject go, float rotation = 0f)
    {
        var image = go.GetComponent<Image>();
        var text = go.GetComponent<Text>();
        if (!image && !text) return;


        var uishiny = go.GetComponent<UIShiny>();
        if (!uishiny)
        {
            uishiny = go.AddComponent<UIShiny>();
        }
        uishiny.rotation = rotation;
        uishiny.Play();
        return;
    }


    /// <summary>
    /// 变灰
    /// </summary>
    /// <param name="go"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static GameObject OnUIGray(this GameObject go, float factor)
    {
        var uie = GetOrAddComponent<UIEffect>(go);
        go.GetComponent<Graphic>().material.EnableKeyword("GRAYSCALE");
        uie.effectMode = EffectMode.Grayscale;
        uie.effectFactor = factor;
        return go;
    }

    /// <summary>
    /// 变灰
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIGray(this GameObject go)
    {
        var uie = GetOrAddComponent<UIEffect>(go);
        go.GetComponent<Graphic>().material.EnableKeyword("GRAYSCALE");
        uie.effectMode = EffectMode.Grayscale;
        DOTween.To(x => uie.effectFactor = x, 0, 1, 0.5f);
        return go;

    }

#endif

    /// <summary>
    /// 图片或文字叠加一层渐变色
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2)
    {
        var image = go.GetComponent<Image>();
        var text = go.GetComponent<Text>();
        if (!image && !text) return go;
        var uie = go.GetComponent<UIGradient>();
        if (!uie)
        {
            uie = go.AddComponent<UIGradient>();
        }
        uie.direction = UIGradient.Direction.Horizontal;
        uie.color1 = color1;
        uie.color2 = color2;
        return go;
    }

    /// <summary>
    /// 图片或文字叠加一层渐变色
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2, Color color3, Color color4)
    {
        var image = go.GetComponent<Image>();
        var text = go.GetComponent<Text>();
        if (!image && !text) return go;
        var uie = go.GetComponent<UIGradient>();
        if (!uie)
        {
            uie = go.AddComponent<UIGradient>();
        }
        uie.direction = UIGradient.Direction.Diagonal;
        uie.color1 = color1;
        uie.color2 = color2;
        uie.color3 = color3;
        uie.color4 = color4;
        return go;
    }

    /// <summary>
    /// 图片或文字叠加一层渐变色并旋转
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2, float duration, float angle)
    {
        go.OnUIJianBian_Gradient(color1, color2);
        var uie = GetOrAddComponent<UIGradient>(go);
        uie.direction = UIGradient.Direction.Angle;
        uie.rotation = 0f;
        DOTween.To(() => uie.rotation, x => uie.rotation = x, angle, duration);
        return go;
    }

    /// <summary>
    /// 图片或文字叠加一层渐变色并旋转
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2, Color color3, Color color4, float duration, float angle)
    {
        go.OnUIJianBian_Gradient(color1, color2, color3, color4);
        var uie = GetOrAddComponent<UIGradient>(go);
        uie.direction = UIGradient.Direction.Diagonal;
        uie.rotation = 0f;
        DOTween.To(() => uie.rotation, x => uie.rotation = x, angle, duration);
        return go;
    }
    /// <summary>
    /// 图片或文字叠加一层渐变色并且渐变色移动
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    /// <param name="duration"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2, bool left2right)
    {
        go.OnUIJianBian_Gradient(color1, color2);
        var uie = GetOrAddComponent<UIGradient>(go);
        uie.direction = UIGradient.Direction.Horizontal;
        uie.offset = left2right ? -1f : 1f;
        DOTween.To(() => uie.offset, x => uie.offset = x, left2right ? 1f : -1f, 1f);
        return go;
    }

    /// <summary>
    /// 图片或文字叠加一层渐变色并且渐变色移动
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    /// <param name="duration"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static GameObject OnUIJianBian_Gradient(this GameObject go, Color color1, Color color2, Color color3, Color color4, bool left2right)
    {
        go.OnUIJianBian_Gradient(color1, color2, color3, color4);
        var uie = GetOrAddComponent<UIGradient>(go);
        uie.direction = UIGradient.Direction.Horizontal;
        uie.offset = left2right ? -1f : 1f;
        DOTween.To(() => uie.offset, x => uie.offset = x, left2right ? 1f : -1f, 1f);
        return go;
    }


    /// <summary>
    /// 图片或文字添加轮廓线并闪烁的效果
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isOn"></param>
    /// <param name="color"></param>
    /// <param name="outLineSize">轮廓大小</param>
    /// <returns></returns>
    public static GameObject OnUIOutLine(this GameObject go, bool isOn, Color color = default, float outLineSize = 5f)
    {

        var uie = GetOrAddComponent<UIEffect>(go);
        var uis = GetOrAddComponent<UIShadow>(go);
        uis.style = ShadowStyle.Outline8;
        if (color == default)
        {
            color = Color.green;
        }
        color.a = 0f;
        uis.effectColor = color;

        if (isOn)
        {
            if (UIOutLine_Cache.ContainsKey(go))
            {
                UIOutLine_Cache[go].dis.Play().SetLoops(-1, LoopType.Yoyo);
                UIOutLine_Cache[go].alpha.Play().SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                var dis = DOTween.To(() => uis.effectDistance, (x) => uis.effectDistance = x, Vector2.one * outLineSize, 1f).SetLoops(-1, LoopType.Yoyo).Play();
                var alpha = DOTween.ToAlpha(() => uis.effectColor, (x) => uis.effectColor = x, 0.5f, 1f).SetLoops(-1, LoopType.Yoyo).Play();
                UIOutLine_Cache.Add(go, (dis, alpha));
            }
        }
        else
        {
            if (UIOutLine_Cache.ContainsKey(go))
            {
                UIOutLine_Cache[go].dis.Pause();
                UIOutLine_Cache[go].alpha.Pause();
            }
        }
        return go;
    }
    private static T GetOrAddComponent<T>(GameObject go) where T : MonoBehaviour
    {
        var component = go.GetComponent<T>();
        if (!component)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }
}

