/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotRequestMessageBase.cs
    文件功能描述：企业微信智能机器人接收到请求的消息基类
    
    
    创建标识：Wang Qian - 20250802
----------------------------------------------------------------*/
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 会话类型，single\group，分别表示：单聊\群聊
    /// </summary>
    public enum ChatType 
    {
        Single,
        Group
    }

    public interface IWorkBotRequestMessageBase : IRequestMessageBase
    {
        string ChatId { get; set; }
        ChatType ChatType { get; set; }

    }
    
    /// <summary>
    /// 企业微信智能机器人接收到请求的消息基类
    /// </summary>
    public abstract class WorkBotRequestMessageBase : RequestMessageBase, IWorkBotRequestMessageBase
    {
        //基类中包含:
        
        //RequestMessageBase 中包含：
        //MsgId:对应msgid，本次回调的唯一性标志，开发者需据此进行事件排重（可能因为网络等原因重复回调）
        //MsgType:对应msgtype，消息类型

        //MessageBase 中包含:
        //ToUserName: 对应aibotid，智能机器人id
        //FromUserName: 对应userid，用户id
        


        /// <summary>
        /// 对应chatid，会话id，仅群聊类型时候返回
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// 对应chattype，会话类型，single\group，分别表示：单聊\群聊
        /// </summary>
        public ChatType ChatType { get; set; }

        //默认返回Unknown，如果返回Unknown，说明是mixed或者stream消息，要通过msgtype来判断，特殊处理
        public override RequestMsgType MsgType 
        { 
            get
            {
                return RequestMsgType.Unknown;
            }
        }

    }
}