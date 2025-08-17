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
    public class WorkBotResponseMessageStream : WorkBotResponseMessageBase
    {
        /// <summary>
        /// 流式响应消息的内部类，用于图文混排消息，目前只支持image，对应stream.msg_item集合中的元素
        /// </summary>
        public Stream stream { get; set; }

        /// <summary>
        /// 对应stream.msg_item集合中的元素的类，目前只支持图片
        /// </summary>
        public class MsgItem
        {
            public string msgtype { get; set; }
            public Image image { get; set; }

            public class Image
            {
                public string base64 { get; set; }
                public string md5 { get; set; }
            }
        }

        public class Stream
        {
            public string id { get; set; }
            public bool finish { get; set; }

            public string content { get; set; }

            public List<MsgItem> msg_item { get; set; }
        }
    }
}