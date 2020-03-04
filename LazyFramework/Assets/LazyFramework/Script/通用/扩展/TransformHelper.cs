using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class TransformHelper 
{
    /// <summary>
    /// 深度遍历 某物体子物体，返回第一个<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="trans"></param>
    /// <param name="goName"></param>
    /// <returns></returns>
    public static T FindFormAllChild<T>(this Transform trans, string goName) where T : Object
    {
        Transform child = trans.Find(goName);
        if (child != null)
        {
            return child.GetComponent<T>();
        }
        Transform go = null;
        for (int i = 0; i < trans.childCount; i++)
        {
            child = trans.GetChild(i);
            go = FindFormAllChild<Transform>(child, goName);
            if (go != null)
            {
                return go.GetComponent<T>();
            }
        }
        return null;
    }
}
