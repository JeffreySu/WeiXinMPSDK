/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotRequestMessageBase.cs
    文件功能描述：企业微信智能机器人接收到请求的消息基类
    
    
    创建标识：Wang Qian - 20250802
----------------------------------------------------------------*/
using Senparc.NeuChar;
using Senparc.Weixin.Work.Entities.BotChatObjects;

namespace Senparc.Weixin.Work.Entities
{
    
    public class WorkBotRequestMessageBase
    {
        
        /// <summary>
        /// 对应msgid，本次回调的唯一性标志，开发者需据此进行事件排重（可能因为网络等原因重复回调）
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 对应aibotid，智能机器人id
        /// </summary>
        public string AIBotId { get; set; }

        /// <summary>
        /// 对应chatid，会话id，仅群聊类型时候返回
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// 对应chattype，会话类型，single\group，分别表示：单聊\群聊
        /// </summary>
        public ChatType ChatType { get; set; }

        /// <summary>
        /// 对应from，该事件触发者的信息，里面包含userid
        /// </summary>
        public From From { get; set; }
        
        /// <summary>
        /// 消息类型，对应msgtype参数，使用NeuChar中的RequestMsgType枚举，支持文本、图片、语音、视频、位置以及链接信息。
        /// </summary>
        public virtual MsgType MsgType 
        { 
            get { return MsgType.Unknown; }
        }
    }
}