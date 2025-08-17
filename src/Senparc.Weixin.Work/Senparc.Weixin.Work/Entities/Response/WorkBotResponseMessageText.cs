/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageText.cs
    文件功能描述：企业微信机器人响应回复消息文本类型
    
    
    创建标识：Wang Qian - 20250804

    修改标识：Wang Qian - 20250817
    修改描述：修改响应消息基类，直接映射文档原始键名，便于无特性反序列化
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 机器人回复欢迎语文本消息，目前仅支持进入会话回调事件时，支持被动回复文本消息
    /// </summary>
    public class WorkBotResponseMessageText : WorkBotResponseMessageBase
    {
        public string content { get; set; }
    }
}