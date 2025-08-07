/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageStream.cs
    文件功能描述：机器人响应回复流式消息

    
    创建标识：Wang Qian - 20250807
----------------------------------------------------------------*/


using System.Collections.Generic;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 流式响应消息
    /// </summary>
    public class WorkBotResponseMessageStream : WorkBotResponseMessageBase, IResponseMessageText
    {
        /// <summary>
        /// 流式响应消息的内部类，用于图文混排消息，目前只支持image，对应stream.msg_item集合中的元素
        /// </summary>
        public class MsgItem
        {
            /// <summary>
            /// 消息类型，目前只支持image，对应stream.msg_item.msgtype
            /// </summary>
            public string MsgType { get; set; } = "image";

            /// <summary>
            /// 图片内容的Base64编码,对应image.base64
            /// </summary>
            public string Base64 { get; set; }

            /// <summary>
            /// 图片内容的md5值,对应image.md5,编码前最大不能超过10M
            /// </summary>
            public string Md5 { get; set; }
        }
        public override ResponseMsgType MsgType => ResponseMsgType.Stream;

        /// <summary>
        /// 流式消息id，对应stream.id
        /// </summary>
        public string StreamId { get; set; }

        /// <summary>
        /// 是否结束流式消息
        /// </summary>
        public bool Finish { get; set; }

        /// <summary>
        /// 流式消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 流式消息内容，对应stream.msg_item集合
        /// </summary>
        public List<MsgItem> StreamMsgItem { get; set; }
    }
}