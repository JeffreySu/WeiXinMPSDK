/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageText.cs
    文件功能描述：企业微信机器人响应回复消息文本类型
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 机器人回复欢迎语文本消息，目前仅支持进入会话回调事件时，支持被动回复文本消息
    /// </summary>
    public class WorkBotResponseMessageText : WorkBotResponseMessageBase, IResponseMessageText
    {
        public override ResponseMsgType MsgType => ResponseMsgType.Text;

        /// <summary>
        /// 对应text.content，文本内容
        /// </summary>
        public string Content { get; set; }
    }
}