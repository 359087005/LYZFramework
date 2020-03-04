/*******************************************************************************
* 类 名 称：EventManager
* 创建日期：2020-1-2
* 作者名称：林奕州
* CLR 版本：4.0.30319.42000
* 功能描述：
* 备注：
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主题事件
/// </summary>
public static class EventManager
{
    public delegate void MessageDelegate(object sender, params object[] message);

    private static Dictionary<string, List<MessageDelegate>> dictionary_LogEvent = new Dictionary<string, List<MessageDelegate>>();

    /// <summary>
    /// 增加一个事件
    /// </summary>
    /// <param name="topic">事件的主题</param>
    /// <param name="logDelegate">事件要执行的内容（不建议用匿名函数，因为可能牵扯到事件注销）</param>
    public static void AddEvent(string topic, MessageDelegate logDelegate)
    {
        if (dictionary_LogEvent.ContainsKey(topic))
            dictionary_LogEvent[topic].Add(logDelegate);
        else
        {
            List<MessageDelegate> temp = new List<MessageDelegate>();
            temp.Add(logDelegate);
            dictionary_LogEvent.Add(topic, temp);
        }
       
    }
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="topic">事件主题</param>
    /// <param name="sender">消息发送者</param>
    /// <param name="message">发送的消息内容</param>
    public static void SendMessage(string topic, object sender, params object[] message)
    {
        foreach (var item in dictionary_LogEvent[topic])
        {
            item.Invoke(sender, message);
        }
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="topic">事件主题</param>
    /// <param name="message">需要移除的方法</param>
    public static void RemoveEvent(string topic, MessageDelegate message)
    {
        if (!dictionary_LogEvent[topic].Contains(message)) return;
        dictionary_LogEvent[topic].Remove(message);
    }
    /// <summary>
    /// 移除整个主题
    /// </summary>
    /// <param name="topic">事件主题</param>
    public static void RemoveTopic(string topic)
    {
        if (!dictionary_LogEvent.ContainsKey(topic)) return;
        dictionary_LogEvent.Remove(topic);
    }
    /// <summary>
    /// 清除全部注册的事件
    /// </summary>
    public static void ClearAll()
    {
        dictionary_LogEvent.Clear();
    }
}
