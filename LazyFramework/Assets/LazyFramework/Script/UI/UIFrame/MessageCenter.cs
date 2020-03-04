/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MessageCenter
* 创建日期：2019-12-13 10:32:43
* 作者名称：张文政
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Lazy
{
    public class Message
    {
        public ushort id;
        public object[] arguments;
        
        public Message(ushort id, params object[] arguments)
        {
            this.id = id;
            this.arguments = arguments;
        }

    }
    /// <summary>
    /// 
    /// </summary>
	public class MessageCenter
    {
        private static Dictionary<UInt16, List<IProcessEvent>> eventDic = new Dictionary<ushort, List<IProcessEvent>>();


        public static void SendEvent(Message message)
        {
            if (!eventDic.ContainsKey(message.id))
            {
                Debug.LogError("没有监听" + message.id);
                
                return;
            }
            var allLinstener = eventDic[message.id];
            for (int i = 0; i < allLinstener.Count; i++)
            {
                if (allLinstener[i] != null)
                {
                allLinstener[i].ProcessEvent(message);
                }
            }

        }

        public static void RegisterEvent(ushort id, IProcessEvent processEvent)
        {
            if (!eventDic.ContainsKey(id))
                eventDic.Add(id, new List<IProcessEvent>());
            if (!eventDic[id].Contains(processEvent))
                eventDic[id].Add(processEvent);

        }

        public static void UnregisterEvent(ushort id, IProcessEvent processEvent)
        {
            if (!eventDic.ContainsKey(id))
            {
                Debug.LogError("未注册事件" + id.ToString());
                return;
            }


            if (eventDic[id].Contains(processEvent))
            {
                eventDic[id].Remove(processEvent);
            }
        }
    }

    public static class MessageCenterExpand
    {
        public static void Register(this IProcessEvent processEvent, ushort id)
        {
            MessageCenter.RegisterEvent(id, processEvent);
        }
        public static void Unregister(this IProcessEvent processEvent, ushort id)
        {
            MessageCenter.UnregisterEvent(id, processEvent);
        }
    }
}

