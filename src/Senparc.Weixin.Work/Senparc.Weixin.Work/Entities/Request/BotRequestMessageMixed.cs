/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageMixed.cs
    文件功能描述：接收图文混排消息
    
    
    创建标识：Wang Qian - 20250803

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class BotRequestMessageMixed : WorkBotRequestMessageBase, IRequestMessageText, IRequestMessageImage
    {
        /// <summary>
        /// 返回Unknown，进行特殊处理
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Mixed;

        /// <summary>
        /// 因为接口约束必须实现的属性，在这里不使用
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 图片链接，对应image.url
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 文本消息内容，对应text.content
        /// </summary>
        public string Content { get; set; }
    }
}