/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageStream.cs
    文件功能描述：接收流式消息
    
    
    创建标识：Wang Qian - 20250803

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class BotRequestMessageStream : WorkBotRequestMessageBase
    {
        /// <summary>
        /// 默认返回Unknown类型，进行特殊处理
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Stream;

        /// <summary>
        /// 流式消息id，对应stream.id
        /// </summary>
        public string StreamId { get; set; }
    }
}