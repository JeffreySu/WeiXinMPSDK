/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageTemplateCard.cs
    文件功能描述：机器人响应回复模板卡片消息

    
    创建标识：Wang Qian - 20250805
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 模板卡片消息（未实现）
    /// </summary>
    public class WorkBotResponseMessageTemplateCard : WorkBotResponseMessageBase
    {
        
        public override ResponseMsgType MsgType => ResponseMsgType.Unknown;

        /// <summary>
        /// 对应cardtype
        /// </summary>
        public TemplateCard_CardTypeEnum CardType { get; set; }

        /// <summary>
    }
}