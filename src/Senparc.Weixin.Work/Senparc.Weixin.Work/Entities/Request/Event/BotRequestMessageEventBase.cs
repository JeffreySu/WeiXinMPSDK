/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageEventBase.cs
    文件功能描述：企业微信智能机器人事件消息基类
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 企业微信智能机器人事件消息基类接口
    /// </summary>
    public interface IBotRequestMessageEventBase : IWorkBotRequestMessageBase, IRequestMessageEvent
    {
        

        /// <summary>
        /// 事件类型，对应event.eventtype
        /// </summary>
        Event Event { get; set; }
    }
    /// <summary>
    /// 具有EventKey属性的RequestMessage接口
    /// </summary>
    public interface IBotRequestMessageEventKey
    {
        string EventKey { get; set; }
    }

    /// <summary>
    /// 企业微信智能机器人事件消息基类
    /// </summary>
    public class BotRequestMessageEventBase : WorkBotRequestMessageBase, IBotRequestMessageEventBase
    {
        /// <summary>
        /// 企业id，对应from.corpid若为企业内部智能机器人不返回
        /// </summary>
        public string CorpId { get; set; }

        /// <summary>
        /// 事件类型，对应event.eventtype
        /// </summary>
        public virtual Event Event { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual object EventType { get { return Event; } }

        /// <summary>
        /// 获取事件类型的字符串
        /// </summary>
        public string EventName { get { return EventType != null ? EventType.ToString() : null; } }

        public override RequestMsgType MsgType => RequestMsgType.Event;
    }
}