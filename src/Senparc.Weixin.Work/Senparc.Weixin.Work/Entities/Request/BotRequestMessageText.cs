/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageText.cs
    文件功能描述：接收文本消息
    
    
    创建标识：Wang Qian - 20250803

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class BotRequestMessageText : WorkBotRequestMessageBase, IRequestMessageText
    {
        public override RequestMsgType MsgType => RequestMsgType.Text;

        /// <summary>
        /// 文本消息内容，对应text.content
        /// </summary>
        public string Content { get; set; }
    }
}
