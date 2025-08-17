/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageBase.cs
    文件功能描述：企业微信机器人响应回复消息基类
    
    
    创建标识：Wang Qian - 20250804

    修改标识：Wang Qian - 20250817
    修改描述：修改响应消息基类，直接映射文档原始键名，便于无特性反序列化
----------------------------------------------------------------*/
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{

    public class WorkBotResponseMessageBase 
    {
        public string msgtype { get; set; }
    }
}