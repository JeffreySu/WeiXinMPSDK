/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageEvent_Enter.cs
    文件功能描述：用户进入机器人对话的事件推送(enter)
    当用户当天首次进入机器人对话时，会收到该事件推送。
    开发者可回复一条文本或模版消息
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 用户进入机器人对话的事件推送（enter_chat）
    /// </summary>
    public class BotRequestMessageEvent_Enter : BotRequestMessageEventBase
    {
        /// <summary>
        /// 事件对象：仅包含 eventtype = "enter_chat"
        /// </summary>
        public Event_Enter @event { get; set; }

        public class Event_Enter
        {
            public string eventtype { get; set; } = "enter_chat";
        }
    }
}