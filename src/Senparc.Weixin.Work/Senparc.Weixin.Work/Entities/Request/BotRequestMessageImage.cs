/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageImage.cs
    文件功能描述：接收图片消息
    
    
    创建标识：Wang Qian - 20250803

----------------------------------------------------------------*/


using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class BotRequestMessageImage : WorkBotRequestMessageBase, IRequestMessageImage
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Image; }
        }
        /// <summary>
        /// 因为接口约束必须实现的属性，在这里不使用
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 图片链接，对应image.url
        /// </summary>
        public string PicUrl { get; set; }
    }
}